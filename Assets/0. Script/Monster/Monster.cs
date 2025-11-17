using UnityEngine;

public class Monster : MonoBehaviour, IEnemyStradegy
{
    public EnemyStateType currentState = EnemyStateType.Idle;

    [SerializeField]
    float idleTime = 2f;
    float patrolSpeed = 2f;
    float patrolTime = 3f;
    float StateTimer = 0f;
    int movedir = 1;

    [SerializeField]
    float AggroDis = 6f;
    float AttackDis;
    float SkillDis = 3f;

    Vector2 PatrolDir;

    float DistanceToPlayer;
    public Transform PlayerTransform;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PatrolDir = Vector2.right;
        ChangeState(EnemyStateType.Idle);
    }

    void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, PlayerTransform.position);

        StateTimer += Time.deltaTime;
        Debug.Log(currentState);

        switch (currentState)
        {
            case EnemyStateType.Idle:
                Idle();
                break;

            case EnemyStateType.Patrol:
                Patrol();
                break;

            case EnemyStateType.Chase:
                Chase();
                break;

            case EnemyStateType.Skill:
                SkillA();
                break;
        }

    }

    public void ChangeState(EnemyStateType nextState)
    {
        currentState = nextState;
        StateTimer = 0f;
    }

    public void Idle()
    {
        // 일정 범위 안에서 이동 -> 정지 반복 
        if (StateTimer >= idleTime)
        {
            ChangeState(EnemyStateType.Patrol);
        }

        if (DistanceToPlayer <= AggroDis)
        {
            ChangeState(EnemyStateType.Chase);
        }
    }

    public void Patrol()
    {
        // 일정 범위 안에서 이동 -> 정지 반복
        transform.position += new Vector3(movedir * patrolSpeed * Time.deltaTime, 0f, 0f);

        if (StateTimer >= patrolTime)
        {
            movedir *= -1;
            ChangeState(EnemyStateType.Idle);
        }

        if (DistanceToPlayer <= AggroDis)
        {
            ChangeState(EnemyStateType.Chase);
        }
    }

    public void Chase()
    {
        // 추노
        Vector2 targetPos = new Vector2(PlayerTransform.position.x, transform.position.y);

        Vector2 dir = (targetPos - (Vector2)transform.position).normalized;

        rb.linearVelocity = dir * patrolSpeed;
    }

    public void Attack()
    {

    }

    public void SkillA()
    {

    }

    public void Dead()
    {
        ChangeState(EnemyStateType.Dead);

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 벽에 충돌 시 반대 방향으로 이동
        if (collision.gameObject.CompareTag("Wall"))
        {
            movedir *= -1;
        }

        //// 플레이어와 충돌 시 플레이어 체력 감소
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    //TempGameManager.instance.AttackDmg(CollisionDMG);
        //}
    }
}
