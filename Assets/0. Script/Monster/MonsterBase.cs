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
    public float SkillPower;

    public Rigidbody2D rb;
    
}

public enum MonsterStateType { Idle, Patrol, Chase, Attack, Skill_A, Dead }

public abstract class MonsterBase : MonoBehaviour
{
    public MonsterData monsterData;
    public MonsterStateType currentState = MonsterStateType.Idle;

    float DistanceToP;
    float StateTimer;
    public Transform PlayerPosition;

    public abstract void MonsterData();

    public virtual void MonsterFSM()
    {
        StateTimer += Time.deltaTime;

        switch (currentState)
        {
            case MonsterStateType.Idle: Idle(); break;
            case MonsterStateType.Patrol: Patrol(); break;
            case MonsterStateType.Chase: Chase(PlayerPosition); break;
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
        if (DistanceToP <= monsterData.AggroRange)
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
            ChangeState(MonsterStateType.Patrol);
        }

        if(DistanceToP <= monsterData.AggroRange)
        {
            ChangeState(MonsterStateType.Chase);
        }
    }

    public virtual void Chase(Transform PlayerPosition)
    {
        Vector2 TargetPosition = new Vector2(PlayerPosition.position.x, transform.position.y);

        Vector2 direction = (TargetPosition - (Vector2)transform.position).normalized;

        monsterData.rb.linearVelocity = direction * monsterData.PatrolSpeed;
    }
}
