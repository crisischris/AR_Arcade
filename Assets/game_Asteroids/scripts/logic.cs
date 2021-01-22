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
        user = GameObject.Find("AR Session Origin");
    }

    // Update is called once per frame
    void Update()
    {
        //DestroyOldAsteroids();

        userX = user.transform.position.x;
        userY = user.transform.position.y;
        userZ = user.transform.position.z;

        //time = (int)Time.time;

        //makeshift counter
        //TODO
        //better counter logic?
        if (counter >= 120)
        {
            counter = 0;
            spawnAsteroid();
        }

        //NO
        //StartCoroutine(wait(2));
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
        //asteroidList.Add(asteroid);
    }


    public void Pause()
    {
        //Time.timeScale = 0;
        //TODO
        //figure out how to stop asteroids
    }
    //OBSOLETE
    /*
    private void DestroyOldAsteroids()
    {
        for(int i = 0; i < asteroidList.Count; i++)
        {
            var script = asteroidList[i].GetComponent<asteroid>();

            if (script.getTimeAlive() >= script.getLifeSpan())
            {
                script.selfDestruct();
                asteroidList.Remove(asteroidList[i]);
            }
        }
    }
    */

    //IEnumerator wait(int waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //}
}
