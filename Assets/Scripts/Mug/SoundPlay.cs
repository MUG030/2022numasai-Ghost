using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // audioSource.Play(clip1);
        }

        if (Input.GetMouseButton(1))
        {
            source2.PlayOneShot(clip2);
        }
    }
}
