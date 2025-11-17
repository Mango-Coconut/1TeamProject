using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHP;
    public float curHP;
    //public float exp;
    public float jumpForce;
    public float moveSpeed;
    public Rigidbody2D Rigidbody2D;
    private bool isGrounded = true;

    public event Action<float> ChangedHP;
    //public event Action<float> ChangedExp;

    void Start()
    {
        if (Rigidbody2D == null)
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        Move();
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
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

    public void ChangeHP(float value)
    {
        curHP += value;
        
        ChangedHP?.Invoke(curHP);
    }

}