using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Audio related to class
    public AudioClip laser_sound;
    public AudioSource sound_source;
    private int lifeSpan = 6;
    private float timeStart;
    private int timeAlive;
    public float speed = .2f;

    // Start is called before the first frame update
    void Start()
    {
        //hook into the audio souce manager
        sound_source = GameObject.Find("Manager_Audio").GetComponent<AudioSource>();

        //play the laser audio clip on instantiation
        sound_source.PlayOneShot(laser_sound);

        //start the sys clock
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the time alive
        timeAlive = (int)(Time.time - timeStart);
        if (timeAlive >= lifeSpan)
            selfDestruct();

        transform.position += transform.forward * speed;
    }

    //Call this to clean up
    public void selfDestruct()
    {
        Destroy(gameObject);
    }
}
