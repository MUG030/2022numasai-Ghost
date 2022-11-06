using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip SEMove;
    public AudioClip SEAttack;
    public AudioClip SERecovery;

    AudioSource audioSource;
    
    int recovery;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        recovery = PlayerController.hp;
    }

    // Update is called once per frame
    void Update()
    {

        //  ˆÚ“®‰¹
        if (Input.GetButtonDown("Horizontal"))
        {
            audioSource.PlayOneShot(SEMove);
        }

        //  UŒ‚‰¹
        if (Input.GetKeyDown(KeyCode.Q))
        {
            audioSource.PlayOneShot(SEAttack);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //‘Ì—Í‰ñ•œˆ—
        if (col.gameObject.tag == "Item" & recovery <= 4)
        {
            audioSource.PlayOneShot(SERecovery);
            Debug.Log("‰ñ•œ");
        }
    }
}
