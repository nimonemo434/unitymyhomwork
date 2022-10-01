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

        if (Input.GetButtonDown("Horizontal")) // 방향 전환
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
}
