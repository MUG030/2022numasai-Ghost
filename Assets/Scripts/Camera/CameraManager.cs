using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; //追従対象
    Vector3 pos;

    void Start()
    {
        pos = Camera.main.gameObject.transform.position; // カメラの初期位置
    }

    void Update()
    {
        Vector3 cameraPos = target.transform.position;

        cameraPos.x = target.transform.position.x; // カメラの位置に対象の位置を代入

        // もし対象の横位置が0より小さい場合
        if (target.transform.position.x < 0)
        {
            cameraPos.x = 0; // カメラの横位置に0を入れる
        }

        cameraPos.y = 0;  // 縦のカメラは固定

        cameraPos.z = -10;
        Camera.main.gameObject.transform.position = cameraPos;
    }
}
