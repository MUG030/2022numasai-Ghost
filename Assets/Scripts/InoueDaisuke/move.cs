using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class move : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public float speed = 0.01f;
    public float houkou = 1.0f;
    public EnemyCollisionCheck checkCollision;
    /*private float p1=0.0f;
    public trigger checkCollision;
    private string enemyTag = "Enemy";
    Start is called before the first frame update*/
    void Start()
    {
    }

    void Update()
    {
        if (checkCollision.isOn)
        {
            houkou = houkou * -1.0f;
        }
            transform.Translate(houkou * speed, 0, 0);
            transform.localScale = new Vector3(houkou, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject, 0.0f);
        }
    }

}