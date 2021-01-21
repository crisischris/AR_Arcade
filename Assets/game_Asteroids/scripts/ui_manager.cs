using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_manager : MonoBehaviour
{
    //TODO
    //Do we really want all of these publics or do we want on start to collect necessary objects?

    public Text tmp_high_score;
    public Text lives;
    public Text asteroids_count;
    public Text gameOver;

    private string string_amount_lives;

    private GameObject user;
    private int lives_left;

    public int highscore = 0;
    private static int x_padding = 325;
    private static int y_padding_score = 225;

    private int asteroid_count;
  

    private static int y_padding_lives = y_padding_score + 100;
    private static int y_padding_asteroids_count = y_padding_lives + 100;
    private static string life_symbol = "▐ ";

    private string one_life = life_symbol;
    private string two_lives = life_symbol + ' ' + life_symbol;
    private string three_lives = life_symbol + ' ' + life_symbol + ' ' + life_symbol;


    //TODO
    //remove functions we don't use included start

    void Awake()
    {
        tmp_high_score.transform.position = new Vector2(x_padding, Screen.height - y_padding_score);
        lives.transform.position = new Vector2(x_padding, Screen.height - y_padding_lives);
        asteroids_count.transform.position = new Vector2(x_padding, Screen.height - y_padding_asteroids_count);
        gameOver.transform.position = new Vector2(Screen.width/2, Screen.height/2);
        gameOver.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //hook into the user
        user = GameObject.Find("AR Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //get static count of asteroids
        asteroid_count = Asteroid.count;

        lives_left = user.GetComponent<User>().lives;
        string_amount_lives = "";
        switch (lives_left)
        {
            case 8:
                for (int i = 0; i < 8; i++)
                    string_amount_lives += life_symbol;
                break;
            case 7:
                for (int i = 0; i < 7; i++)
                    string_amount_lives += life_symbol;
                break;
            case 6:
                for (int i = 0; i < 6; i++)
                    string_amount_lives += life_symbol;
                break;
            case 5:
                for (int i = 0; i < 5; i++)
                    string_amount_lives += life_symbol;
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                    string_amount_lives += life_symbol;
                break;
            case 3:
                for (int i = 0; i < 3; i++)
                    string_amount_lives += life_symbol;
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                    string_amount_lives += life_symbol;
                break;
            case 1:
                string_amount_lives += life_symbol;
                break;
        }
        tmp_high_score.text = highscore.ToString();
        lives.text = string_amount_lives;
        asteroids_count.text = asteroid_count.ToString();
    }


    //turn of extranious UI for gameover
    private void turnOffUI()
    {
        lives.gameObject.SetActive(false);
        asteroids_count.gameObject.SetActive(false);
        
    }
    public void GameOver()
    {

        //TODO
        //persist High score if greater than starting HS
        GameObject.Find("Manager_Logic").GetComponent<Logic>().Pause();
        
        //turn off un-needed UI
        turnOffUI();

        //turn on game over
        gameOver.gameObject.SetActive(true);
        tmp_high_score.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 + 100);
        tmp_high_score.alignment = TextAnchor.MiddleCenter;
    }
}
