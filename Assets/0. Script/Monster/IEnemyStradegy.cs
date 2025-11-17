using UnityEngine;

public enum EnemyStateType { Idle, Patrol, Chase, Attack, Skill ,Dead }

public interface IEnemyStradegy
{
    void ChangeState(EnemyStateType nextState);

    void Idle();

    void Patrol();

    void Chase();

    void Attack();

    void SkillA();

    void Dead();
}
