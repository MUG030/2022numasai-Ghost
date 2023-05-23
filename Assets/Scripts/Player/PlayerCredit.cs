using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCredit : MonoBehaviour
{
    Animator animator;  //アニメーター
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    string nowAnime = "";
    string oldAnime = "";
    public static int actState = 0;

    [SerializeField]
    Image Backimg; // ワープ時のフェード用画像

    public GameObject creditObject;
    public GameObject textObject;
    public GameObject buttonObject;

    // Start is called before the first frame update
    void Start()
    {
        creditObject.gameObject.SetActive(false);
        textObject.gameObject.SetActive(true);
        buttonObject.gameObject.SetActive(true);


        //Animatorを持ってくる
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(actState == 0)
            {
                actState = 1;
            }
            else if (actState == 1)
            {
                actState = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            actState = 0;
        }
    }

    private void FixedUpdate()
    {
        if (actState == 0)
        {
            nowAnime = moveAnime;
        }
        else if (actState == 1)
        {
            nowAnime = stopAnime;
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    // アニメーション再生
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Credit")
        {
            Backimg.DOFade(1, 2); // フェードアウト
            Invoke("FadeIn", 2.0f); // フェードイン
        }
    }
    public void FadeIn()
    {
        Backimg.DOFade(0, 3);
        creditObject.gameObject.SetActive(true);
        textObject.gameObject.SetActive(false);
        buttonObject.gameObject.SetActive(false);
    }
}
