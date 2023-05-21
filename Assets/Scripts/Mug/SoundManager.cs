using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public AudioClip bgmForest;
    public AudioClip bgmUnderGround;

    private AudioSource bgmSource;
    public float volumeScale = 0.5f;

    private bool isBGMForestPlaying = true;

    void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.clip = bgmForest;
        // 音量を半減させる
        bgmSource.volume = volumeScale * bgmSource.volume;
        bgmSource.Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BGMChangeEvent")
        {
            if (isBGMForestPlaying)
            {
                bgmSource.clip = bgmUnderGround;
                isBGMForestPlaying = false;
            }
            else
            {
                bgmSource.clip = bgmForest;
                isBGMForestPlaying = true;
            }
            bgmSource.Play();
        }
        else if (col.gameObject.tag == "Warp")
        {
            bgmSource.clip = bgmForest;
            isBGMForestPlaying = true;
            bgmSource.Play();
        }
    }
}
