using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

     void OnTriggerEnter2D(Collider2D col)
     {
         if (col.gameObject.tag == "Clear")//Clear�̃^�O���t���I�u�W�F�N�g�ɐڐG������N���A�V�[���ւ̐؂�ւ�
         {
                SceneManager.LoadScene("ClearScene");
                Debug.Log("Touch Goal");
         }

        if (col.gameObject.tag == "Enemy")//Clear�̃^�O���t���I�u�W�F�N�g�ɐڐG������N���A�V�[���ւ̐؂�ւ�
        {
            Debug.Log("Hit Enemy");
        }

    }
}
