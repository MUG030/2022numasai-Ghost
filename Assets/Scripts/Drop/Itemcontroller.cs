using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        // �Փ˂��������Player�^�O���t���Ă���Ƃ�
        if (col.gameObject.tag == "Player")
        {
            // 0.2�b��ɏ�����
            Destroy(gameObject, 0.0f);
        }
    }
}
