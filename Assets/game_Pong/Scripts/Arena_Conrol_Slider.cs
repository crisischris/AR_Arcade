using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arena_Conrol_Slider : MonoBehaviour
{
    public Slider slider;
    float SliderVal;
    public GameObject gm;
    // Update is called once per frame
    void Update()
    {
        MoveArenaUpDown();

    }

    void MoveArenaUpDown()
    {
        SliderVal = slider.value;
        SliderVal *= -1;
        Vector3 moveArena = transform.position;
        moveArena.y = SliderVal;
        transform.position = moveArena;
    }
}
