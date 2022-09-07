using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; //�Ǐ]�Ώ�
    Vector3 pos;

    void Start()
    {
        pos = Camera.main.gameObject.transform.position; // �J�����̏����ʒu
    }

    void Update()
    {
        Vector3 cameraPos = target.transform.position;

        cameraPos.x = target.transform.position.x; // �J�����̈ʒu�ɑΏۂ̈ʒu����

        // �����Ώۂ̉��ʒu��0��菬�����ꍇ
        if (target.transform.position.x < 0)
        {
            cameraPos.x = 0; // �J�����̉��ʒu��0������
        }

        cameraPos.y = 0;  // �c�̃J�����͌Œ�

        cameraPos.z = -10;
        Camera.main.gameObject.transform.position = cameraPos;
    }
}
