using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;              //Rigidbody2D型の変数
    float axisH = 0.0f;             //入力
    public float speed = 3.0f;      //移動速度

    public float jump = 9.0f;       //ジャンプ力
    public LayerMask groundLayer;      //着地できるレイヤー
    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //地面に立っているフラグ

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを持ってくる
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //水平方向のにゅうりょくをチェックする
        axisH = Input.GetAxisRaw("Horizontal");
        //向きの調整
        if (axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        } else if (axisH < 0.0f)
        {
            //左移動
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1); //左右反転させる 
        }
        //キャラをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump(); //ジャンプ
        }
    }

    void FixedUpdate()
    {
        //地上判定
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)
        {
            //地面の上or速度が0ではない
            //速度の更新
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            //地面の上でジャンプキーが押された
            //ジャンプさせる
            Debug.Log("ジャンプ!");
            Vector2 jumpPw = new Vector2(0, jump);          //ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //瞬間的な力を加える
            goJump = false; //ジャンプフラグを下ろす
        }
    }
    //ジャンプ
    public void Jump()
    {
        goJump = true;      //ジャンプフラグを立てる
        Debug.Log("ジャンプボタン押し!");
    }
}
