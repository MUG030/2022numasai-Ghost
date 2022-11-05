using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartIndicator : MonoBehaviour
{
    //　ライフゲージプレハブ
    [SerializeField] private GameObject lifeObj;
    [SerializeField] private GameObject LostlifeObj;

    //　ライフゲージ全削除＆HP分作成
    public void SetLifeGauge(int life)
    {
        //　体力を一旦全削除
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        //　現在の体力数分のライフゲージを作成
        for (int i = 0; i < life; i++)
        {
            Instantiate<GameObject>(lifeObj, transform);
        }
        for (int j = 5; j > life; j--)
        {
            Instantiate<GameObject>(LostlifeObj, transform);
        }
    }
    //　ダメージ分だけ削除
    public void SetLifeGauge2(int damage)
    {
        for (int i = 0; i < damage; i++)    //  初期値i=0でダメージ食らったら比較でfor内が呼び出されてi+1されてfor文を抜ける
        {
            //　最後のライフゲージを削除
            Destroy(transform.GetChild(i).gameObject);
            //  Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject);
        }
    }
}
