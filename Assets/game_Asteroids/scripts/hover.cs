using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hover : MonoBehaviour
{
    public float yDist = 0;
    public float speed = 10;
    public float offset = 3;

    // Update is called once per frame
    void Update()
    {
        yDist = Mathf.Sin(Time.time) * Time.deltaTime * speed;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+yDist, gameObject.transform.position.z);
    }
}
