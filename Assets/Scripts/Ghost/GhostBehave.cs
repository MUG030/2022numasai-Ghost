using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehave : MonoBehaviour
{
    [Header("追いかけられる側"),SerializeField]GameObject target;
    [Header("お化けの速さ"),SerializeField]float GhostSpeed = 0.4f;
    [Header("何秒に1回相手の座標を取得するか"),SerializeField]float GetCoordinate = 5f;
    Renderer targetRenderer;
    Vector2 direction = Vector2.zero;
    float coordinateTimer = 0;
    // float radian = 0;
    // Start is called before the first frame update

    [Header("幽霊から出るもの"),SerializeField]public GameObject prefabGhostHart;
    void Start()
    {
        GetCoordinate = coordinateTimer;
        targetRenderer = GetComponent<Renderer>();
    }


    // Update is called once per frame
    void Update()
    {
        // radian += Time.deltaTime;
        // if(radian >= 2){
        //     radian = 0;
        // }
        
        if (targetRenderer.isVisible)
        {
            // 画面内にいるときの処理
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
        
        coordinateTimer += Time.deltaTime;
        if(coordinateTimer > GetCoordinate )
        {
            direction = Coordinate();
            coordinateTimer = 0;
        }
        //ゆらゆら動くとき
        //float cos = Mathf.Cos(Mathf.PI * radian); 
        
        // 対象物へ回転する
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        //対象物へ接近
        this.transform.Translate(Vector2.up * Time.deltaTime * GhostSpeed);
    }
    private void OnDestroy() {
        Instantiate(prefabGhostHart, new Vector2(0,0), Quaternion.identity);
    }
}
