using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject user;
    private int time_tick;

    // Start is called before the first frame update
    void Start()
    {
        time_tick = 0;
        user = GameObject.Find("AR Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (time_tick > 180)
        {
            Spawn();
            time_tick = 0;

        }


        time_tick++;
    }


    private void Spawn()
    {
        //GameObject a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //a.AddComponent < Asteroid(0, 0, 0) > ();
        //var a = new Asteroid(5, 0, 0);
        //a.name = "test_asteroid";
        //a.transform.position = user.transform.position;
        //a.transform.position = new Vector3(a.transform.position.x + 5, a.transform.position.y, a.transform.position.z);
    }
}
