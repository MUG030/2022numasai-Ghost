using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    bool goDash = false;            //ダッシュ開始フラグ
    public float dashDistance = 3.0f; //ダッシュの移動距離

    // Update is called once per frame
    void Update()
    {
        // キャラをダッシュさせる
        if (Input.GetButtonDown("Fire3") && PlayerController.instance.axisH != 0.0f)
        {
            // ダッシュ先にBlockTileオブジェクトがあるかチェックする
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * PlayerController.instance.axisH, dashDistance, LayerMask.GetMask("BlockTile"));

            if (!hit)
            {
                // BlockTileオブジェクトがない場合、ダッシュを実行する
                Dash();
            }
        }
    }

    private void FixedUpdate()
    {
        Transform mytransform = this.transform;

        if (goDash && PlayerController.instance.axisH > 0.0f)
        {
            mytransform.Translate(dashDistance, 0.0f, 0.0f);
            goDash = false;
        }
        else if (goDash && PlayerController.instance.axisH < 0.0f)
        {
            mytransform.Translate(-dashDistance, 0.0f, 0.0f);
            goDash = false;
        }

    }
    /// <summary>
    /// ダッシュ
    /// </summary>
    public void Dash()
    {
        goDash = true;
    }
}
