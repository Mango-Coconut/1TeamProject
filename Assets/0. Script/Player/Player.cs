using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    HP hp;
    public HP HP => hp;
    Rigidbody2D rb; // 변수 선언은 소문자로 시작. 단 rigidbody2D같은 일부 예약어는 사용 불가해서 rb로 바꿈
    public float jumpForce = 5;
    public float moveSpeed = 10;
    public bool isGrounded = true;

    private void Awake()
    {
        hp = GetComponent<HP>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    void Update()
    {
        //개발자 도구
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hp.Heal(20);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hp.TakeDamage(20);
        }

        Attack(); //함수 구현하기
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        //수평 벽은 Ground, 수직 벽은 Wall로 일단 했음
        //일단은 벽타기도 가능하게 함
        if ((go.CompareTag("Ground") || go.CompareTag("Wall")) && !isGrounded)
        {
            isGrounded = true;
        }
        //부딪힌 게임오브젝트에서 IAttackable을 찾음
        //적 몸체, 투사체, 함정 등 종류 상관없이 이 한줄의 코드로 정리 
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
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
    }

    void Attack()
    {

    }
}