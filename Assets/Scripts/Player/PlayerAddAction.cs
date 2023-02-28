using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddAction : MonoBehaviour
{
    bool goDash = false;            //ダッシュ開始フラグ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // キャラをダッシュさせる
        if (Input.GetButtonDown("Fire3") && PlayerController.instance.axisH != 0.0f)
        {
            Dash();     //ダッシュ
        }
    }

    private void FixedUpdate()
    {
        Transform mytransform = this.transform;

        if (goDash && PlayerController.instance.axisH > 0.0f)
        {
            mytransform.Translate(3.0f, 0.0f, 0.0f);
            goDash = false;
        }
        else if (goDash && PlayerController.instance.axisH < 0.0f)
        {
            mytransform.Translate(-3.0f, 0.0f, 0.0f);
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
