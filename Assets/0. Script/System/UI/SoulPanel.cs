using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulPanel : MonoBehaviour
{
    [SerializeField] Image soulImage;
    [SerializeField] TMP_Text soulName;
    [SerializeField] TMP_Text soulDescript;

    public void Set(Sprite img, string name, string desc){
        soulImage.sprite = img;
        soulName.text = name;
        soulDescript.text = desc;
    }
}