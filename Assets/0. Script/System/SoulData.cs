using UnityEngine;

[CreateAssetMenu(fileName = "SoulData", menuName = "Scriptable Objects/SoulData")]
public class SoulData : ScriptableObject
{


    [Header("영성 ID")]
    public int index;
    [Header("영성 그룹")]
    public int group;
    [Header("영성 이미지")]
    public Sprite soulSprite;
    [Header("영성 이름")]
    public string displayName; //게임내 표기할 이름(파일이름)
    [Header("영성 설명")]
    public string soulDescript;
    
    [Header("영성 효과")]
    public SoulEffect[] effects;
    
    [Header("캐릭터 제한")]
    public CharacterId soulCharacterType;

    [Header("기타 조건 제한")]
    public SoulNeedType soulNeedType;

    [Header("레벨 제한일 경우 몇 제한?")]
    public int levelConstrains = 0;

    [Header("중첩 가능 여부")]
    public bool isStackable = true;

    [Header("최대 중첩 개수")]
    public int maxStack = 9999;




    //디폴트 설정
    void OnValidate()
    {
        displayName = name;  // ← ScriptableObject의 파일 이름
    }

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
        //if (!isStackable && player.Stats.HasSoul(this))
        //    return false;

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
}