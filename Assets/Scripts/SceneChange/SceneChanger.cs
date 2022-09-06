using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void SCTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void SCMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SCClear()
    {
        SceneManager.LoadScene("ClearScene");
    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Clear")//Clearのタグが付くオブジェクトに接触したらクリアシーンへの切り替え
        {
               SceneManager.LoadScene("ClearScene");
               Debug.Log("Touch Goal");
        }

       if (col.gameObject.tag == "Enemy")//Clearのタグが付くオブジェクトに接触したらクリアシーンへの切り替え
       {
           Debug.Log("Hit Enemy");
       }

   }*/
}
