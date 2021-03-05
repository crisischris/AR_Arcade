using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public Logic_Manager logicManager;
    private AudioSource source;
    public Game_Manager gameManager;
    public Transform arena;
    public AudioClip[] clips;


    // Velocity Vector
    public Vector3 velocity;
    [Range(0,1)]
    public float speed = 0.01f;
    public Vector3 Ball_Starting_Position;

    private float z;
    private float x;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ball Start");
        //Vector3 newPosition = transform.position;
        //newPosition.y = arena.position.y;
        //transform.position = newPosition;
        //Invoke("ResetBall", 5f);
        source = gameObject.GetComponent<AudioSource>();
    }

    public void ResetBall()
    {
        // Reset Position
        getBallStartPosition();
        if (DidPlayerScore())
        {
            z = 1;
            SetDidPlayerScore();
        }
        else if (DidAIScore())
        {
            Debug.Log(DidAIScore());
            z = -1;
            SetDidAiScore();
            Debug.Log(DidAIScore());
        }
        else 
        { 
        // Want it to move either 1 or -1 on z axis
        // Random int between 0 and 1, multiply by 2 it will be either 0 or 2, -1 it will be -1 or 1
        z = Random.Range(0, 2) * 2f - 1f;
        }
        // Dont want it to come straight at player 
        x = Random.Range(0, 2) * 2f - 1f * Random.Range(0.2f, 1f);
        velocity = new Vector3(x, 0, z);
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = velocity.normalized * speed;
        transform.position += velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.transform.name)
        {
            
            case "Right Bounding Wall":
            case "Left Bounding Wall":
                //play the bounce sounnd
                source.PlayOneShot(clips[0]);

                // When hitting bounding walls ball will go opposite direction it was going
                velocity.x *= -1f;
                return;
            case "Player Score Wall":
            case "Opp Score Wall":
                //play the bounce sounnd
                source.PlayOneShot(clips[1]);
                ResetBall();
                //Invoke("ResetBall", 3);
                //gameObject.SetActive(false);
                //logicManager.BallResetCountdown();
                logicManager.score(collision.transform.name);
                Debug.Log("Point Scored");
                if (logicManager.ai_score >= 11)
                {
                    gameManager.EndGame();
                    //return;
                }
                /* We decided for player to keep playing so they can achieve a high score
                else if (logicManager.player_score >=11)
                {
                    gameManager.EndGame();
                }
                */
                return;
            case "Player Paddle":
            case "Opp Paddle":
                //play the bounce sounnd
                source.PlayOneShot(clips[0]);
                velocity.z *= -1f;
                return;

        }
        
    }


    void destroyGameObject()
    {
        Destroy(gameObject);
    }

    void getBallStartPosition()
    {
        GameObject gm = GameObject.Find("Game_Manager");
        Game_Manager tempGMVar = gm.GetComponent<Game_Manager>();
        transform.position = tempGMVar.ballStartPosition;
    }
    private bool DidPlayerScore()
    {
        GameObject lm = GameObject.Find("Logic_Manager");
        Logic_Manager tempLMVar = lm.GetComponent<Logic_Manager>();
        return tempLMVar.player_scored;
    }
    private bool DidAIScore()
    {
        GameObject lm = GameObject.Find("Logic_Manager");
        Logic_Manager tempLMVar = lm.GetComponent<Logic_Manager>();
        return tempLMVar.ai_scored;
    }
    void SetDidPlayerScore()
    {
        GameObject lm = GameObject.Find("Logic_Manager");
        Logic_Manager tempLMVar = lm.GetComponent<Logic_Manager>();
        tempLMVar.player_scored = false;
    }
    void SetDidAiScore()
    {
        GameObject lm = GameObject.Find("Logic_Manager");
        Logic_Manager tempLMVar = lm.GetComponent<Logic_Manager>();
        tempLMVar.ai_scored = false;
    }
}
