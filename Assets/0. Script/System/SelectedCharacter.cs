using UnityEngine;

public enum CharacterId
{
    None = 0,
    Serena = 1,
    temp2 = 2,
    temp3 = 3
    // 필요한 만큼 추가
}

public static class SelectedCharacter
{
    static CharacterId current = CharacterId.None;

    public static CharacterId Current
    {
        get { return current; }
        set { current = value; }
    }
}
