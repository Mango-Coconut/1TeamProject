using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class HaveSoulsPanel : MonoBehaviour
{
    [SerializeField] HaveSoulTooltipUI tooltipUI;
    [SerializeField] PlayerStats stats;
    [SerializeField] HaveSoulUI prefabHaveSoulUI;
    List<HaveSoulUI> soulUIs = new List<HaveSoulUI>();

    void OnEnable()
    {
        HideTooltipUI();
        //이름순 정렬하기
        //foreach (SoulData soul in stats.Souls.OrderBy(soul => soul.soulName)) 
        //단순 획득순 정렬
        foreach (SoulData soul in stats.Souls)
        {
            HaveSoulUI haveSoulUI = Instantiate(prefabHaveSoulUI, transform);
            haveSoulUI.MouseEntered += ShowTooltipUI;
            haveSoulUI.MouseExited += HideTooltipUI;
            haveSoulUI.Bind(soul);
            soulUIs.Add(haveSoulUI);
        }
    }
    void OnDisable()
    {
        foreach (HaveSoulUI haveSoulUI in soulUIs)
        {
            haveSoulUI.MouseEntered -= ShowTooltipUI;
            haveSoulUI.MouseExited -= HideTooltipUI;
            Destroy(haveSoulUI.gameObject);
        }
        soulUIs.Clear();
        HideTooltipUI();
    }

    void ShowTooltipUI(Vector2 pos, SoulData data)
    {
        tooltipUI.gameObject.SetActive(true);
        tooltipUI.rect.anchoredPosition = pos;
        tooltipUI.Set(data);
    }
    void HideTooltipUI()
    {
        tooltipUI.gameObject.SetActive(false);
    }
}

