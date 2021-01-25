using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    public Transform ball;
    

    // Update is called once per frame
    void Update()
    {
        // Vector3 of position of paddle
        Vector3 newPosition = transform.position;
        // Change the value of the x axis, to match the balls position
        newPosition.x = Mathf.Lerp(transform.position.x, ball.position.x, 1);
        // Set new position for paddle
        transform.position = newPosition;

        
    }
}
