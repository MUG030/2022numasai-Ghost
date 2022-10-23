using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;              //Rigidbody2D�^�̕ϐ�
    float axisH = 0.0f;             //����
    public float speed = 3.0f;      //�ړ����x

    public float jump = 9.0f;       //�W�����v��
    public LayerMask groundLayer;   //���n�ł��郌�C���[
    public LayerMask waterLayer;   //���n�ł��郌�C���[
    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�n�ʂɗ����Ă���t���O
    bool onWater = false;          //�n�ʂɗ����Ă���t���O
    bool isAttacking = false;       // �U�����[�V�����̃t���O

    //�A�j���[�V�����Ή�
    Animator animator;  //�A�j���[�^�[
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string attackAnime = "PlayerAttack";
    public string jumpAnime = "PlayerJump";
    public string damageAnime = "PlayerDamage";
    public static string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";
    public static int actState = 0;

    //�_���[�W�Ή�
    public static int hp = 5;       //�v���C���[��hp
    public int Gethp()              //�擾�֐�
    {
        return hp;
    }
    public static string gameState; //�Q�[���̏��
    bool inDamage = false;          //�_���[�W���̃t���O
    //�@LifeGauge�X�N���v�g
    [SerializeField]
    private HeartIndicator lifeGauge;

    //�C�x���g�p
    public bool controlEnabled {get; set; } = true; //����L������Bool�l

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D�������Ă���
        rbody = this.GetComponent<Rigidbody2D>();
        //Animator�������Ă���
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;   
        oldAnime = stopAnime;
        //�Q�[���̏�Ԃ��v���C���ɂ���
        gameState = "playing";
        //�@�̗̓Q�[�W�ɔ��f
        lifeGauge.SetLifeGauge(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlEnabled)//�X�g�[���[�C�x���g�p
        {
            //�Q�[�����ȊO�ƃ_���[�W���͉������Ȃ�
            if (gameState != "playing" || inDamage)
            {
                return;
            }

            //���������̂ɂイ��傭���`�F�b�N����
            axisH = Input.GetAxisRaw("Horizontal");
            //�����̒���
            if (axisH > 0.0f)
            {
                //�E�ړ�
                //Debug.Log("�E�ړ�");
                actState = 1;
                transform.localScale = new Vector2(1, 1);
            }
            else if (axisH < 0.0f)
            {
                //���ړ�
                //Debug.Log("���ړ�");
                actState = 1;
                transform.localScale = new Vector2(-1, 1); //���E���]������ 
            }
            //�L�������W�����v������
            if (Input.GetButtonDown("Jump"))
            {
                Jump();     //�W�����v
            }

            Attack();
        }
    }

    void FixedUpdate()
    {
        // �U���A�j���Đ����́A�ȉ��̏������Ȃ��@�@�@�@�@�@�@�@�@�@�@�@�@
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
        {
            return;
        }

        //�Q�[�����͉������Ȃ�
        if (gameState != "playing")
        {
            return;
        }
        if (inDamage)
        {
            //�_���[�W���͓_�ł���
            float val = Mathf.Sin(Time.time * 50);
            Debug.Log(val);
            if (val > 0)
            {
                //�X�v���C�g��\��
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //�X�v���C�g���\��
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

            return;     //�_���[�W���͑���ɂ��e�����󂯂Ȃ�
        }

        //�n�㔻��
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.9f), groundLayer);
        onWater = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.9f), waterLayer);

        if (onGround || axisH != 0 && !onWater)
        {
            //�n�ʂ̏�or���x��0�ł͂Ȃ��A�̓�𖞂����Ă��Ă����ʂł͂Ȃ�
            //���x�̍X�V
            speed = 3.0f;
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        else if (onWater || axisH != 0)
        {
            //���ʂł̑��x�̍X�V
            speed = 1.7f;
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }

        if (onGround && goJump)
        {
            //�n�ʂ̏�ŃW�����v�L�[�������ꂽ
            //�W�����v������
            //Debug.Log("�W�����v!");
            Vector2 jumpPw = new Vector2(0, jump);          //�W�����v������x�N�g�������
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //�u�ԓI�ȗ͂�������
            goJump = false; //�W�����v�t���O�����낷
        }
        else if (onWater && goJump)
        {
            //���ʂŃW�����v�L�[�������ꂽ
            //�W�����v������
            Vector2 jumpPw = new Vector2(0, jump);          //�W�����v������x�N�g�������
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //�u�ԓI�ȗ͂�������
            goJump = false; //�W�����v�t���O�����낷
        }

        //��~�ƈړ��ƍU���̃A�j���[�V����
        if (onGround)
        {
            //�n�ʂ̏�
            if (actState == 0)
            {
                nowAnime = stopAnime;   //��~��
            }
            else if (actState == 1)
            {
                nowAnime = moveAnime;   //�ړ���
                actState = 0;
            }
        }
        else�@                          //��
        {
            nowAnime = jumpAnime;        
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    // �A�j���[�V�����Đ�
        }



    }

    public void Attack()
    {
        //�U���A�j���[�V������(Q�L�[�������ƍU���J�n)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //�U����
            animator.Play(attackAnime);
            //�ړ���~
            rbody.velocity = new Vector2(0, 0);
        }
    }

    //�W�����v
    public void Jump()
    {
        goJump = true;      //�W�����v�t���O�𗧂Ă�
        //Debug.Log("�W�����v�{�^������!");
    }

    // �U���A�j���[�V�����I���֐�
    IEnumerable endMotionAnime()
    {
        yield return new WaitForSeconds(1);
        actState = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //�̗͉񕜏���
        if (col.gameObject.tag == "Item")
        {
            Debug.Log("�񕜃A�C�e���ɐG�ꂽ");
            hp++;
            lifeGauge.SetLifeGauge(hp);
        }
    }

    //�ڐG����
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Clear")//Clear�̃^�O���t���I�u�W�F�N�g�ɐڐG������N���A�V�[���ւ̐؂�ւ�
        {
            //Debug.Log("Touch Goal");
            hp = 5;
            SceneManager.LoadScene("ClearScene");
        }


        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log("Hit Enemy");
            //�_���[�W�A�j���[�V����

            // �U���A�j���Đ����́A�ȉ��̏������Ȃ�(���G����)
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
            {
                return;
            }

            if (gameState == "gameover")
            {
                return;
            }

            animator.Play(damageAnime);

            // �_���[�W���͏����X�L�b�v
            if (inDamage)
            {
                return;
            }


            hp--;       //HP�����炷

            if (hp >= 1)
            {
                //�ړ���~
                rbody.velocity = new Vector2(0, 0);
                //�G�L�����̔��Ε����Ƀq�b�g�o�b�N������
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

                //�_���[�W�t���OON
                inDamage = true;
                Invoke("DamageEnd", 0.5f);
            }
            else
            {
                //�Q�[���I�[�o�[
                GameOver();
            }

        }

    }

    //�_���[�W�I��
    void DamageEnd()
    {
        //�_���[�W�t���OOFF
        inDamage = false;
        //�X�v���C�g�����ɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void WaitDead()
    {
        SceneManager.LoadScene("TitleScene"); 
        hp = 5;
    }

    //�Q�[���I�[�o�[
    void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[");
        gameState = "gameover";
        animator.Play(deadAnime);
        Invoke("WaitDead", 1.0f);
    }
}
