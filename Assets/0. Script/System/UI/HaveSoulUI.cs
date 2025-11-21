using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class HaveSoulUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rect;
    [SerializeField] Image soulImage;
    [SerializeField] TMP_Text soulName;
    SoulData data;

    public event Action<RectTransform, SoulData> MouseEntered;
    public event Action MouseExited;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Bind(SoulData data)
    {
        this.data = data;
        soulImage.sprite = data.soulSprite;
        soulName.text = data.displayName;
    }

    public void OnPointerEnter(PointerEventData e)
    {
        // Debug.Log(
        //     $"ENTER {name} / " +
        //     $"pointerEnter={e.pointerEnter?.name} / " +
        //     $"currentRaycast={e.pointerCurrentRaycast.gameObject?.name}"
        // );
        MouseEntered?.Invoke(rect, data);
    }

    public void OnPointerExit(PointerEventData e)
    {
        // Debug.Log(
        //     $"EXIT {name} / " +
        //     $"pointerEnter={e.pointerEnter?.name} / " +
        //     $"currentRaycast={e.pointerCurrentRaycast.gameObject?.name}"
        // );
        MouseExited?.Invoke();
    }
}
