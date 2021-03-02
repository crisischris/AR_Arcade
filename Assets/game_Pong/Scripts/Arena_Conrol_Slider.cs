using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arena_Conrol_Slider : MonoBehaviour
{
    public Slider slider;
    float SliderVal;
    // Update is called once per frame
    void Update()
    {
        SliderVal = slider.value;
        Vector3 moveArena = transform.position;
        moveArena.y = SliderVal;
        transform.position = moveArena;
        
    }
}
