using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidUIMove : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float moveDistance = 2f; // 矢印の移動距離
    public float moveTime = 2f; // 矢印の移動にかかる時間

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;

    private void Start()
    {
        startPosition = transform.position;
        timer = 0f;
    }

    private void Update()
    {
        // 矢印の目標位置を設定
        targetPosition = new Vector3(player.position.x + moveDistance, transform.position.y, transform.position.z);
        
        // タイマーを更新し、移動処理を実行
        timer += Time.deltaTime;
        float t = Mathf.PingPong(timer / moveTime, 1f);
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
    }
}
