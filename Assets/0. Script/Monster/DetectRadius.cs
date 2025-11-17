using UnityEngine;

public class DetectRadius : MonoBehaviour
{
    public Monster monster;
    public EnemyStateType currentState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            monster.ChangeState(EnemyStateType.Chase);
        }
    }
}
