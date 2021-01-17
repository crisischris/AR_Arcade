using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buttonArragement : MonoBehaviour
{
    public Button displayUpRight;
    public Button displayUpLeft;
    public int verticalOffset = 20;

    // set all the buttons
    void Awake()
    {
        //main UI display up button
        displayUpRight.transform.position = new Vector3(Screen.width-40, 50, 0);
        displayUpLeft.transform.position = new Vector3(40, 50, 0);
    }
}
