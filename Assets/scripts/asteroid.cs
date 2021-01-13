using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public int lifeStage;
    public float inertia;
    public Vector3 pos;
    
    private GameObject user;
    private GameObject obj;
    private float userX;
    private float userY;
    private float userZ;




    // Start is called before the first frame update
    void Start()
    {
        //Establish the user posistion
        user = GameObject.Find("AR Session Origin");
        userX = user.transform.position.x;
        userY = user.transform.position.y;
        userZ = user.transform.position.z;

        //Use user position to randomize spawn location of asteroid
        float randomX = Random.Range(userX - 10, userX + 10);
        float randomY = Random.Range(userY - 10, userY + 10);
        float randomZ = Random.Range(userZ - 10, userZ + 10);
        float randomScale = Random.Range(.1f, 1.3f);

        Debug.Log("pos = " + randomX.ToString() + " " + randomY.ToString() + " " + randomZ.ToString());

        //assign random intertia for spawned astroid
        inertia = Random.Range(.1f, 4f);

        

        //apply the randoms to the asteroid
        obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        obj.transform.position = new Vector3(randomX, randomY, randomZ);

        //TODO
        //We need to rotate the asteriod on spawn to look at or towards the user
        //random noise towards
        //vector math needed here

        //StartSelfDestruct();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.forward;
        Debug.Log("direction to move: " + dir.ToString());
        obj.transform.position += dir * inertia;
    }

    void StartSelfDestruct()
    {
        StartCoroutine(SelfDestruct(5));
        Destroy(this);
    }

    //Delay the destroy 
    private IEnumerator SelfDestruct(int countdown)
    {
        yield return new WaitForSeconds(countdown);
    }
}
