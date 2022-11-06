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
        //  ジャンプ音
        if (Input.GetButtonDown("Jump"))
        {
            audioSource.PlayOneShot(SEJump);
        }

        //  移動音
        if (Input.GetButtonDown("Horizontal"))
        {
            audioSource.PlayOneShot(SEMove);
        }

        //  攻撃音
        if (Input.GetKeyDown(KeyCode.Q))
        {
            audioSource.PlayOneShot(SEAttack);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //  ダメージ音
        if(col.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(SEDamage);
        }
    }
}
