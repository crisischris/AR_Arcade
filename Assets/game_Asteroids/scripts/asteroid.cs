﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public static int count = 0;
    //asteroid public vars
    public float inertia;
    public Vector3 pos;
    private int timeAlive;
    public int lifeSpan;

    //asteroid private vars
    private float timeStart;
    private GameObject user;
    private GameObject ui_manager;
    public ParticleSystem explosion;
    private float userX;
    private float userY;
    private float userZ;
    private int score = 100;
    private bool dying = false;

   



    //TODO
    //collision detection

    // Start is called before the first frame update
    void Start()
    {
        count++;
        //hook into ui_manager
        ui_manager = GameObject.Find("Manager_UI");

        //get the highscore variable
        timeStart = Time.time;


        //set life
        lifeSpan = Random.Range(10, 16);

        //Establish the user posistion
        user = GameObject.Find("AR Camera");
        userX = user.transform.position.x;
        userY = user.transform.position.y;
        userZ = user.transform.position.z;

        //TODO
        //we need to randomize the spawn to be a certain distance from the user.
        //maybe we make a sphere of randomsize (min 10), raycast a random angle
        //and take the intersect as a spawn point?

        float randomX;
        float randomY;
        float randomZ;
        int randomFlip;
        //Use user position to randomize spawn location of asteroid

        //flip for X pos
        randomFlip = Random.Range(0, 2);

        if (randomFlip == 1)
            randomX = Random.Range(userX - 10, userX - 20);
        else
            randomX = Random.Range(userX + 10, userX + 20);

        //we don't want to randomize the Y or else the user will have to be looking
        //towards the ground
        randomY = Random.Range(userY + 10, userY + 20);


        //flip for Z pos
        randomFlip = Random.Range(0, 2);

        if (randomFlip == 1)
            randomZ = Random.Range(userZ - 10, userZ - 20);
        else
            randomZ = Random.Range(userZ + 10, userZ + 20);


        //assign random size of asteroid
        float randomScale = Random.Range(.1f, 1.3f);

        //assign random intertia for spawned astroid
        inertia = Random.Range(.05f, .15f);


        //apply the randoms to the asteroid
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        transform.position = new Vector3(randomX, randomY, randomZ);

        //TODO
        //We need to rotate the asteriod on spawn to look at or towards the user
        //random noise towards
        //vector math needed here

        //TODO
        //understand how to timer self-destruct.
        //StartSelfDestruct();

        //rotate towards the target at instantiation
        Vector3 target = user.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target, Mathf.PI, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // Update is called once per frame
    void Update()
    {

        //Update the time alive
        timeAlive = (int)(Time.time - timeStart);


        //move forward over time
        transform.position += transform.forward * inertia;
        if (timeAlive >= lifeSpan)
            selfDestruct();
    }

    //getter
    public int getTimeAlive()
    {
        return timeAlive;
    }

    //getter
    public int getLifeSpan()
    {
        return lifeSpan;
    }

    //Explosion is spawned and inherits the asteroid inertia, position and rotation.
    //Asteroid is then destroyed
    public void selfDestruct()
    {
        //Implimenting the dying variable to stop race condition
        //where the asteroid would call mulitple selfDestructs in a
        //single frame
        //create the explosion and inherit values of the asteroid
        var curExplosion = Instantiate(explosion);
        curExplosion.transform.position = transform.position;
        curExplosion.transform.rotation = transform.rotation;
        curExplosion.GetComponent<Explosion>().inertia = inertia / 4;

        //decrement count
        count--;
        //clean up the asteroid
        Destroy(gameObject);
    }


    //This function detects a collision of a tag
    //"projectile".  Calls selfDestruct() on collision
    private void OnCollisionEnter(Collision collision)
    {
       
        //The laser hit the asteroid!
        if(collision.gameObject.CompareTag("projectile"))
        {
            //hook into the manager_ui
            ui_manager.GetComponent<ui_manager>().highscore += score;

            //TODO
            //rectify this double explosion
            if(!dying)
            {
                dying = true;
                selfDestruct();

            }
        }

        

    }

    private void OnTriggerEnter(Collider collision)
    {
        user.GetComponent<User>().hit_time = Time.time;

        //detect if hit was on plater
        if (collision.gameObject.CompareTag("player"))
        {
            //cooldown is over baby
            if (user.GetComponent<User>().hit_time - user.GetComponent<User>().last_hit_time > 1)
            {
                //assign latest hit time relative to the game start
                user.GetComponent<User>().last_hit_time = user.GetComponent<User>().hit_time;

                //decrement life 
                user.GetComponent<User>().lives--;

            }
        }
    }

    //private IEnumerator hitCountDown()
    //{
    //    //super hacky way of buffering a hit
    //    //by iterating through a loop and then
    //    //making bool false
    //    for (int i = 0; i < 10000; i++)
    //    {
    //        var x = 1;
    //    }

    //    user.GetComponent<User>().isTakingHit = false;
    //    yield return new WaitForSeconds(1);
    //}

    //OLD

    ////Start the self destruct counter
    //void StartSelfDestruct()
    //{
    //    StartCoroutine(SelfDestruct(5));
    //    Destroy(this);
    //}

    ////Delay the destroy 
    //private IEnumerator SelfDestruct(int countdown)
    //{
    //    yield return new WaitForSeconds(countdown);
    //}
}
