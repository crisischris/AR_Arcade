using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Animation : MonoBehaviour
{
    public float starting_s_size;
    public float s_size;
    public bool reverse = false;
    private int frames = 0;

    // Start is called before the first frame update
    void Start()
    {
        starting_s_size = transform.localScale.x;
        s_size = starting_s_size;
    }

    // Update is called once per frame
    void Update()
    {
        //time checks to skip every other frame
        if (frames > 2)
        {
            s_size = transform.localScale.x;
            //occilate back and forward
            if (s_size >= starting_s_size)
            {
                if (s_size - starting_s_size > .05)
                    reverse = true;
            }
            else
            {
                if (starting_s_size - s_size > .05)
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
        transform.localScale = new Vector3(transform.localScale.x - .01f, transform.localScale.y - .01f, transform.localScale.z);

    }

    void Expand()
    {
        transform.localScale = new Vector3(transform.localScale.x + .01f, transform.localScale.y + .01f, transform.localScale.z);
    }
}
