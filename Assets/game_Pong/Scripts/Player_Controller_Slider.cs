using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller_Slider : MonoBehaviour
{
    public Slider PlayerSlider;
    float SliderVal;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        SliderVal = PlayerSlider.value;
        SliderVal *= -1;
        Vector3 movePlayer = transform.position;
        movePlayer.x = SliderVal;
        transform.position = movePlayer;
    }
}
