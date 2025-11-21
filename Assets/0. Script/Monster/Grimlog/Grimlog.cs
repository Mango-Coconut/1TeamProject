using System.Collections;
using UnityEngine;

public class Grimlog : MonsterBase, IAttackable
{
    HP hp;
    [SerializeField] float damage = 20f;
    public float Damage { get { return damage; } }

    public override void MonsterDataSetting()
    {
        monsterData.IdleTime = 3f;
        monsterData.PatrolSpeed = 3f;
        monsterData.PatrolTime = 3f;

        monsterData.MoveDirection = -1;

        monsterData.AggroRange = 10f;
        monsterData.SkillActiveRange = 6f;
        monsterData.SkillPower = 20f;

        monsterData.SkillA_coolTime = 15f;
    }

    bool canUseSkillA = true;

    protected override MonsterSkillType DecideSkillType()
    {
        if (!isSkillReady) return MonsterSkillType.None;

        if (DistanceToPlayer <= monsterData.SkillActiveRange && canUseSkillA)
            return MonsterSkillType.Skill_A;
        else return MonsterSkillType.None;
    }

    protected override IEnumerator SkillA()
    {
        canUseSkillA = false;

        float dirX = Mathf.Sign(PlayerPosition.position.x - transform.position.x);

        yield return new WaitForSeconds(0.5f);

        rb.AddForce(new Vector2(dirX * monsterData.SkillPower, 0f), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        rb.linearVelocity = new Vector2(0f, 0f);

        yield return new WaitForSeconds(3f);

        ChangeState(MonsterStateType.Patrol);

        yield return new WaitForSeconds(monsterData.SkillA_coolTime);

        canUseSkillA = true;
    }
}
