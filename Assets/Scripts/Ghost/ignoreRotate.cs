using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreRotate : MonoBehaviour
{
    [Header("参照するカメラ"),SerializeField]GameObject CameraPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void FixedUpdate()
    {
        //カメラの位置参照
        //Vector2 cameraPos = CameraPos.transform.position;
        //幽霊自身の位置参照
        //Vector2 ghostPos = transform.position;
        if(CameraPos.transform.position.x > transform.position.x){
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if(CameraPos.transform.position.x <= transform.position.x){
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }
}
