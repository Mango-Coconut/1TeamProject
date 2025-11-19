using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class SoulPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image soulImage;
    [SerializeField] TMP_Text soulName;
    [SerializeField] TMP_Text soulDescript;
    SoulData myData;

    public event Action<SoulData> SoulSelected;

    public void Set(SoulData data)
    {
        soulImage.sprite = data.soulSprite;
        soulName.text = data.soulName;
        soulDescript.text = data.soulDescript;
        myData = data;
    }
    public void Clear()
    {
        myData = null;
    }
    public void OnPointerEnter(PointerEventData e)
    {
        //애니메이션 적용
    }
    public void OnPointerExit(PointerEventData e)
    {
        //애니메이션 적용
    }

    public void OnPointerClick(PointerEventData e)
    {
        SoulSelected?.Invoke(myData);
    }
}