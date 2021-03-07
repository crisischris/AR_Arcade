using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
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
        //check the delta frames and also the gameover state
        if (counter >= 120 && !GlobalGameOverState)
        {
            counter = 0;
            spawnAsteroid();
        }

        //tick the frame counter
        counter++;
    }
    
    private void spawnAsteroid()
    {
        var curAsteroid = Instantiate(asteroid);
        curAsteroid.name = "asteroid";
    }
}
