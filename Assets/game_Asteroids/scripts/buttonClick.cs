using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class buttonClick : MonoBehaviour
{

    //list of variables we will be needing
    public ARSessionOrigin session;
    public GameObject location;

    public bool right;
    public bool left;
    public string destination;
    public Camera userPos;
    public List<string> persistanceText = new List<string>();
    public List<string> seenImage = new List<string>();
    public Text output;
    public GameObject Uinterface;
    public bool lerpUIUp;
    public bool lerpUIDown;
    private Vector2 upRightStartPos;
    private Vector2 upLeftStartPos;
    private Button upRight;
    private Button upLeft;
    private Vector3 curPos;
    private Vector3 curRot;


    //gather all the things we need and instantiate vars
    void Start()
    {
        userPos = FindObjectOfType<Camera>();
        destination = null;
        upRight = GameObject.FindObjectOfType<Canvas>().GetComponent<buttonArragement>().displayUpRight;
        upLeft = GameObject.FindObjectOfType<Canvas>().GetComponent<buttonArragement>().displayUpLeft;
        upRightStartPos = upRight.transform.position;
        upLeftStartPos = upLeft.transform.position;
        output.text = null;
    }
    
    void Update()
    {
        //track the current user position in 3d space
        curPos = userPos.transform.position;
        curRot = userPos.transform.eulerAngles;

        //append our persistant text log to the output variable and onto the screen
        foreach (String s in persistanceText)
            output.text += s + "\n";   

        //clear the Text clog.
        persistanceText.Clear();

        //check the bool to raise the UI up and send the displayUp button down
        if(lerpUIUp)
        {
            Uinterface.transform.position = Vector2.Lerp(Uinterface.transform.position, new Vector2(Uinterface.transform.position.x, Screen.height / 3.5f ), Time.deltaTime * 10);
            upRight.transform.position = Vector2.Lerp(upRight.transform.position, new Vector2(upRightStartPos.x, upRightStartPos.y - upRight.GetComponent<RectTransform>().sizeDelta.y * 2), Time.deltaTime * 5);
            upLeft.transform.position = Vector2.Lerp(upLeft.transform.position, new Vector2(upLeftStartPos.x, upLeftStartPos.y - upLeft.GetComponent<RectTransform>().sizeDelta.y * 2), Time.deltaTime * 5);
        }

        //check the bool to send the UI back down, bring the displayUp button back
        if (lerpUIDown)
        {
            Uinterface.transform.position = Vector3.Lerp(Uinterface.transform.position, new Vector2(Uinterface.transform.position.x, -50), Time.deltaTime * 10);
            upRight.transform.position = Vector2.Lerp(upRight.transform.position, upRightStartPos, Time.deltaTime * 10);
            upLeft.transform.position = Vector2.Lerp(upLeft.transform.position, upLeftStartPos, Time.deltaTime * 10);
        }
    }


    /****************************************************************
     * on-click function mapped to Unity's botton click UI
     * must be public to access the on-click in Unity engine
     ****************************************************************/
    public void displayUI()
    {
        lerpUIDown = false;
        lerpUIUp = true;
    }

    /****************************************************************
     * helper function sets a flag to lower the UI in update()
     ****************************************************************/
    private void hideUI()
    {
        lerpUIDown = true;
        lerpUIUp = false;
    }

    /****************************************************************
     * on-click function mapped to Unity's botton click UI
     * must be public to access the on-click in Unity engine
     ****************************************************************/
    public void testButton1()
    {
        persistanceText.Add("test button 1");
        hideUI();
    }


    /****************************************************************
    * on-click function mapped to Unity's botton click UI
    ****************************************************************/
    public void testButton2()
    {
        persistanceText.Add("test button 2");
        hideUI();
    }

    /****************************************************************
    * on-click function mapped to Unity's botton click UI
    ****************************************************************/
    public void testButton3()
    {
        persistanceText.Add("test button 3");
        hideUI();
    }

    /****************************************************************
    * on-click function logs the user position and rotation to
    * the output text on the screen
    ****************************************************************/
    public void logPos()
    {
        persistanceText.Add("pos: " + curPos.ToString());
        persistanceText.Add("rot: " + curRot.ToString());
        hideUI();
    }

    /****************************************************************
    * on-click function will reset the user in 3d space to a 
    * pre-defined point
    ****************************************************************/
    public void resetPos()
    {
        session.transform.position = new Vector3(0, 1.67f, 0);
        session.transform.rotation = Quaternion.Euler(0, 0, 0);
        persistanceText.Add("position reset");
        hideUI();
    }

    /****************************************************************
    * on-click function clear the text log that is displayed in on 
    * screen
    ****************************************************************/
    public void clearPos()
    {
        output.text = null;
        hideUI();
    }

    /****************************************************************
    * on-click function rotates the user by a pre-defined angle 
    * amount
    ****************************************************************/
    public void rotate()
    {
        session.transform.rotation = Quaternion.Euler(session.transform.rotation.eulerAngles.x, session.transform.rotation.eulerAngles.y + 5, session.transform.rotation.eulerAngles.z);
        hideUI();
    }
}
