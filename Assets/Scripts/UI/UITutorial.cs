using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITutorial : MonoBehaviour
{
    bool breakUI = false;           // UIを壊すためのフラグ

    [SerializeField]
    private GameObject gaidObject;  // 案内UIの表示非表示
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //接触判定
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "UIBreak")
        {
            if (breakUI == true)
            {
                Debug.Log("test");
                gaidObject.gameObject.SetActive(false);
            }
        }
        else if (col.gameObject.tag == "TextEvent")
        {
            count++;
            if (count == 2)
            {
                gaidObject.gameObject.SetActive(true);
                breakUI = true;
            }
        }
    }

}
