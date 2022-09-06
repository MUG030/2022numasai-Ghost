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
    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�n�ʂɗ����Ă���t���O

    //�_���[�W�Ή�
    public static int hp = 3;       //�v���C���[��hp
    public static string gameState; //�Q�[���̏��
    bool inDamage = false;          //�_���[�W���̃t���O

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D�������Ă���
        rbody = this.GetComponent<Rigidbody2D>();
        //�Q�[���̏�Ԃ��v���C���ɂ���
        gameState = "playing";
    }

    // Update is called once per frame
    void Update()
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
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            //���ړ�
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1); //���E���]������ 
        }
        //�L�������W�����v������
        if (Input.GetButtonDown("Jump"))
        {
            Jump();     //�W�����v
        }
    }

    void FixedUpdate()
    {
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
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)
        {
            //�n�ʂ̏�or���x��0�ł͂Ȃ�
            //���x�̍X�V
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            //�n�ʂ̏�ŃW�����v�L�[�������ꂽ
            //�W�����v������
            Debug.Log("�W�����v!");
            Vector2 jumpPw = new Vector2(0, jump);          //�W�����v������x�N�g�������
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //�u�ԓI�ȗ͂�������
            goJump = false; //�W�����v�t���O�����낷
        }
    }
    //�W�����v
    public void Jump()
    {
        goJump = true;      //�W�����v�t���O�𗧂Ă�
        Debug.Log("�W�����v�{�^������!");
    }

    //�ڐG����
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Clear")//Clear�̃^�O���t���I�u�W�F�N�g�ɐڐG������N���A�V�[���ւ̐؂�ւ�
        {
            SceneManager.LoadScene("ClearScene");
            Debug.Log("Touch Goal");
        }

        if (col.gameObject.tag == "Enemy")//Clear�̃^�O���t���I�u�W�F�N�g�ɐڐG������N���A�V�[���ւ̐؂�ւ�
        {
            Debug.Log("Hit Enemy");
            hp--;       //HP�����炷
            if (hp > 0)
            {
                //�ړ���~
                rbody.velocity = new Vector2(0, 0);
                //�G�L�����̔��Ε����Ƀq�b�g�o�b�N������
                if (transform.localScale.x >= 0)
                {
                    this.rbody.AddForce(transform.right * -400.0f);
                } 
                else
                {
                    this.rbody.AddForce(transform.right * 400.0f);
                }
                //�_���[�W�t���OON
                inDamage = true;
                Invoke("DamageEnd", 0.25f);
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
    //�Q�[���I�[�o�[
    void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[");
        //gameState = "gameover";

    }
}
