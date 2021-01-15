using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private int lifeSpan = 6;
    private float timeStart;
    private int timeAlive;

    public float speed = .2f;

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
        if (timeAlive >= lifeSpan)
            selfDestruct();


        transform.position += transform.forward * speed;

        //TODO
        //collision detection in asteroid
    }


    //Call this to clean up
    public void selfDestruct()
    {
        Destroy(gameObject);
    }
}
