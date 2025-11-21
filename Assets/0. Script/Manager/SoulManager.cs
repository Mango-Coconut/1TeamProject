using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class SoulManager : MonoBehaviour
{
    [SerializeField] Player player;

    // 데이터 원본. 
    List<SoulData> allSouls;

    // 인게임 인스턴스. 플레이 중에 보유되는 영성들
    List<SoulInstance> curSouls = new List<SoulInstance>();
    public List<SoulInstance> CurSouls => curSouls;

    void Awake()
    {// Resources/SoulDatas 폴더
        SoulData[] loaded = Resources.LoadAll<SoulData>("SoulDatas");

        allSouls = loaded.ToList();
    }

    public void EnrollSoul(SoulData data)
    {
        if (data == null) return;

        //1회용 영성일 경우 사용하고 끝
        if (data.isDisposable)
        {
            ApplySoulEffectsOnce(data);
            return;
        }

        SoulInstance inst = curSouls.Find(s => s.data == data);

        // 이미 보유한 영성이면 스택 쌓기
        if (inst != null)
        {
            // maxStack <= 0 → 무한 스택
            if (data.maxStack <= 0 || inst.stack < data.maxStack)
            {
                inst.stack++;
                //플레이어 스탯에게 스탯 변경 알림
                player.Stats.MakeDirty();
            }
            // 더 이상 쌓을 수 없으면 무시
            return;
        }

        // 처음 얻는 경우 그대로 추가
        curSouls.Add(new SoulInstance(data));
        player.Stats.MakeDirty();
    }
    void ApplySoulEffectsOnce(SoulData data)
    {
        if (data.effects == null) return;

        foreach (var effect in data.effects)
        {
            if (effect == null) continue;

            switch (effect.type)
            {
                case SoulEffectType.HealHP:
                    player.HP.Heal(effect.healAmount);
                    break;

                case SoulEffectType.LearnSkill:
                    //skillManager.UnlockSkill(effect.skillToLearn);
                    Debug.Log($"{effect.skillToLearn} skill 배움");
                    break;
            }
        }
    }

    #region 영성 뽑기
    // 이전에 뽑았던 영성을 제외하고 num 만큼 영성 뽑기
    public SoulData[] GetSouls(SoulData[] previousCandidates, int num)
    {
        if (num <= 0 || allSouls == null || allSouls.Count == 0)
        {
            Debug.Log($"GetSouls 오류: {num}, {allSouls == null}, {allSouls.Count == 0}");
            return Array.Empty<SoulData>();
        }

        // 0. 먼저 조건(캐릭터/레벨/기타) 만족하는 애들만 필터링
        List<SoulData> candidates = allSouls
            .Where(soul => soul != null && soul.CanOffer(player))
            .ToList();

        if (candidates.Count == 0)
        {
            Debug.Log("GetSouls: 조건을 만족하는 영성이 없음");
            return Array.Empty<SoulData>();
        }

        List<SoulData> result = new List<SoulData>(num);

        // 1. 직전에 나왔던 애들을 제외한 "새로운" 후보 목록 만들기
        List<SoulData> freshCandidates = new List<SoulData>();

        for (int i = 0; i < candidates.Count; i++)
        {
            SoulData soul = candidates[i];

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
            // result에 이미 들어간 것만 제외한 전체 후보(candidates)를 다시 후보로
            List<SoulData> fallbackCandidates = new List<SoulData>();

            for (int i = 0; i < candidates.Count; i++)
            {
                SoulData soul = candidates[i];

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

        return result.ToArray();
    }
    #endregion
}

