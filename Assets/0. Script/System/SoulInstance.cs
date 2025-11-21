[System.Serializable]
public class SoulInstance
{
    public SoulInstance(SoulData data)
    {
        this.data = data;
    }
    public SoulData data;  // SO 원본 참조
    public int stack = 1;      // 이 소울의 현재 스택
}
