using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    public Rigidbody2D Rigidbody2D;
    private bool isGrounded = true;

    void Start()
    {
        if (Rigidbody2D == null)
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; 
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        Rigidbody2D.linearVelocity = new Vector2(horizontalInput * moveSpeed, Rigidbody2D.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}