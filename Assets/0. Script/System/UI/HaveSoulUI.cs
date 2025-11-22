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
    [SerializeField] TMP_Text soulNum;
    SoulInstance inst;


    public event Action<RectTransform, SoulInstance> MouseEntered;
    public event Action MouseExited;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Bind(SoulInstance inst)
    {
        this.inst = inst;
        soulImage.sprite = inst.data.soulIcon;
        soulName.text = inst.data.displayName;
        soulNum.SetText("{0}", inst.stack);
    }

    public void OnPointerEnter(PointerEventData e)
    {
        // Debug.Log(
        //     $"ENTER {name} / " +
        //     $"pointerEnter={e.pointerEnter?.name} / " +
        //     $"currentRaycast={e.pointerCurrentRaycast.gameObject?.name}"
        // );
        MouseEntered?.Invoke(rect, inst);
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

