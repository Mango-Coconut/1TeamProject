using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] SoulManager soulManager;
    [SerializeField] SoulPanel soulPanel1;
    [SerializeField] SoulPanel soulPanel2;
    [SerializeField] TMP_Text rerollText;
    int remainRerollNum = 3;
    SoulPanel selectedSoulPanel;

    public event Action SelectSoulCompleted;

    //초기화
    void OnEnable()
    {
        soulPanel1.SoulMouseEntered += HandleMouseHoverSoul;
        soulPanel2.SoulMouseEntered += HandleMouseHoverSoul;
        soulPanel1.SoulMouseExited += HandleMouseExitSoul;
        soulPanel2.SoulMouseExited += HandleMouseExitSoul;
        soulPanel1.SoulMouseClicked += HandleSelectSoul;
        soulPanel2.SoulMouseClicked += HandleSelectSoul;
        remainRerollNum = 3;
        selectedSoulPanel = null;
        Reroll();
    }
    void OnDisable()
    {
        soulPanel1.SoulMouseEntered -= HandleMouseHoverSoul;
        soulPanel2.SoulMouseEntered -= HandleMouseHoverSoul;
        soulPanel1.SoulMouseExited -= HandleMouseExitSoul;
        soulPanel2.SoulMouseExited -= HandleMouseExitSoul;
        soulPanel1.SoulMouseClicked -= HandleSelectSoul;
        soulPanel2.SoulMouseClicked -= HandleSelectSoul;
    }

    public void Reroll()
    {
        if (remainRerollNum == 0)
        {
            return;
        }

        SoulData[] datas = soulManager.GetDoubleSoul();
        
        if(datas == null) return;
        else if(datas.Length == 2)
        {
            soulPanel1.Set(datas[0]);
            soulPanel2.Set(datas[1]);
        }
        else if(datas.Length == 1)
        {
            soulPanel1.Set(datas[0]);
            soulPanel2.gameObject.SetActive(false);
        }
        else
        {
            soulPanel1.gameObject.SetActive(false);
            soulPanel2.gameObject.SetActive(false);
        }
        Debug.Log(datas.Length);
        remainRerollNum--;
        RerollTextSet(remainRerollNum);
    }
    public void RerollTextSet(int remains)
    {
        if (remains == 0)
        {
            rerollText.color = Color.red;
        }
        else
        {
            rerollText.color = Color.black;
        }
        rerollText.SetText("{0}", remains);
    }

    //SoulPanel에 마우스 올리면 커짐
    void HandleMouseHoverSoul(SoulPanel panel)
    {
        //단, 이미 선택한 Panel이 있으면 취소
        if (selectedSoulPanel != null) return;
        panel.ExpandPanelScale();
    }

    //SoulPanel에서 마우스 내리면 원래대로 작아짐
    void HandleMouseExitSoul(SoulPanel panel)
    {
        //단, 이미 선택한 Panel이 있으면 취소
        if (selectedSoulPanel != null) return;
        panel.OriginPanelScale();
    }

    //SoulPanel을 클릭하면 해당 소울을 선택함
    void HandleSelectSoul(SoulPanel panel)
    {   
        //단, 이미 선택한 Panel이 있었으면 해당 Panel은 선택 취소
        if (selectedSoulPanel != null)
        {
            HandleDeSelectSoul();
        }

        selectedSoulPanel = panel;
        //이전에 선택된 Panel 때문에 못 커졌으니 지금이라도 커지게 함
        selectedSoulPanel.ExpandPanelScale();
    }

    //배경 화면을 누르면 선택한 Panel 선택 취소
    public void HandleDeSelectSoul()
    {
        if(selectedSoulPanel == null) return;

        selectedSoulPanel.OriginPanelScale();
        selectedSoulPanel = null;
    }

    //selectbutton에 연결된 이벤트. 선택한 Soul을 플레이어에게 등록
    public void EnrollSoul()
    {
        if (selectedSoulPanel == null) return;

        soulManager.EnrollSoulToPlayer(selectedSoulPanel.SoulData);
        SelectSoulCompleted?.Invoke();
    }
}

