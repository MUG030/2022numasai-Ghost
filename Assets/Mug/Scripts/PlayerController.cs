using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;              //Rigidbody2D�^�̕ϐ�
    float axisH = 0.0f;             //����
    public float speed = 3.0f;      //�ړ����x

    public float jump = 9.0f;       //�W�����v��
    public LayerMask groundLayer;      //���n�ł��郌�C���[
    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�n�ʂɗ����Ă���t���O

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D�������Ă���
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���������̂ɂイ��傭���`�F�b�N����
        axisH = Input.GetAxisRaw("Horizontal");
        //�����̒���
        if (axisH > 0.0f)
        {
            //�E�ړ�
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1, 1);
        } else if (axisH < 0.0f)
        {
            //���ړ�
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1); //���E���]������ 
        }
        //�L�������W�����v������
        if (Input.GetButtonDown("Jump"))
        {
            Jump(); //�W�����v
        }
    }

    void FixedUpdate()
    {
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
}