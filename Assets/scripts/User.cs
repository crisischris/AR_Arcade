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
        shot.transform.rotation = gameObject.transform.rotation;
    }
}
