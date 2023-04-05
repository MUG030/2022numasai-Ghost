using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float killTime = 5;
    float killTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        killTimer += Time.deltaTime ;
        if(killTimer > killTime)
        {
            Destroy (this.gameObject);
        }
    }
}
