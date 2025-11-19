using UnityEngine;

public static class SelectedCharacter
{
    //전역 현재 캐릭터
    static CharacterId curCharacter = CharacterId.세레나;

    public static CharacterId CurCharacter
    {
        get { return curCharacter; }
        set { curCharacter = value; }
    }
}
