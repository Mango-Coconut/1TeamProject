using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerStats : MonoBehaviour
{

    List<SoulData> souls = new List<SoulData>();
    public List<SoulData> Souls => souls;


    float damage = 10;
    public float Damage => GetDamage();
    float moveSpeed = 10;
    public float MoveSpeed => GetMoveSpeed();
    float jumpForce = 5;
    public float JumpForce => GetJumpForce();
    float attackCooldown = 1f;
    public float AttackCooldown => GetCooldown();

    float GetDamage()
    {
        float bonus = souls
            .Where(s => s.soulReinforceStatType == CharacterStats.Damage)
            .Sum(s => s.soulReinforceStatIncrease);

        return damage * (1f + bonus);
    }

    float GetMoveSpeed()
    {
        float bonus = souls
            .Where(s => s.soulReinforceStatType == CharacterStats.MoveSpeed)
            .Sum(s => s.soulReinforceStatIncrease);

        return moveSpeed * (1f + bonus);
    }
        
    float GetJumpForce()
    {
        return jumpForce;
    }

    float GetCooldown()
    {
        float bonus = souls
            .Where(s => s.soulReinforceStatType == CharacterStats.Cooldown)
            .Sum(s => s.soulReinforceStatIncrease);

        return attackCooldown * (1f - bonus);
    }

    public bool HasSoul(SoulData data){
        return souls.Contains(data);
    }
    public void EnrollSoul(SoulData data)
    {
        souls.Add(data);
    }
}
