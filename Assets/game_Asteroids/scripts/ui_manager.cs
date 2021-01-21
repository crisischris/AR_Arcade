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

    private string string_amount_lives;

    private GameObject user;
    private int lives_left;

    public int highscore = 0;
    private static int x_padding = 325;
    private static int y_padding_score = 225;

    private int asteroid_count;
  

    private static int y_padding_lives = y_padding_score + 100;
    private static int y_padding_asteroids_count = y_padding_lives + 100;
    private static string life_symbol = "▐";

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

        switch (lives_left)
        {
            case 3:
                string_amount_lives = three_lives;
                break;
            case 2:
                string_amount_lives = two_lives;
                break;
            case 1:
                string_amount_lives = one_life;
                break;
        }
        tmp_high_score.text = highscore.ToString();
        lives.text = string_amount_lives;
        asteroids_count.text = asteroid_count.ToString();
    }
}
