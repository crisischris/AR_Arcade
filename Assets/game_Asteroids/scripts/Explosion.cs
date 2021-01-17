using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //inherit from the asteroid
    public float inertia;

    private float timeStart;
    private int timeAlive;
    private int lifeSpan = 2;


    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.time;
        
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

    //Call this to clean up
    public void selfDestruct()
    {
        Destroy(gameObject);
    }
}
