using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{

    //touch vars
    private Touch touch;
    public bool left;
    public bool right;

    public int lerpSpeed = 10;

    private bool lerpLeft = false;
    private bool lerpRight = false;

    public Text debugStartEndPosText;
    public string debugStartEndString;

    public Text debugTouchCount;
    public int debugTouchCountString = 0;


    private Vector3 touchPosWorld;


    //for possible inertia boost
    private Vector2 beginTouchPos, endTouchPos;

    public float interia = 0;



    bool touchingObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (lerpLeft)
            transform.position = Vector2.Lerp(transform.position, new Vector2(-200, transform.position.y), Time.deltaTime * lerpSpeed);





        //DEBUG update the touch count
        debugTouchCountString = Input.touchCount;


        ////turn the debug text on
        //if (debug)
        //{
        ////set the string
        //debugString = "inertia = " + interia.ToString();
        //debugTouchCount.text = debugTouchCountString.ToString();

        ////display the string
        //debugText.text = debugString;
        //debugStartEndPosText.text = debugStartEndString;
        //}
        /////debug text is off
        //else
        //{
        //debugText.text = null;
        //debugStartEndPosText.text = null;
        //debugTouchCount.text = null;
        //}



        //move with intertia
        //user.transform.position += user.transform.forward* interia;

        //TODO
        //remove the space key after debug
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LerpUILeft();
        }
      
        if (Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;
            Vector2 touchPosTrans = new Vector2(touchPosWorld.x, touchPosWorld.y);
            touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);

            RaycastHit2D hit = Physics2D.Raycast(touchPosTrans, Camera.main.transform.forward);
            if (hit.collider != null)
                touchingObject = true;

            //if(touchingObject)
            //{
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
                    Mathf.Max(beginTouchPos.y, endTouchPos.y);

                    if(beginTouchPos.x < endTouchPos.x)
                        LerpUILeft();
                    if (beginTouchPos.x > endTouchPos.x)
                        LerpUIRight();
                    break;
                }
            }
            transform.position = new Vector2(touchPos.x, transform.position.y);
            //}
        }

        touchingObject = false;
    }


    public void LerpUILeft()
    {
        //check to see if we are at the left most position
        if(!left)
            lerpLeft = true;
    }

    public void LerpUIRight()
    {
        if (!right)
            lerpRight = true;
    }
        
    
}
