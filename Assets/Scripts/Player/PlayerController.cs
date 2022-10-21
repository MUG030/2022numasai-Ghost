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
    public LayerMask waterLayer;   //着地できるレイヤー
    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //地面に立っているフラグ
    bool onWater = false;          //地面に立っているフラグ
    bool isAttacking = false;       // 攻撃モーションのフラグ

    //アニメーション対応
    Animator animator;  //アニメーター
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string attackAnime = "PlayerAttack";
    public string jumpAnime = "PlayerJump";
    public string damageAnime = "PlayerDamage";
    string nowAnime = "";
    string oldAnime = "";
    public static int actState = 0;

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

    //イベント用
    public bool controlEnabled {get; set; } = true; //操作有効無効Bool値

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを持ってくる
        rbody = this.GetComponent<Rigidbody2D>();
        //Animatorを持ってくる
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;   
        oldAnime = stopAnime;
        //ゲームの状態をプレイ中にする
        gameState = "playing";
        //　体力ゲージに反映
        lifeGauge.SetLifeGauge(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlEnabled)//ストーリーイベント用
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
                //Debug.Log("右移動");
                actState = 1;
                transform.localScale = new Vector2(1, 1);
            }
            else if (axisH < 0.0f)
            {
                //左移動
                //Debug.Log("左移動");
                actState = 1;
                transform.localScale = new Vector2(-1, 1); //左右反転させる 
            }
            //キャラをジャンプさせる
            if (Input.GetButtonDown("Jump"))
            {
                Jump();     //ジャンプ
            }

            Attack();
        }
    }

    void FixedUpdate()
    {

        // 攻撃アニメ再生中は、以下の処理しない　　　　　　　　　　　　　
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
        {
            return;
        }

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
        onWater = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.9f), waterLayer);

        if (onGround || axisH != 0 && !onWater)
        {
            //地面の上or速度が0ではない、の二つを満たしていてかつ水面ではない
            //速度の更新
            speed = 3.0f;
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        else if (onWater || axisH != 0)
        {
            //水面での速度の更新
            speed = 1.7f;
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }

        if (onGround && goJump)
        {
            //地面の上でジャンプキーが押された
            //ジャンプさせる
            //Debug.Log("ジャンプ!");
            Vector2 jumpPw = new Vector2(0, jump);          //ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //瞬間的な力を加える
            goJump = false; //ジャンプフラグを下ろす
        }
        else if (onWater && goJump)
        {
            //水面でジャンプキーが押された
            //ジャンプさせる
            Vector2 jumpPw = new Vector2(0, jump);          //ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //瞬間的な力を加える
            goJump = false; //ジャンプフラグを下ろす
        }

        //停止と移動と攻撃のアニメーション
        if (onGround)
        {
            //地面の上
            if (actState == 0)
            {
                nowAnime = stopAnime;   //停止中
            }
            else if (actState == 1)
            {
                nowAnime = moveAnime;   //移動中
                actState = 0;
            }
        }
        else　                          //空中
        {
            nowAnime = jumpAnime;        
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    // アニメーション再生
        }

        

    }

    public void Attack()
    {
        //攻撃アニメーション時(Qキーを押すと攻撃開始)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //攻撃中
            animator.Play(attackAnime);
            //移動停止
            rbody.velocity = new Vector2(0, 0);
        }
    }

    //ジャンプ
    public void Jump()
    {
        goJump = true;      //ジャンプフラグを立てる
        //Debug.Log("ジャンプボタン押し!");
    }

    // 攻撃アニメーション終了関数
    IEnumerable endMotionAnime()
    {
        yield return new WaitForSeconds(1);
        actState = 0;
    }

    //接触判定
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Clear")//Clearのタグが付くオブジェクトに接触したらクリアシーンへの切り替え
        {
            //Debug.Log("Touch Goal");
            hp = 5;
            SceneManager.LoadScene("ClearScene");
        }

        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log("Hit Enemy");
            //ダメージアニメーション

            // 攻撃アニメ再生中は、以下の処理しない(無敵判定)
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
            {
                return;
            }

            animator.Play(damageAnime);

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
