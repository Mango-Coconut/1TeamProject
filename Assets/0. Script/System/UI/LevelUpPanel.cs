using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.PackageManager.Requests;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] SoulManager soulManager;
    [SerializeField] SoulPanel[] soulPanels;
    int selectIndex = -1;
    [SerializeField] TMP_Text rerollText;
    [SerializeField] int RerollNum = 2;
    int remainRerollNum;
    SoulPanel selectedSoulPanel;

    public event Action SelectSoulCompleted;

    void Awake()
    {
        soulPanels = GetComponentsInChildren<SoulPanel>();
    }

    public void Initialize()
    {
        remainRerollNum = RerollNum+1;
        candidates = null;
    }

    void OnEnable()
    {
        SubscribeChildEvent();
        Initialize();
    }
    void OnDisable()
    {
        UnSubscribeChildEvent();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectIndex == -1)
            {
                HandleSelectSoul(soulPanels[0]);
            }
            else
            {
                //selectIndex - 1이 0보다 작으면 0으로 만들기
                selectIndex = --selectIndex < 0 ? 0 : selectIndex;
                HandleSelectSoul(soulPanels[selectIndex]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int max = soulPanels.Length - 1;
            if (selectIndex == -1)
            {
                HandleSelectSoul(soulPanels[max]);
            }
            else
            {
                //selectIndex + 1이 length-1보다 크면 length-1으로 만들기
                selectIndex = ++selectIndex > max ? max : selectIndex;
                HandleSelectSoul(soulPanels[selectIndex]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Reroll();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            EnrollSoul();
        }
    }

    SoulData[] candidates;
    public void Reroll()
    {
        if (remainRerollNum == 0) return;
        
        selectIndex = -1;
        HandleDeSelectSoul();

        candidates = soulManager.GetSouls(candidates, soulPanels.Length);

        if (candidates == null) Debug.Log("뽑을 수 있는 영성이 없음");
        else
        {
            int i = 0;
            //반환된 소울 데이터의 길이 만큼 패널 세팅
            for (; i < candidates.Length; i++)
            {
                soulPanels[i].gameObject.SetActive(true);
                soulPanels[i].Set(candidates[i]);
            }
            //얻을 수 있는 소울이 부족하면 남는 패널은 끄기
            for (; i < soulPanels.Length; i++)
            {
                soulPanels[i].gameObject.SetActive(false);
            }
        }

        remainRerollNum--;

        //남은 리롤 횟수에 따른 세팅
        //if (remainRerollNum == 0) rerollText.color = Color.red;
        //else rerollText.color = Color.white;
        rerollText.SetText("{0}", remainRerollNum);
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

    //선택한 Panel 선택 취소
    public void HandleDeSelectSoul()
    {
        if (selectedSoulPanel == null) return;

        selectedSoulPanel.OriginPanelScale();
        selectedSoulPanel = null;
    }

    //선택한 Soul을 플레이어에게 등록
    public void EnrollSoul()
    {
        if (selectedSoulPanel == null) return;

        soulManager.EnrollSoul(selectedSoulPanel.SoulData);
        SelectSoulCompleted?.Invoke();
    }

    #region 이벤트 구독
    void SubscribeChildEvent()
    {
        UnSubscribeChildEvent();
        foreach (SoulPanel soulPanel in soulPanels)
        {
            soulPanel.SoulMouseEntered += HandleMouseHoverSoul;
            soulPanel.SoulMouseExited += HandleMouseExitSoul;
            soulPanel.SoulMouseClicked += HandleSelectSoul;
        }
    }
    void UnSubscribeChildEvent()
    {
        foreach (SoulPanel soulPanel in soulPanels)
        {
            soulPanel.SoulMouseEntered -= HandleMouseHoverSoul;
            soulPanel.SoulMouseExited -= HandleMouseExitSoul;
            soulPanel.SoulMouseClicked -= HandleSelectSoul;
        }
    }
    #endregion
}

