using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fadein : MonoBehaviour
{
    public GameObject img;

    public Ease Ease_Type;
    public float duration = 2.0f;
    public float start_alpha = 1f;
    public float end_alpha = 0f;

    public void Start()
    {
        var img = GetComponent<Image>();
        img.DOFade(end_alpha, duration)
            .SetEase(Ease_Type);
    }
}
