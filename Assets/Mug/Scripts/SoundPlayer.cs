using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip SEJump;
    public AudioClip SEMove;
    public AudioClip SEAttack;
    public AudioClip SEDamage;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //  �W�����v��
        if (Input.GetButtonDown("Jump"))
        {
            audioSource.PlayOneShot(SEJump);
        }

        //  �ړ���
        if (Input.GetButtonDown("Horizontal"))
        {
            audioSource.PlayOneShot(SEMove);
        }

        //  �U����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            audioSource.PlayOneShot(SEAttack);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //  �_���[�W��
        if(col.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(SEDamage);
        }
    }
}
