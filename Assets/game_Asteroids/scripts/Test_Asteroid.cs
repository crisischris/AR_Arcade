using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Asteroid : MonoBehaviour
{
    public int Speed;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.forward * Speed;
    }
}
