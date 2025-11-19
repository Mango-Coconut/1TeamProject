using UnityEngine;

[CreateAssetMenu(fileName = "SoulData", menuName = "Scriptable Objects/SoulData")]
public class SoulData : ScriptableObject
{

    public bool CanOffer(Player player)
    {
        // 1) 캐릭터 타입 제한
        if (soulCharacterType != CharacterId.None &&
            soulCharacterType != SelectedCharacter.CurCharacter)
            return false;

        // 2) 필요 조건 체크 (레벨, 특정 스킬, 특정 아이템…)
        if (!CheckNeedCondition(player))
            return false;

        // 3) 중복 Soul 제한
        if (!isStackable && player.Stats.HasSoul(this))
            return false;

        return true;
    }

    private bool CheckNeedCondition(Player player)
    {
        switch (soulNeedType)
        {
            case SoulNeedType.None:
                return true;

            case SoulNeedType.LevelNeed:
                return player.Exp.CurLevel >= levelConstrains;

            //case SoulNeedType.MustHaveSkill:
            //    return player.HasSkill(requiredSkill);

                // 필요한 조건 계속 확장하면 됨
        }

        return true;
    }

    public Sprite soulSprite;
    public string soulName;
    public string soulDescript;

    public bool isStackable;

    [Header("스탯 or 스킬 강화")]
    public SoulReinforceType soulReinforceType;

    [Header("스탯이라면 어떤 스탯을?")]
    public CharacterStats soulReinforceStatType;

    [Header("얼마나 강화? (float단위)")]
    public float soulReinforceStatIncrease;

    [Header("스킬이라면 무슨 스킬 획득?")]
    public CharacterSkills soulReinforceSkill;

    [Header("캐릭터 제한")]
    public CharacterId soulCharacterType;

    [Header("기타 조건 제한")]
    public SoulNeedType soulNeedType;
    
    [Header("레벨 제한일 경우 몇 제한?")]
    public int levelConstrains = 0;
}