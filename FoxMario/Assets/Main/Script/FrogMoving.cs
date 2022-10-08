using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMoving : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D frogcollider;
    public int nextMove; // ���� ������
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        frogcollider = GetComponent<BoxCollider2D>();

        Invoke("Think", 5); // Think �Լ� 5�� ������
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // �� ���� üũ �ڵ�
        Vector2 frontvec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontvec, Vector2.down * 2, new Color(0, 1, 0)); // ���� ���� ����
        RaycastHit2D rayhit = Physics2D.Raycast(frontvec, Vector2.down, 1, LayerMask.GetMask("Platform"));
        // ���� ��ݿ� �Ʒ� �������� 1��ŭ "Platform" �� ����
        if (rayhit.collider == null) Turn(); // �������� ������ ������
    }

    void Think() // ����Լ� : �ڽ��� ������ ȣ���ϴ� �Լ� - ���� ����!!
    {
        nextMove = Random.Range(-1, 2);

        anim.SetInteger("frog_jump", nextMove); // ������ ���ڿ� ���� Ȱ��

        if (nextMove != 0) spriteRenderer.flipX = nextMove == 1;

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime); // ������ ���ڸ�ŭ ����Լ�
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke(); // Invoke �۵�����
        Invoke("Think", 5); // ��Ȱ��ȭ
    }

    public void onDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.8f); // ����ȭ

        spriteRenderer.flipY = true; // �������ִ� �Լ�

        frogcollider.enabled = false; // collider �� Ȱ��

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse); // ��¦ ���� �� �߶� �ӵ�

        Invoke("DeActive", 5); // 5�� �� �����
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
