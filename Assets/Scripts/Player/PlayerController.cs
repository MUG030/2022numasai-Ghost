using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;              //Rigidbody2D型の変数
    float axisH = 0.0f;             //入力
    public float speed = 3.0f;      //移動速度

    public float jump = 9.0f;       //ジャンプ力
    public LayerMask groundLayer;   //着地できるレイヤー
    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //地面に立っているフラグ

    //ダメージ対応
    public static int hp = 5;       //プレイヤーのhp
    public int Gethp()              //取得関数
    {
        return hp;
    }
    public static string gameState; //ゲームの状態
    bool inDamage = false;          //ダメージ中のフラグ
    //　LifeGaugeスクリプト
    [SerializeField]
    private HeartIndicator lifeGauge;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを持ってくる
        rbody = this.GetComponent<Rigidbody2D>();
        //ゲームの状態をプレイ中にする
        gameState = "playing";
        //　体力ゲージに反映
        lifeGauge.SetLifeGauge(hp);
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム中以外とダメージ中は何もしない
        if (gameState != "playing" || inDamage)
        {
            return;
        }

        //水平方向のにゅうりょくをチェックする
        axisH = Input.GetAxisRaw("Horizontal");
        //向きの調整
        if (axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            //左移動
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1); //左右反転させる 
        }
        //キャラをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();     //ジャンプ
        }
    }

    void FixedUpdate()
    {
        //ゲーム中は何もしない
        if (gameState != "playing")
        {
            return;
        }
        if (inDamage)
        {
            //ダメージ中は点滅する
            float val = Mathf.Sin(Time.time * 50);
            Debug.Log(val);
            if (val > 0)
            {
                //スプライトを表示
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //スプライトを非表示
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;     //ダメージ中は操作による影響を受けない
        }
        //地上判定
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.9f), groundLayer);

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

    //接触判定
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Clear")//Clearのタグが付くオブジェクトに接触したらクリアシーンへの切り替え
        {
            SceneManager.LoadScene("ClearScene");
            Debug.Log("Touch Goal");
        }

        if (col.gameObject.tag == "Enemy")//Clearのタグが付くオブジェクトに接触したらクリアシーンへの切り替え
        {
            Debug.Log("Hit Enemy");

            // ダメージ中は処理スキップ
            if (inDamage)
            {
                return;
            }

            hp--;       //HPを減らす

            if (hp >= 0)
            {
                //移動停止
                rbody.velocity = new Vector2(0, 0);
                //敵キャラの反対方向にヒットバックさせる
                if (transform.localScale.x >= 0)
                {
                    this.rbody.AddForce(transform.right * -100.0f);
                } 
                else
                {
                    this.rbody.AddForce(transform.right * 100.0f);
                }

                lifeGauge.SetLifeGauge(hp);
                lifeGauge.SetLifeGauge2(hp);

                //ダメージフラグON
                inDamage = true;
                // コルーチン開始
                StartCoroutine("WaitForIt");
                Invoke("DamageEnd", 1.0f);

                
            }
            else
            {
                //ゲームオーバー
                GameOver();
            }

        }

    }

    IEnumerable WaitForIt()
    {
        yield return new WaitForSeconds(10);
    }

    //ダメージ終了
    void DamageEnd()
    {
        //ダメージフラグOFF
        inDamage = false;
        //スプライトを元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    //ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー");
        //gameState = "gameover";

    }
}
