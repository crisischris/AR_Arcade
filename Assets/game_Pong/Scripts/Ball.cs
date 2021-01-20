using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    // Velocity Vector
    Vector3 velocity;
    [Range(0,1)]
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        // Want it to move either 1 or -1 on z axis
        // Random int between 0 and 1, multiply by 2 it will be either 0 or 2, -1 it will be -1 or 1
        float z = Random.Range(0, 2) * 2f - 1f;
        // Dont want it to comestraight at player 
        float x = Random.Range(0, 2) * 2f - 1f * Random.Range(0.2f, 1f);
        velocity = new Vector3(x, 0, z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = velocity.normalized * speed;
        transform.position += velocity;
    }
}
