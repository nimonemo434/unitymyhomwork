using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMoving : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D frogcollider;
    public int nextMove; // 다음 움직임
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        frogcollider = GetComponent<BoxCollider2D>();

        Invoke("Think", 5); // Think 함수 5초 딜레이
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 앞 지형 체크 코드
        Vector2 frontvec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontvec, Vector2.down * 2, new Color(0, 1, 0)); // 감지 막대 생성
        RaycastHit2D rayhit = Physics2D.Raycast(frontvec, Vector2.down, 1, LayerMask.GetMask("Platform"));
        // 물리 기반에 아래 방향으로 1만큼 "Platform" 을 감지
        if (rayhit.collider == null) Turn(); // 물리적인 감지가 있을때
    }

    void Think() // 재귀함수 : 자신을 스스로 호출하는 함수 - 아주 위험!!
    {
        nextMove = Random.Range(-1, 2);

        anim.SetInteger("frog_jump", nextMove); // 랜덤한 숫자에 따라 활성

        if (nextMove != 0) spriteRenderer.flipX = nextMove == 1;

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime); // 랜덤한 숫자만큼 재귀함수
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke(); // Invoke 작동중지
        Invoke("Think", 5); // 재활성화
    }

    public void onDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.8f); // 투명화

        spriteRenderer.flipY = true; // 뒤집어주는 함수

        frogcollider.enabled = false; // collider 비 활성

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse); // 살짝 점프 후 추락 속도

        Invoke("DeActive", 5); // 5초 후 사라짐
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
