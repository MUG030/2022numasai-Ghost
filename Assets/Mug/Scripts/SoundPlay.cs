using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    public PlayerController playerController;
    int aSound;

    private void Start()
    {
        aSound = playerController.Gethp();
    }

    void Update()
    {
        if (aSound >= 1)
        {
            source1.PlayOneShot(clip1);
        }

        if (Input.GetMouseButton(1))
        {
            source2.PlayOneShot(clip2);
        }
    }
}
