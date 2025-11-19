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
    SoulData selectedSoul;

    public event Action SelectSoulCompleted;

    //초기화
    void OnEnable()
    {
        soulPanel1.SoulSelected += SelectSoul;
        soulPanel2.SoulSelected += SelectSoul;
        remainRerollNum = 3;
        selectedSoul = null;
        Reroll();
    }
    void OnDisable()
    {
        soulPanel1.SoulSelected -= SelectSoul;
        soulPanel2.SoulSelected -= SelectSoul;
    }

    public void Reroll()
    {
        SoulData[] datas = soulManager.GetDoubleSoul();
        soulPanel1.Set(datas[0]);
        soulPanel2.Set(datas[1]);
        remainRerollNum--;
        rerollText.SetText("{0}", remainRerollNum);
    }
    void SelectSoul(SoulData data)
    {
        selectedSoul = data;
        Debug.Log($"{data.soulName} 선택 됨");
    }
    //selectbutton 연결 이벤트
    public void EnrollSoul()
    {
        if(selectedSoul == null){
            Debug.Log("소울이 선택 되지 않은 버그");
            return;
        }
        soulManager.EnrollSoulToPlayer(selectedSoul);
        SelectSoulCompleted?.Invoke();
    }
}

