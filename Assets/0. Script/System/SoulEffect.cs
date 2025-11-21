[System.Serializable]
public class SoulEffect
{
    public SoulEffectType type;

    public PlayerStatType targetStat;   // Stat 관련일 때만 사용
    public int flatValue;
    public int percentValue;

    public CharacterSkills skillToLearn; // 스킬일 때만 사용

    public int healAmount;              // 체력 회복일 때만 사용
}