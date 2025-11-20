using UnityEngine;
using TMPro;

public class HaveSoulTooltipUI : MonoBehaviour
{
    public RectTransform rect;
    [SerializeField] TMP_Text soulName;
    [SerializeField] TMP_Text soulDescript;
    
    public void Set(SoulData data){
        soulName.text = data.soulName;
        soulDescript.text = data.soulDescript;
    }
}
