using UnityEngine;

//public struct EnemyData
//{
//    public float Test;
//}

public enum EnemyStateType { Idle, Patrol, Chase, Attack, Skill_A ,Dead }

public interface IEnemyStradegy
{
    void ChangeState(EnemyStateType nextState);

    void Idle();

    void Patrol();

    void Chase();

    void Attack();

    void Skill();

    void Dead();
}
