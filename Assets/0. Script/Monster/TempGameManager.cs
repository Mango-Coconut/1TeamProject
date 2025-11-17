using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TempGameManager : MonoBehaviour
{
    public static TempGameManager instance;

    public GameObject P;
    public int PlayerMaxHP = 3;
    public int PlayerHP;

    bool isInvincible = false;
    public float invincibleTime = 1.0f;

    void Awake()
    {
        // 싱글톤 세팅
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 넘어가도 유지하고 싶으면
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayerHP = PlayerMaxHP;
    }

    void Update()
    {

    }

    public void AttackDmg(int dmg)
    {
        if (isInvincible) return;

        PlayerHP -= dmg;
        PlayerHP = Mathf.Clamp(PlayerHP, 0, PlayerMaxHP);

        Debug.Log("Player HP : " + PlayerHP);

        if (PlayerHP <= 0)
        {
            PlayerDeath();
        }

        StartCoroutine(InvincibleRoutine());
    }

    private IEnumerator InvincibleRoutine()
    {
        isInvincible = true;

        // 나중에 여기서 깜빡이는 연출 같은 것도 넣을 수 있음

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
    }

    void PlayerDeath()
    {
        Debug.Log("GameOver");
    }
}
