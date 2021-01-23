using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class has all of the primary functions for the user
/// This class defines the shoot funciton, and also tracks the lives
/// of the user and hit state of the user.  It is the first class to
/// set the gameover state in motion.
/// </summary>
public class User : MonoBehaviour
{

    //Audio related to class
    public AudioClip hit_sound;
    public AudioSource sound_source;

    public GameObject laser;
    private GameObject Manager_UI;

    public int lives;
    //public bool isTakingHit = false;


    public float hit_time;
    public float last_hit_time = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        //hook into the audio souce manager
        sound_source = GameObject.Find("Manager_Audio").GetComponent<AudioSource>();

        Manager_UI = GameObject.Find("Manager_UI");


        //establish time for hit cooldowns
        last_hit_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
            GameOver();
    }


    public void Shoot()
    {
        //check to make sure the gameover state is false
        if(!Logic.GlobalGameOverState)
        {
            GameObject shot = Instantiate(laser);
            shot.transform.position = gameObject.transform.position;
            //adjust the shot down
            shot.transform.position = new Vector3(shot.transform.position.x, shot.transform.position.y - .1f, shot.transform.position.z);
            shot.transform.rotation = gameObject.transform.rotation;
            //shot.transform.eulerAngles = new Vector3(90, transform.rotation.y, transform.rotation.z);
        }
    }

    public void TookHit()
    {
        sound_source.PlayOneShot(hit_sound);
        lives--;
    }

    private void GameOver()
    {
        //set the gameover state
        Logic.GlobalGameOverState = true;
        Manager_UI.GetComponent<UI_manager>().GameOver();
    }
}
