using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLIDED");
        if (collision.gameObject.CompareTag("projectile"))
            Debug.Log("tag name is " + collision.gameObject.tag);

    }
}
