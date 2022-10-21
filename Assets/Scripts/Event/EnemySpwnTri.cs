using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwnTri : MonoBehaviour
{
    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject ghost3;
    public GameObject ghost4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ghost1.SetActive(true);
            ghost2.SetActive(true);
            ghost3.SetActive(true);
            ghost4.SetActive(true);
        }
    }
}
