using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Animation : MonoBehaviour
{
    private Text t;
    public int starting_t_size;
    public int t_size;
    public bool reverse = false;
    private int frames = 0;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        starting_t_size = t.fontSize;
        t_size = starting_t_size;
    }

    // Update is called once per frame
    void Update()
    {
        //time checks to skip every other frame
        if(frames > 2)
        {
            t_size = t.fontSize;
            //occilate back and forward
            if (t_size >= starting_t_size)
            {
                if (t_size - starting_t_size > 5)
                    reverse = true;
            }
            else
            {
                if (starting_t_size - t_size > 5)
                    reverse = false;
            }


            if (reverse)
                Shrink();
            else
                Expand();

            frames = 0;
        }
       

        frames++;
        
    }

    void Shrink()
    {
        t.fontSize--;

    }

    void Expand()
    {
        t.fontSize++;
    }
}
