using UnityEngine;

public class IngameCharacterUI : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] HPBar hpBar;

    void Awake()
    {
        
    }

    void Start()
    {
        hpBar.Bind(player.HP);
    }
    
}
