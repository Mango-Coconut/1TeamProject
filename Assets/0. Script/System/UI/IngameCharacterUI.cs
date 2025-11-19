using UnityEngine;

public class IngameCharacterUI : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] HPBar hpBar;
    [SerializeField] ExpBar expBar;

    void Awake()
    {
        
    }

    void Start()
    {
        hpBar.Bind(player.HP);
        expBar.Bind(player.Exp);
    }
    
}
