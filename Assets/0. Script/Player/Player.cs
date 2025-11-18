using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    HP hp;
    //public float exp;
    public float jumpForce = 5;
    public float moveSpeed = 10;
    Rigidbody2D Rigidbody2D;
    public bool isGrounded = true;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if ((go.CompareTag("Ground") || go.CompareTag("Wall")) && !isGrounded)
        {
            isGrounded = true;
        }
        IAttackable attacker = collision.collider.GetComponent<IAttackable>();
        if (attacker != null)
        {
            hp.TakeDamage(attacker.Damage);
        }
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        float horizontalMovement = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMovement = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMovement = -1f;
        }
        Rigidbody2D.linearVelocity = new Vector2(horizontalMovement * moveSpeed, Rigidbody2D.linearVelocity.y);
    }
}