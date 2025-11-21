[System.Serializable]
public class SoulInstance
{
    public SoulData data;  // SO 원본 참조
    public int stack;      // 이 소울의 현재 스택
    public bool consumed;  // 일회성일 때
}
