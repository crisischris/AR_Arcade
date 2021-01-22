using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{


    //isChild is a switch that tells us we do not generate variabels, instead
    //we inherit them from the parent
    public bool isChild = false;

    //Audio related to class
    public AudioClip[] asteroid_sound;
    public AudioSource sound_source;
    public GameObject asteroid_source;

    //set range min / max
    private float scale_min = .3f;
    private float scale_max = 1.2f;
    private float inertia_min = .07f;
    private float inertia_max = .12f;


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
        Debug.Log(isChild);

        //hook into the audio souce manager
        sound_source = GameObject.Find("Manager_Audio").GetComponent<AudioSource>();

        

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
        randomY = Random.Range(userY + 5, userY + 10);


        //flip for Z pos
        randomFlip = Random.Range(0, 2);

        if (randomFlip == 1)
            randomZ = Random.Range(userZ - 10, userZ - 20);
        else
            randomZ = Random.Range(userZ + 10, userZ + 20);


        //assign random size of asteroid
        float randomScale = Random.Range(scale_min, scale_max);


        //If we are not a child, we need to make all of these.
        //If we are a child, we should have inherited all of these
        if(!isChild)
        {
            //apply the randoms to the asteroid
            inertia = Random.Range(inertia_min, inertia_max);
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
        
    }

    // Update is called once per frame
    void Update()
    {

        //Update the time alive
        timeAlive = (int)(Time.time - timeStart);


        //move forward over time
        transform.position += transform.forward * inertia;
        if (timeAlive >= lifeSpan)
            selfDestruct(false);
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
    public void selfDestruct(bool fracture)
    {

        if (fracture)
            Fracture();

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
            ui_manager.GetComponent<UI_manager>().highscore += score;

            //TODO
            //rectify this double explosion
            if(!dying)
            {
                //pick a random explosion sound and play it
                int pick = Random.Range(0, 3);
                //TODO
                //sounds 0 and 2 are too soft
                sound_source.PlayOneShot(asteroid_sound[1]);

                dying = true;

                //check to see if fracturable
                if (transform.localScale.x / 2 > scale_min)
                    selfDestruct(true);
                else
                    selfDestruct(false);

            }
        }

        

    }

    private void OnTriggerEnter(Collider collision)
    {
        //hook the user
        var usr = user.GetComponent<User>();
        usr.hit_time = Time.time;

        //detect if hit was on plater
        if (collision.gameObject.CompareTag("player"))
        {
            //cooldown is over baby
            if (usr.hit_time - user.GetComponent<User>().last_hit_time > .5)
            {
                //assign latest hit time relative to the game start
                usr.last_hit_time = user.GetComponent<User>().hit_time;
                usr.TookHit();
            }
        }
    }

    private void Fracture()
    {
        var child1 = Instantiate(asteroid_source);
        var child2 = Instantiate(asteroid_source);
        child1.name = "child1";
        child2.name = "child1";

        //hacky way of inhereting all of the old vars from the parent asteroid
        child1.GetComponent<Asteroid>().inertia = inertia;
        child1.GetComponent<Asteroid>().isChild = true;

        //slightly move new asteroids away from eachother
        child1.transform.position = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
        child1.transform.rotation = transform.rotation;
        child1.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2);
        //child1.transform.localRotation = new Quaternion(transform.rotation.x, transform.rotation.y + 20f, transform.rotation.z, 1);

        child2.GetComponent<Asteroid>().inertia = inertia;
        child2.GetComponent<Asteroid>().isChild = true;

        //slightly move new asteroids away from eachother
        child2.transform.position = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
        child2.transform.position = transform.position;
        child2.transform.rotation = transform.rotation;
        child2.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2);
        //child2.transform.localRotation = new Quaternion(transform.rotation.x, transform.rotation.y - 20f, transform.rotation.z, 1);

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
