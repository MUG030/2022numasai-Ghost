using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearItemController : MonoBehaviour
{
    [SerializeField] GameObject clearObject;

    // Start is called before the first frame update
    void Start()
    {
        clearObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            clearObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
