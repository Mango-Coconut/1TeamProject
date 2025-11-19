using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SoulManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] List<SoulData> allSouls;  // 여기다가 SO들 전부 끌어넣기

    public SoulData[] GetDoubleSoul()
    {
        List<SoulData> chosen = new List<SoulData>();

        // 첫 번째 Soul
        var first = MakeRandomSoul();
        if (first != null)
            chosen.Add(first);

        // 두 번째 Soul: 첫 번째를 제외하고 뽑기
        var second = MakeRandomSoul(chosen);
        if (second != null)
            chosen.Add(second);

        return chosen.ToArray();
    }

    public SoulData MakeRandomSoul(List<SoulData> exclude = null)
    {
        // 조건에 따라 뽑을 수 있는 후보 리스트
        var candidates = allSouls
            .Where(soul => soul.CanOffer(player))
            .ToList();

        // 제외해야 할 Soul 제거
        if (exclude != null)
            candidates.RemoveAll(soul => exclude.Contains(soul));

        if (candidates.Count == 0)
            return null;

        // 랜덤 뽑기
        return candidates[Random.Range(0, candidates.Count)];
    }
    // 증강으로만 스탯, 스킬 강화
}
