using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehave : MonoBehaviour
{
    [Header("追いかけられる側"),SerializeField]GameObject target;
    [Header("お化けの速さ"),SerializeField]float GhostSpeed = 0.4f;
    [Header("何秒に1回相手の座標を取得するか"),SerializeField]float GetCoordinate = 10f;
    float coordinateTimer = 0;
    // float radian = 0;
    // Start is called before the first frame update
    void Start()
    {
         
    }


    // Update is called once per frame
    void Update()
    {
        // radian += Time.deltaTime;
        // if(radian >= 2){
        //     radian = 0;
        // }
        // coordinateTimer += Time.deltaTime;
        if(coordinateTimer > GetCoordinate )
        {
            Coordinate();
            coordinateTimer = 0;
        }else{
            GhostMove();
        }
        
    }

    Vector2 Coordinate()
    {
    // 対象物へのベクトルを算出
    Vector2 toDirection = target.transform.position - transform.position;
    Debug.Log("Load coordinate");
    return toDirection;
    }

    void GhostMove()
    {
        Vector2 direction = Coordinate();
        //ゆらゆら動くとき
        //float cos = Mathf.Cos(Mathf.PI * radian); 
        
        // 対象物へ回転する
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        //対象物へ接近
        this.transform.Translate(Vector2.up * Time.deltaTime * GhostSpeed);
    }
}
