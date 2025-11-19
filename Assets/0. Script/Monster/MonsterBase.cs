using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct MonsterData
{
    public float IdleTime;

    public int MoveDirection;
    public float PatrolTime;
    public float PatrolSpeed;

    public float AggroRange;
    public float AttackRange;
    public float SkillActiveRange;
    public float SkillReadyTime;
    public float SkillPower;
}

public enum MonsterStateType { Idle, Patrol, Chase, Attack, Skill, Dead }
public enum MonsterSkillType { None, Skill_A, Skill_B, Skill_C }

public abstract class MonsterBase : MonoBehaviour
{
    public MonsterData monsterData;
    public MonsterStateType currentState = MonsterStateType.Idle;
    public MonsterSkillType selectedSkill = MonsterSkillType.None;

    public float StateTimer;

    public float DistanceToPlayer; // 플레이어와 적 사이의 거리

    public bool isSkillReady;
    public float SkillCoolTime;

    public Transform PlayerPosition; // 플레이어 현재 위치
    public Rigidbody2D rb;

    protected Coroutine skillCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // 인스펙터에서 안 넣어줬으면 Tag로 자동 찾기
        if (PlayerPosition == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                PlayerPosition = player.transform;
            else
                Debug.LogWarning($"{name} : Player Tag를 가진 오브젝트를 찾지 못했습니다.");
        }
    }

    private void Start()
    {
        MonsterDataSetting();
    }

    private void Update()
    {
        MonsterFSM();
    }

    public abstract void MonsterDataSetting();

    public virtual void MonsterFSM()
    {
        StateTimer += Time.deltaTime;

        if (PlayerPosition != null)
            DistanceToPlayer = Vector2.Distance(transform.position, PlayerPosition.position);

        Debug.Log($"{gameObject.name} ({GetInstanceID()}) State: {currentState}");

        switch (currentState)
        {
            case MonsterStateType.Idle: Idle(); break;
            case MonsterStateType.Patrol: Patrol(); break;
            case MonsterStateType.Chase: Chase(PlayerPosition); break;
            case MonsterStateType.Skill: Skill(); break;
        }
    }

    public virtual void ChangeState(MonsterStateType nextState)
    {
        currentState = nextState;
        StateTimer = 0f;
    }

    public virtual void Idle()
    {
        if(StateTimer >= monsterData.IdleTime)
        {
            ChangeState(MonsterStateType.Patrol);
        }
        if (DistanceToPlayer <= monsterData.AggroRange)
        {
            ChangeState(MonsterStateType.Chase);
        }
    }

    public virtual void Patrol()
    {
        transform.position += new Vector3(monsterData.MoveDirection * monsterData.PatrolSpeed * Time.deltaTime, 0f, 0f);

        if(StateTimer >= monsterData.PatrolTime)
        {
            monsterData.MoveDirection *= -1;
            ChangeState(MonsterStateType.Idle);
        }

        if(DistanceToPlayer <= monsterData.AggroRange)
        {
            ChangeState(MonsterStateType.Chase);
        }
    }

    public virtual void Chase(Transform PlayerPosition)
    {
        Vector2 TargetPosition = new Vector2(PlayerPosition.position.x, transform.position.y);

        Vector2 direction = (TargetPosition - (Vector2)transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position,TargetPosition,monsterData.PatrolSpeed * Time.deltaTime);

        if (DistanceToPlayer > monsterData.AggroRange * 3f)
        {
            ChangeState(MonsterStateType.Idle);
        }
        
        if (DistanceToPlayer <= monsterData.SkillActiveRange && isSkillReady)
        {
            ChangeState(MonsterStateType.Skill);
        }
    }

    public virtual void Skill()
    {
        if (StateTimer >= monsterData.SkillReadyTime && isSkillReady)
        {
            if (skillCoroutine != null)
                StopCoroutine(skillCoroutine);

            skillCoroutine = StartCoroutine(UsingDash());
            ChangeState(MonsterStateType.Chase);
        }
    }

    protected virtual IEnumerator UsingDash()
    {
        yield break;
    }

    // ********* 작업중 ************
    //protected virtual void UseSkill(MonsterSkillType skillType)
    //{
    //    if (!isSkillReady || skillType == MonsterSkillType.None) return;

    //    if (skillCoroutine != null) StopCoroutine(skillCoroutine);

    //    skillCoroutine = StartCoroutine(SkillSelect(skillType));
    //}

    //protected virtual IEnumerator SkillSelect(MonsterSkillType skillType)
    //{
    //    isSkillReady = false;

    //    switch (skillType)
    //    {
    //        case MonsterSkillType.Skill_A: yield return SkillA(); break;
    //        case MonsterSkillType.Skill_B: yield return SkillB(); break;
    //        case MonsterSkillType.Skill_C: yield return SkillC(); break;
    //    }

    //    isSkillReady = true;
    //    ChangeState(MonsterStateType.Chase);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            monsterData.MoveDirection *= -1;
        }
    }
}