using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] SoulManager SM;
    void Awake()
    {
        if (SM == null) Debug.Log($"playerStats에 soulmanager 넣기");
    }

    // ===== Base Stats =====
    [SerializeField] float baseDamage = 10f;
    [SerializeField] float baseDefense = 1f;
    [SerializeField] float baseAttackSpeed = 1f;
    [SerializeField] float baseMoveSpeed = 10f;
    [SerializeField] float baseCooldown = 1f;
    [SerializeField] float baseLifeSteal = 1f;
    [SerializeField] float baseJumpForce = 5f;

    // ===== Cached Stats =====
    float cachedDamage;
    float cachedDefense;
    float cachedAttackSpeed;
    float cachedMoveSpeed;
    float cachedCooldown;
    float cachedLifeSteal;
    float cachedJumpForce;

    // ===== Dirty Flag =====
    bool statDirty = true;

    // ===== Public Properties =====
    public float Damage { get { UpdateStatsIfDirty(); return cachedDamage; } }
    public float Defense { get { UpdateStatsIfDirty(); return cachedDefense; } }
    public float AttackSpeed { get { UpdateStatsIfDirty(); return cachedAttackSpeed; } }
    public float MoveSpeed { get { UpdateStatsIfDirty(); return cachedMoveSpeed; } }
    public float SkillCooldown { get { UpdateStatsIfDirty(); return cachedCooldown; } }
    public float LifeSteal { get { UpdateStatsIfDirty(); return cachedLifeSteal; } }
    public float JumpForce { get { UpdateStatsIfDirty(); return cachedJumpForce; } }

    // =====================================
    //              Core Logic
    // =====================================

    //스탯이 변경되었다고 알려줌
    public void MakeDirty()
    {
        statDirty = true;
    }   

    //스탯이 변경되었을 경우 업데이트 함
    void UpdateStatsIfDirty()
    {
        if (!statDirty) return;
        RecalculateStats();
        statDirty = false;
    }

    void RecalculateStats()
    {
        cachedDamage = CalcFinalStat(baseDamage, PlayerStatType.Damage);
        cachedDefense = CalcFinalStat(baseDefense, PlayerStatType.Defense);
        cachedAttackSpeed = CalcFinalStat(baseAttackSpeed, PlayerStatType.AttackSpeed);
        cachedMoveSpeed = CalcFinalStat(baseMoveSpeed, PlayerStatType.MoveSpeed);
        cachedCooldown = CalcFinalStat(baseCooldown, PlayerStatType.SkillCooldown);
        cachedLifeSteal = CalcFinalStat(baseLifeSteal, PlayerStatType.LifeSteal);

        // 점프력은 소울 영향 안 받는다고 했으므로 그대로
        cachedJumpForce = baseJumpForce;
    }

    float CalcFinalStat(float baseStat, PlayerStatType statType)
    {
        if (SM == null || SM.CurSouls == null || SM.CurSouls.Count == 0)
            return baseStat;
        
        int bonusFlat = 0;
        int bonusPercent = 0;

        //모든 영성에 대해서 확인
        for (int i = 0; i < SM.CurSouls.Count; i++)
        {
            SoulInstance inst = SM.CurSouls[i];
            if (inst == null || inst.data == null || inst.data.effects == null)
                continue;

            //해당 영성의 모든 효과 확인
            SoulEffect[] effects = inst.data.effects;

            for (int j = 0; j < effects.Length; j++)
            {
                SoulEffect effect = effects[j];
                if (effect == null) continue;

                // 이 효과가 지금 계산 중인 스탯(statType)에 해당하는지
                if (effect.targetStat != statType)
                    continue;

                // 스택이 있다면, 스택 수만큼 곱해줌 (없으면 1로 생각)
                int stack = inst.stack > 0 ? inst.stack : 1;

                if (effect.type == SoulEffectType.StatFlat)
                {
                    bonusFlat += effect.flatValue * stack;
                }
                else if (effect.type == SoulEffectType.StatPercent)
                {
                    bonusPercent += effect.percentValue * stack;
                }
            }
        }

        return (baseStat + bonusFlat) * (1f + bonusPercent * 0.01f);
    }
}
