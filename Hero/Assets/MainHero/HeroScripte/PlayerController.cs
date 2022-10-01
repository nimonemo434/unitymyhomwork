using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 20.0f;
    float maxwalkSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !animator.GetBool("JumpTrigger"))
        {
            rigid2D.AddForce(transform.up * jumpForce);
            animator.SetBool("JumpTrigger", true);
            //animator.SetBool("PlayerWalking", false);
        }
        else if(rigid2D.velocity.y == 0) animator.SetBool("JumpTrigger", false);

        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("PlayerWalking", true);
            //if (Input.GetKeyDown(KeyCode.X)) animator.SetBool("PlayerWalking", false);
            key = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("PlayerWalking", true);
            //if (Input.GetKeyDown(KeyCode.X)) animator.SetBool("PlayerWalking", false);
            key = -1;
        }
        else animator.SetBool("PlayerWalking", false);
        //플레이어 속도 체크
        float speedx = Mathf.Abs(rigid2D.velocity.x);
        //스피드 제한
        if (speedx < maxwalkSpeed)
        {
            rigid2D.AddForce(transform.right * key * this.walkForce);
        }
        //움직이는 방향 전환
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        if (rigid2D.velocity.y == 0)
        {
            //플레이어 속도에 맞춰 애니메이션 속도 변경
            animator.speed = speedx / 2.0f;
        }
        else
        {
            animator.speed = 1.0f;
        }

        Debug.DrawRay(rigid2D.position, Vector2.down, new Color(0, 1, 0));

        if(rigid2D.velocity.y < 0)
        {
            RaycastHit2D rayhit = Physics2D.Raycast(rigid2D.position, Vector2.down, 1);
            if (rayhit.collider != null)
            {
                if (rayhit.distance < 0.5f)
                    animator.SetBool("JumpTrigger", false);
            }
        }

    }
}
