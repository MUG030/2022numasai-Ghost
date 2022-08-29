using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehave : MonoBehaviour
{
    [Header("追いかけられる側"),SerializeField]GameObject target;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 対象物へのベクトルを算出
        Vector2 toDirection = target.transform.position - transform.position;
        // 対象物へ回転する
        transform.rotation = Quaternion.FromToRotation(Vector2.up, toDirection);
        this.transform.Translate(Vector2.up * Time.deltaTime * 0.9f);
    }
}
