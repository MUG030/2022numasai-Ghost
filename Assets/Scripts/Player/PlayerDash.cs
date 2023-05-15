using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashDistance = 3.0f;   // ダッシュ距離
    public float dashDuration = 0.5f;   // ダッシュ時間
    public float dashCooldown = 1.0f;   // ダッシュのクールダウン時間
    private float lastDashTime;         // 前回ダッシュした時刻

    public bool hit = false;            // 当たり判定フラグ
    bool isDashing = false;             // ダッシュ中フラグ

    // Update is called once per frame
    void Update()
    {
        if ( Time.time > lastDashTime + dashCooldown)
        {
            // ダッシュの開始
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Horizontal") != 0.0f && !isDashing)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private IEnumerator Dash()
    {
        // ダッシュ開始前に当たり判定フラグをオフにする
        hit = false;
        isDashing = true;

        // ダッシュ速度の計算
        float dashSpeed = dashDistance / dashDuration;

        // ダッシュ時間だけ進む
        float elapsedTime = 0.0f;
        while (elapsedTime < dashDuration)
        {
            float distanceToMove = dashSpeed * Time.deltaTime;
            if(Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                transform.Translate(transform.right * distanceToMove);
            } else if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                transform.Translate(-transform.right * distanceToMove);
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // ダッシュ後に当たり判定フラグをオンにする
        hit = true;
        isDashing = false;
        lastDashTime = Time.time;
    }
}
