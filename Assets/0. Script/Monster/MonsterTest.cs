using System.Collections;
using UnityEngine;

public class MonsterTest : MonsterBase
{
    public override void MonsterDataSetting()
    {
        monsterData.IdleTime = 3f;
        monsterData.PatrolSpeed = 3f;
        monsterData.PatrolTime = 3f;

        monsterData.MoveDirection = 1;

        monsterData.AggroRange = 6f;
        monsterData.SkillActiveRange = 3f;
        monsterData.SkillReadyTime = 2f;
        monsterData.SkillPower = 20f;
    }

    protected override IEnumerator UsingDash()
    {
        float dirX = Mathf.Sign(PlayerPosition.position.x - transform.position.x);

        rb.AddForce(new Vector2(dirX * monsterData.SkillPower, 0f), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        rb.linearVelocity = new Vector2(0f, 0f);

        yield return new WaitForSeconds(0.4f);

        ChangeState(MonsterStateType.Chase);
        StartCoroutine(SkillCool());
    }

    IEnumerator SkillCool()
    {
        isSkillReady = false;

        yield return new WaitForSeconds(SkillCoolTime);

        isSkillReady = true;
    }

    // ********* ÀÛ¾÷Áß ************
    //protected override MonsterSkillType SelectSkillType(MonsterSkillType skillType)
    //{
    //    if (!isSkillReady) return MonsterSkillType.None;

    //    if (DistanceToPlayer <= monsterData.SkillActiveRange)
    //        return MonsterSkillType.Skill_A;
    //}

    //protected override IEnumerator SkillA()
    //{
    //    float dirX = Mathf.Sign(PlayerPosition.position.x - transform.position.x);

    //    rb.AddForce(new Vector2(dirX * monsterData.SkillPower, 0f), ForceMode2D.Impulse);

    //    yield return new WaitForSeconds(0.2f);

    //    rb.linearVelocity = new Vector2(0f, 0f);

    //    yield return new WaitForSeconds(0.4f);

    //    ChangeState(MonsterStateType.Chase);
    //    StartCoroutine(SkillCool());
    //}
}
