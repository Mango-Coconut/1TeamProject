using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class HaveSoulUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rect;
    Image soulImage;
    SoulData data;
    
    public event Action<Vector2, SoulData> MouseEntered;
    public event Action MouseExited;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        soulImage = GetComponent<Image>();
    }

    public void Bind(SoulData data)
    {
        this.data = data;
        soulImage.sprite = data.soulSprite  ;
    }

    public void OnPointerEnter(PointerEventData e)
    {
        MouseEntered?.Invoke(rect.anchoredPosition, data);
    }

    public void OnPointerExit(PointerEventData e)
    {
        MouseExited?.Invoke();
    }
}
