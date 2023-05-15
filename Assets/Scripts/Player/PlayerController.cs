using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;    //instance化
    Rigidbody2D rbody;              //Rigidbody2D型の変数
    public float axisH = 0.0f;             //入力
    public float speed = 3.0f;      //移動速度

    public float jump = 9.0f;       //ジャンプ力
    public LayerMask groundLayer;   //着地できるレイヤー
    public LayerMask waterLayer;   //着地できるレイヤー
    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //地面に立っているフラグ
    bool onWater = false;          //地面に立っているフラグ
    bool isAttacking = false;       // 攻撃モーションのフラグ

    [SerializeField]
    UnityEngine.UI.Image Backimg; // ワープ時のフェード用画像

    bool warpFlag = false; // ワープ後かワープ前か

    //アニメーション対応
    Animator animator;  //アニメーター
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string attackAnime = "PlayerAttack";
    public string jumpAnime = "PlayerJump";
    public string damageAnime = "PlayerDamage";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";
    public static int actState = 0;

    //  SE関連
    public AudioClip SEJump;
    public AudioClip SEDamage;
    public AudioClip SERecovery;
    AudioSource audioSource;

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
    //  Dashスクリプト
    [SerializeField]
    private PlayerDash playerDash;

    //イベント用
    public bool controlEnabled {get; set; } = true; //操作有効無効Bool値

    public float knockBackPower;

    public void Awake()
    {
        

        if (instance == null)
        {
            instance = this;
        }
    }

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
        audioSource = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    public void Update()
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

            /*// キャラをダッシュさせる
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Dash();     //ダッシュ
            }*/

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
            //Debug.Log(val);
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
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 1.2f), groundLayer);
        onWater = Physics2D.Linecast(transform.position, transform.position - (transform.up * 1.2f), waterLayer);

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
            Vector2 jumpPw = new Vector2(0, jump);          //ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //瞬間的な力を加える
            goJump = false; //ジャンプフラグを下ろす
        }
        else if (onWater && goJump)
        {
            //  水面でジャンプキーが押された
            //  ジャンプさせる
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
        goJump = true;      //  ジャンプフラグを立てる
        audioSource.PlayOneShot(SEJump);    //  ジャンプ音
    }


    //  攻撃アニメーション終了関数
    IEnumerable endMotionAnime()
    {
        yield return new WaitForSeconds(1);
        actState = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //体力回復処理
        if (col.gameObject.tag == "Item" & hp <= 4)
        {
            //Debug.Log("回復アイテムに触れた");
            hp++;
            lifeGauge.SetLifeGauge(hp);
            audioSource.PlayOneShot(SERecovery);
        }
    }

    //接触判定
    void OnTriggerEnter2D(Collider2D col) 
    {
        //敵との衝突処理
        
        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log("Hit Enemy");
            //ダメージアニメーション

            // 攻撃アニメ再生中は、以下の処理しない(無敵判定)
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
            {
                return;
            }

            if (playerDash.hit == false)
            {
                return;
            }

            if (gameState == "gameover")
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

            if (hp >= 1)
            {
                //移動停止
                rbody.velocity = new Vector2(0, 0);

                //ノックバック処理
                Vector2 hitPoint = (this.transform.position - col.transform.position).normalized;
                if (hitPoint.x >= 0)
                {
                    //Debug.Log("R");
                    this.rbody.AddForce(transform.right * 110.0f);
                }
                else
                {
                    //Debug.Log("L");
                    this.rbody.AddForce(transform.right * -110.0f);
                }
                //rbody.AddForce(hitPoint * 4, ForceMode2D.Impulse);

                //  ダメージ音
                if (col.gameObject.tag == "Enemy")
                {
                    audioSource.PlayOneShot(SEDamage);
                }

                lifeGauge.SetLifeGauge(hp);
                lifeGauge.SetLifeGauge2(hp);

                //ダメージフラグON
                inDamage = true;
                // コルーチン開始
                //StartCoroutine("WaitForIt");
                Invoke("DamageEnd", 0.5f);
            }
            else
            {
                //ゲームオーバー
                GameOver();
            }
        }

        // Clearのタグが付くオブジェクトに接触
        if (col.gameObject.tag == "Clear" && warpFlag)
        {
            //Debug.Log("Touch Goal");
            hp = 5;
            warpFlag = false;
            SceneManager.LoadScene("ClearScene");
        }

        // Warpのタグが付くオブジェクトに接触
        if (col.gameObject.tag == "Warp")
        {
            warpFlag = true; // ゴールできるフラグを立てる

            Backimg.DOFade(1, 2); // フェードアウト
            Invoke("FadeIn", 2.0f); // フェードイン
            Invoke("Warp", 1.0f); // ゴール前までワープ
        }
    }

    // フェードイン
    public void FadeIn()
    {   
        Backimg.DOFade(0, 2);
    }

    // ワープ処理
    public void Warp()
    {
        this.transform.position = new Vector3(325, 3, 0);
    }

    IEnumerable WaitForIt()
    {
        yield return new WaitForSeconds(10);
    }

    // ダメージ終了
    void DamageEnd()
    {
        // ダメージフラグOFF
        inDamage = false;
        // スプライトを元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void WaitDead() // 死亡処理
    {
        SceneManager.LoadScene("TitleScene");
        hp = 5;
    }

    // ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー");
        gameState = "gameover";
        animator.Play(deadAnime);
        Invoke("WaitDead", 1.0f);
    }
}
