﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class input : MonoBehaviour
{
    //touch vars
    private Touch touch;
    private GameObject user;

    public Text debugText;
    public string debugString = "null";

    public Text debugStartEndPosText;
    public string debugStartEndString = "null";

    public Text debugTouchCount;
    public int debugTouchCountString = 0;

    //for possible inertia boost
    private Vector2 beginTouchPos, endTouchPos;

    public float interia = 0;



    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.Find("AR Camera");

    }

    // Update is called once per frame
    void Update()
    {
        debugTouchCountString = Input.touchCount;
        debugString = "inertia = " + interia.ToString();
        debugText.text = debugString;
        debugStartEndPosText.text = debugStartEndString;
        debugTouchCount.text = debugTouchCountString.ToString();

        //move with intertia
        user.transform.position += user.transform.forward * interia;

        //TODO
        //remove the space key after debug
        if(Input.GetKeyDown(KeyCode.Space))
        {
            user.GetComponent<User>().shoot();
        }
      
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                {
                    beginTouchPos = touch.position;
                    break;
                }

                case TouchPhase.Ended:
                {
                    endTouchPos = touch.position;

                    //calculate the delta to differentiate
                    float min = Mathf.Min(beginTouchPos.y, endTouchPos.y);
                    float max = Mathf.Max(beginTouchPos.y, endTouchPos.y);

                    //this is a tap, not a swipe
                    if (max - min < 25)
                        user.GetComponent<User>().shoot();

                    //this is a movement
                    else
                    {
                        interia = min / max;
                        debugStartEndString = "beginPos = " + min.ToString() + '\n' + "endPos = " + max.ToString();
                    }


                        break;
                }
            }
        }

        if (interia > 0)
            interia -= .01f;
        else
            interia = 0;


    }
}
