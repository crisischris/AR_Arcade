using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public GameObject laser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
