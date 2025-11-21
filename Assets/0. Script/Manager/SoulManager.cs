using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class SoulManager : MonoBehaviour
{
    [SerializeField] Player player;

    // 데이터 원본. 여기다가 모든 SO들 전부 끌어넣기
    [SerializeField] List<SoulData> allSouls;

    // 인게임 인스턴스. 플레이 중에 보유되는 영성들
    List<SoulInstance> curSouls = new List<SoulInstance>();
    public List<SoulInstance> CurSouls => curSouls;

    public void EnrollSoul(SoulData data)
    {
        //CurSouls.Add(data);
    }

    //이전에 뽑았던 영성을 제외하고 num 만큼 영성 뽑기
    public SoulData[] GetSouls(SoulData[] previousCandidates, int num)
    {
        if (num <= 0 || allSouls == null || allSouls.Count == 0)
        {
            Debug.Log("Getsouls 오류");
            return Array.Empty<SoulData>();
        }

        List<SoulData> result = new List<SoulData>(num);

        // 1. 직전에 나왔던 애들을 제외한 "새로운" 후보 목록 만들기
        List<SoulData> freshCandidates = new List<SoulData>();

        for (int i = 0; i < allSouls.Count; i++)
        {
            SoulData soul = allSouls[i];

            bool wasInPrevious = false;
            if (previousCandidates != null)
            {
                for (int j = 0; j < previousCandidates.Length; j++)
                {
                    if (previousCandidates[j] == soul)
                    {
                        wasInPrevious = true;
                        break;
                    }
                }
            }

            if (!wasInPrevious)
            {
                freshCandidates.Add(soul);
            }
        }

        // 2. freshCandidates에서 먼저 뽑기 (중복 없이 랜덤)
        int need = num;
        int takeFromFresh = Mathf.Min(need, freshCandidates.Count);

        for (int i = 0; i < takeFromFresh; i++)
        {
            int index = UnityEngine.Random.Range(0, freshCandidates.Count);
            SoulData chosen = freshCandidates[index];
            result.Add(chosen);
            freshCandidates.RemoveAt(index);   // 중복 방지
        }

        need = num - result.Count;

        // 3. 아직 더 필요하면, "이제는 기존에 나왔던 것도 포함해서" 뽑기
        if (need > 0)
        {
            // result에 이미 들어간 것만 제외한 전체 리스트를 다시 후보로
            List<SoulData> fallbackCandidates = new List<SoulData>();

            for (int i = 0; i < allSouls.Count; i++)
            {
                SoulData soul = allSouls[i];

                bool alreadyInResult = false;
                for (int j = 0; j < result.Count; j++)
                {
                    if (result[j] == soul)
                    {
                        alreadyInResult = true;
                        break;
                    }
                }

                if (!alreadyInResult)
                {
                    fallbackCandidates.Add(soul);
                }
            }

            int takeFromFallback = Mathf.Min(need, fallbackCandidates.Count);

            for (int i = 0; i < takeFromFallback; i++)
            {
                int index = UnityEngine.Random.Range(0, fallbackCandidates.Count);
                SoulData chosen = fallbackCandidates[index];
                result.Add(chosen);
                fallbackCandidates.RemoveAt(index);
            }
        }

        // 4. 그래도 부족하면 있는 만큼만 반환 (패널은 너 코드대로 꺼짐)
        return result.ToArray();
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
        return candidates[UnityEngine.Random.Range(0, candidates.Count)];
    }
    // 증강으로만 스탯, 스킬 강화
}
