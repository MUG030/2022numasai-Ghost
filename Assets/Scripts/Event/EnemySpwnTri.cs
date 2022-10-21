using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwnTri : MonoBehaviour
{
    public GameObject ghost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ghost.SetActive(true);
        }
    }
}
