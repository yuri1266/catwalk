using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    private Rigidbody2D rbody2D;
    Animator cat_animator;

    private float jumpForce = 350f;

    private int jumpCount = 0;

    float jumpThreshold = 2.0f;    // ジャンプ中か判定するための閾値
    bool isGround = true;        // 地面と接地しているか管理するフラグ

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存
    

    void Start()
    {
    
        this.rbody2D = GetComponent<Rigidbody2D>();
        this.cat_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && this.jumpCount < 2)
        {
            this.rbody2D.AddForce(transform.up * jumpForce);
            jumpCount++;
            isGround = false;
        }

        ChangeState();          // ② 状態を変更する
        ChangeAnimation();      // ③ 状態に応じてアニメーションを変更する
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    void ChangeState()
    {
        // 空中にいるかどうかの判定。上下の速度(rigidbody.velocity)が一定の値を超えている場合、空中とみなす
        if (Mathf.Abs(rbody2D.velocity.y) > jumpThreshold)
        {
            isGround = false;
        }
 
        // 接地している場合
        if (isGround)
        {
            // 走行中
            state = "RUN";
        
            // 空中にいる場合
        }
        else
        {
            // 上昇中
            if (rbody2D.velocity.y > 0)
            {
                state = "JUMP";
            // 下降中
            }
            else if (rbody2D.velocity.y < 0)
            {
                state = "FALL";
            }
        }
    }
 
    void ChangeAnimation()
    {
        // 状態が変わった場合のみアニメーションを変更する
        if (prevState != state)
        {
            switch (state)
            {
                case "JUMP":
                    cat_animator.SetBool("jump_up", true);
                    cat_animator.SetBool("jump_down", false);
                    cat_animator.SetBool("run", false);
                
                    break;
                case "FALL":
                    cat_animator.SetBool("jump_down", true);
                    cat_animator.SetBool("jump_up", false);
                    cat_animator.SetBool("run", false);
                    
                    break;
                case "RUN":
                    cat_animator.SetBool("run", true);
                    cat_animator.SetBool("jump_down", false);
                    cat_animator.SetBool("jump_up", false);
                    
                    break;
            }
            // 状態の変更を判定するために状態を保存しておく
            prevState = state;
        }
    }

    //着地判定
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Debug.Log("ぶつかったよ！1");
            if (!isGround)
                isGround = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
       {
            if (!isGround)
                isGround = true;
        }
    }
}