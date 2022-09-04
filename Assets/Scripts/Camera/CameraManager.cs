using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;
    Vector3 pos;

    void Start()
    {
        pos = Camera.main.gameObject.transform.position; //�J�����̏����ʒu
    }

    void Update()
    {
        Vector3 cameraPos = target.transform.position; // cameraPos�Ƃ����ϐ������A�Ǐ]����Ώۂ̈ʒu������

        // �����Ώۂ̉��ʒu��0��菬�����ꍇ
        if (target.transform.position.x < 0)
        {
            cameraPos.x = 0; // �J�����̉��ʒu��0������
        }

        // �����Ώۂ̏c�ʒu��0��菬�����ꍇ
        if (target.transform.position.y < 0)
        {
            cameraPos.y = target.transform.position.y;   // �J�����̏c�ʒu�ɑΏۂ̈ʒu������
        }

        // �����Ώۂ̏c�ʒu��0���傫���ꍇ
        if (target.transform.position.y > 0)
        {
            cameraPos.y = target.transform.position.y;   // �J�����̏c�ʒu�ɑΏۂ̈ʒu������
        }

        cameraPos.z = -10; // �J�����̉��s���̈ʒu��-10������
        Camera.main.gameObject.transform.position = cameraPos; //�@�J�����̈ʒu�ɕϐ�cameraPos�̈ʒu������

    }
}
