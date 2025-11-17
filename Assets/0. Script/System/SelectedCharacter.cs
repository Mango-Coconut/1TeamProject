using UnityEngine;

public enum CharacterId
{
    None = 0,
    Serena = 1,
    Char2 = 2,
    Char3 = 3
    // 필요한 만큼 추가
}

public static class SelectedCharacter
{
    static CharacterId curCharacter = CharacterId.None;

    public static CharacterId CurCharacter
    {
        get { return curCharacter; }
        set { curCharacter = value; }
    }
}
