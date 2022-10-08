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

        if (Input.GetButton("Horizontal")) // ���� ��ȯ
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

    private void OnCollisionEnter2D(Collision2D collision) // ���𰡿� �������
    {
        if (collision.gameObject.tag == "monster") // ���Ϳ� �������
        {
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y) //  ���� ���� ������ ��Ҵٸ� ����
            {
                onAttack(collision.transform);
            }
            else onDamaged(collision.transform.position); // �׿� ��� �������� �޴´�
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "item")
        {
            collision.gameObject.SetActive(false);
        }
    }

    void onAttack(Transform monster) // ����
    {
        rigid.AddForce(Vector2.up * 20, ForceMode2D.Impulse); // ������ Ƣ�����

        FrogMoving frogmove = monster.GetComponent<FrogMoving>(); // �� ��ũ��Ʈ �ʱ�ȭ
        frogmove.onDamaged(); // ���ݹ��� ���� �ڵ�
    }
    void onDamaged(Vector2 targetPos) // ������
    {
        gameObject.layer = 11; // �ٸ� ���̾�� ����

        spriteRenderer.color = new Color(1, 1, 1, 0.8f); // ����ȭ

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1; // ����
        rigid.AddForce(new Vector2(dirc, 1)*10, ForceMode2D.Impulse); // �з����� ��

        anim.SetTrigger("hurt"); // �ִϸ��̼� Ȱ��
        Invoke("offDamaged", 3); // �ʱ�ȭ �ڵ�
    }

    void offDamaged() // �ʱ�ȭ
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
