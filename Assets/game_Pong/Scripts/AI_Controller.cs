﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    public Transform ball;
    [SerializeField] float paddleSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // Vector3 of position of paddle
        Vector3 newPosition = transform.position;
        //float paddleSpeed = Random.Range(0.1f, 0.5f);
        // Change the value of the x axis, to match the balls position
        //newPosition.x = Mathf.Lerp(transform.position.x, ball.position.x, paddleSpeed);
        // Set new position for paddle
        if (newPosition.x > ball.position.x)
        {
            newPosition.x -= paddleSpeed;
        }
        else if(newPosition.x < ball.position.x)
        {
            newPosition.x += paddleSpeed;
        }
        transform.position = newPosition;

        
    }
}
