using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    //THIS IS BAD STYLE, i'm packing all into this script when I should use more OOP

    //TODO
    //make an asteroid class with the proper vars

    //probably don't need these public
    public int counter;
    private GameObject user;

    public static bool GlobalGameOverState = false;

    //this is to keep track of our asteroids
    //public List<GameObject> asteroidList = new List<GameObject>();
    public GameObject asteroid;


    public float speed = 1f;


    //private vars
    private float userX;
    private float userY;
    private float userZ;

    // Start is called before the first frame update
    void Start()
    {
        //reset the gameover state
        GlobalGameOverState = false;

        //hook the user
        user = GameObject.Find("AR Session Origin");
    }

    // Update is called once per frame
    void Update()
    {
        userX = user.transform.position.x;
        userY = user.transform.position.y;
        userZ = user.transform.position.z;

        //makeshift counter
        //TODO
        //better counter logic?

        //check the delta frames and also the gameover state
        if (counter >= 120 && !GlobalGameOverState)
        {
            counter = 0;
            spawnAsteroid();
        }

        //tick the frame counter
        counter++;
    }

    //TODO
    //want to pool a whole bunch of asteroids I presume so we're not creating and destroying so many
    //or maybe not since they have to break apart.
    //This function spawns the asteroid and slings it
    //TODO sling the asteroid
    private void spawnAsteroid()
    {
        var curAsteroid = Instantiate(asteroid);
        curAsteroid.name = "asteroid";
    }

    //IEnumerator wait(int waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //}
}
