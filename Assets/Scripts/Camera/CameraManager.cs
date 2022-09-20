using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform playerTr; // �v���C���[��Transform
    [SerializeField] Vector3 cameraOrgPos = new Vector3(0, 0, -10f); // �J�����̏����ʒu�ʒu
    [SerializeField] Vector2 camaraMaxPos = new Vector2(100, 0); // �J������(�E,��)���E���W(��ʒ[�ł͒Ǐ]���Ȃ�)
    [SerializeField] Vector2 camaraMinPos = new Vector2(0, 0); // �J������(��,��)���E���W(�c�̃J�����͌Œ�)
    
    void FixedUpdate()
    {
        Vector3 playerPos = playerTr.position; // �v���C���[�̈ʒu
        Vector3 camPos = transform.position; // �J�����̈ʒu

        // �x���������Ȃ���v���C���[�̈ʒu�ɒǏ]
        camPos = Vector3.Lerp(transform.position, playerPos + cameraOrgPos, 3.0f * Time.deltaTime);

        // �J�����̈ʒu�𐧌�
        camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);
        camPos.z = -10f;
        transform.position = camPos;

    }

}
