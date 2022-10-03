using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform playerTr; // プレイヤーのTransform
    [SerializeField] Vector3 cameraOrgPos = new Vector3(0, 0, -10f); // カメラの初期位置位置
    [SerializeField] Vector2 camaraMaxPos = new Vector2(100, 0); // カメラの(右,上)限界座標(画面端では追従しない)
    [SerializeField] Vector2 camaraMinPos = new Vector2(0, 0); // カメラの(左,下)限界座標(縦のカメラは固定)
    
    void FixedUpdate()
    {
        Vector3 playerPos = playerTr.position; // プレイヤーの位置
        Vector3 camPos = transform.position; // カメラの位置

        // 遅延をかけながらプレイヤーの位置に追従
        camPos = Vector3.Lerp(transform.position, playerPos + cameraOrgPos, 3.0f * Time.deltaTime);

        // カメラの位置を制限
        camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);
        camPos.z = -10f;
        transform.position = camPos;

    }

}
