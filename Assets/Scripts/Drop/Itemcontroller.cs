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
        // Õ“Ë‚µ‚½‘Šè‚ÉPlayerƒ^ƒO‚ª•t‚¢‚Ä‚¢‚é‚Æ‚«
        if (col.gameObject.tag == "Player")
        {
            // 0.2•bŒã‚ÉÁ‚¦‚é
            Destroy(gameObject, 0.0f);
        }
    }
}
