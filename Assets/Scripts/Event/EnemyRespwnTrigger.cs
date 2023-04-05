using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespwnTrigger : MonoBehaviour
{
    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject ghost3;
    public GameObject ghost4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ghost1.SetActive(false);
            ghost2.SetActive(false);
            ghost3.SetActive(false);
            ghost4.SetActive(false);
        }
    }
}

