using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public GameObject laser;
    public int lives = 3;
    //public bool isTakingHit = false;


    public float hit_time;
    public float last_hit_time = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        //establish time for hit cooldowns
        last_hit_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
            Debug.Log("WE'RE DEAD BABY");
        
    }


    public void shoot()
    {
        GameObject shot = Instantiate(laser);
        shot.transform.position = gameObject.transform.position;
        //adjust the shot down
        shot.transform.position = new Vector3(shot.transform.position.x, shot.transform.position.y - .1f, shot.transform.position.z);
        shot.transform.rotation = gameObject.transform.rotation;
    }
}
