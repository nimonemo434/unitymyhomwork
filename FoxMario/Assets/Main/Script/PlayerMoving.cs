using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float maxspeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ForceMode2D.Impulse : 순간적인 힘을 주는 코드
        // 점프키를 누르면서 점프 애니메이션이 활성화 되지 않았을때
        if (Input.GetKeyDown(KeyCode.X))//&& !anim.GetBool("jumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("jumping", true);
        }
        // GetAxisRaw : -1, 0, 1 세가지 값중 하나가 반환
        // Horizontal : 수평 이동 명령어
        float h = Input.GetAxisRaw("Horizontal"); // 키 조작
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxspeed) // 오른쪽 최고 속력
            rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxspeed*(-1)) // 왼쪽 최고 속력
            rigid.velocity = new Vector2(maxspeed*(-1), rigid.velocity.y);

        if(Input.GetButtonUp("Horizontal")) // 급정지
        {
            // normalized : 균일한 이동을 위한 벡터의 정규화, 방향에 따른 이동 속도 균등화
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetButton("Horizontal")) // 방향 전환
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // run 애니메이션 활성화
        if (rigid.velocity.normalized.x == 0) anim.SetBool("runing", false);
        else anim.SetBool("runing", true);
        
        // jump 애니메이션 조건문
        if(rigid.velocity.y < 0) // 캐릭터가 위에 있을때
        {
            Debug.DrawRay(rigid.position, Vector2.down*2, new Color(0, 1, 0)); // 감지 막대 생성
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            // 물리 기반에 아래 방향으로 1만큼 "Platform" 을 감지
            if (rayhit.collider != null) // 물리적인 감지가 있을때
            {
                if (rayhit.distance < 1f) anim.SetBool("jumping", false);
                // 물리적인 거리가 1보다 작다면 점프 애니메이션 비활성화
            }
        }       
    }

    private void OnCollisionEnter2D(Collision2D collision) // 무언가와 닿았을때
    {
        if (collision.gameObject.tag == "monster") // 몬스터와 닿았을때
        {
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y) //  몬스터 보다 위에서 닿았다면 공격
            {
                onAttack(collision.transform);
            }
            else onDamaged(collision.transform.position); // 그외 경우 데미지를 받는다
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "item")
        {
            collision.gameObject.SetActive(false);
        }
    }

    void onAttack(Transform monster) // 공격
    {
        rigid.AddForce(Vector2.up * 20, ForceMode2D.Impulse); // 공격후 튀어오름

        FrogMoving frogmove = monster.GetComponent<FrogMoving>(); // 적 스크립트 초기화
        frogmove.onDamaged(); // 공격받은 적의 코드
    }
    void onDamaged(Vector2 targetPos) // 데미지
    {
        gameObject.layer = 11; // 다른 레이어로 변경

        spriteRenderer.color = new Color(1, 1, 1, 0.8f); // 투명화

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1; // 방향
        rigid.AddForce(new Vector2(dirc, 1)*10, ForceMode2D.Impulse); // 밀려나는 힘

        anim.SetTrigger("hurt"); // 애니메이션 활성
        Invoke("offDamaged", 3); // 초기화 코드
    }

    void offDamaged() // 초기화
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
