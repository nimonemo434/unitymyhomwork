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
        // ForceMode2D.Impulse : �������� ���� �ִ� �ڵ�
        // ����Ű�� �����鼭 ���� �ִϸ��̼��� Ȱ��ȭ ���� �ʾ�����
        if (Input.GetKeyDown(KeyCode.X))//&& !anim.GetBool("jumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("jumping", true);
        }
        // GetAxisRaw : -1, 0, 1 ������ ���� �ϳ��� ��ȯ
        // Horizontal : ���� �̵� ��ɾ�
        float h = Input.GetAxisRaw("Horizontal"); // Ű ����
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxspeed) // ������ �ְ� �ӷ�
            rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxspeed*(-1)) // ���� �ְ� �ӷ�
            rigid.velocity = new Vector2(maxspeed*(-1), rigid.velocity.y);

        if(Input.GetButtonUp("Horizontal")) // ������
        {
            // normalized : ������ �̵��� ���� ������ ����ȭ, ���⿡ ���� �̵� �ӵ� �յ�ȭ
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetButtonDown("Horizontal")) // ���� ��ȯ
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // run �ִϸ��̼� Ȱ��ȭ
        if (rigid.velocity.normalized.x == 0) anim.SetBool("runing", false);
        else anim.SetBool("runing", true);
        
        // jump �ִϸ��̼� ���ǹ�
        if(rigid.velocity.y < 0) // ĳ���Ͱ� ���� ������
        {
            Debug.DrawRay(rigid.position, Vector2.down*2, new Color(0, 1, 0)); // ���� ���� ����
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            // ���� ��ݿ� �Ʒ� �������� 1��ŭ "Platform" �� ����
            if (rayhit.collider != null) // �������� ������ ������
            {
                if (rayhit.distance < 1f) anim.SetBool("jumping", false);
                // �������� �Ÿ��� 1���� �۴ٸ� ���� �ִϸ��̼� ��Ȱ��ȭ
            }
        }       
    }
}
