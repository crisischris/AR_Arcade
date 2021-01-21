using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{

    //Audio related to class
    public AudioClip hit_sound;
    public AudioSource sound_source;

    public GameObject laser;
    private GameObject Manager_UI;

    public int lives = 8;
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


    public void shoot()
    {
        GameObject shot = Instantiate(laser);
        shot.transform.position = gameObject.transform.position;
        //adjust the shot down
        shot.transform.position = new Vector3(shot.transform.position.x, shot.transform.position.y - .1f, shot.transform.position.z);
        shot.transform.rotation = gameObject.transform.rotation;
        //shot.transform.eulerAngles = new Vector3(90, transform.rotation.y, transform.rotation.z);

    }

    public void tookHit()
    {
        sound_source.PlayOneShot(hit_sound);
        lives--;
    }

    private void GameOver()
    {
        Manager_UI.GetComponent<ui_manager>().GameOver();
    }
}
