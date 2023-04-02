using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionCheck : MonoBehaviour
{
    /// <summary>
    /// 判定内に敵か壁がある
    /// </summary>
    [HideInInspector] public bool isOn = false;



    //接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOn = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOn = false;
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    { 
    }
    */
}