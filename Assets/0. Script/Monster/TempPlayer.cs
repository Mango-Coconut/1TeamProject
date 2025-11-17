using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    public float PlayerSpeed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        float InputX = Input.GetAxisRaw("Horizontal");

        Vector2 MoveDir = transform.right * InputX;
        Vector2 PlayerVel = MoveDir * PlayerSpeed;

        PlayerVel.y = rb.linearVelocityY;

        rb.linearVelocity = PlayerVel;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
