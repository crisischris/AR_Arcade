using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// This class handles all of the visible UI on screen.  This UI should be
/// device agnostic as it is using the device dimensions to arrange UI.  This
/// class also handles the button clicks of the menu.
/// </summary>
public class UI_manager : MonoBehaviour
{
    //TODO
    //Do we really want all of these publics or do we want on start to collect necessary objects?
    public bool TrackingState;
    public Text tmp_high_score;
    public Text lives;
    public Text debug_asteroids_count;
    public Text AR_session_state;
    public Text gameOver;
    public Button button_playAgain;
    public Button button_exit;



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

    //private string one_life = life_symbol;
    //private string two_lives = life_symbol + ' ' + life_symbol;
    //private string three_lives = life_symbol + ' ' + life_symbol + ' ' + life_symbol;


    //TODO
    //remove functions we don't use included start

    void Awake()
    {
        tmp_high_score.transform.position = new Vector2(x_padding, Screen.height - y_padding_score);
        lives.transform.position = new Vector2(x_padding, Screen.height - y_padding_lives);
        debug_asteroids_count.transform.position = new Vector2(x_padding, Screen.height - y_padding_asteroids_count);
        AR_session_state.transform.position = new Vector2(Screen.width / 2, 100);


        gameOver.transform.position = new Vector2(Screen.width/2, Screen.height/2);
        button_playAgain.gameObject.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - 200);
        button_exit.gameObject.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - 400);


        TurnOffGameOverUI();
   
    }

    // Start is called before the first frame update
    void Start()
    {
        //hook into the user
        user = GameObject.Find("AR Camera");

        //turn on the UI
        TurnOnPlayUI();
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG AR session state
        var state = ARSession.state;
        if (TrackingState)
            AR_session_state.text = "tracking: " + state.ToString();
        else
            AR_session_state.text = "";


        //get static count of asteroids
        asteroid_count = Asteroid.GetCount();


        lives_left = user.GetComponent<User>().lives;
        string_amount_lives = "";

        //build the lives left string
        for (int i = 0; i < lives_left; i++)
            string_amount_lives += life_symbol;

        
        tmp_high_score.text = highscore.ToString();
        lives.text = string_amount_lives;
        debug_asteroids_count.text = asteroid_count.ToString();
    }


   
    public void GameOver()
    {
        //turn off / on UI
        TurnOffUI();
        TurnOnGameOverUI();

        //arrange high score to center
        tmp_high_score.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 + 100);
        tmp_high_score.alignment = TextAnchor.MiddleCenter;
    }

    //turn of extranious UI for gameover
    private void TurnOffUI()
    {
        lives.gameObject.SetActive(false);
        debug_asteroids_count.gameObject.SetActive(false);

    }

    private void TurnOffGameOverUI()
    {
        gameOver.gameObject.SetActive(false);
        button_playAgain.gameObject.SetActive(false);
        button_exit.gameObject.SetActive(false);
    }

    private void TurnOnGameOverUI()
    {
        //turn on game over and nav buttons
        gameOver.gameObject.SetActive(true);
        button_playAgain.gameObject.SetActive(true);
        button_exit.gameObject.SetActive(true);
    }

    private void TurnOnPlayUI()
    {
        lives.gameObject.SetActive(true);
        tmp_high_score.gameObject.SetActive(true);
        debug_asteroids_count.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        //reset the static count of asteroids
        Asteroid.ResetCount();

        
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void  ExitToHome()
    {
        SceneManager.LoadScene(0);
    }
}
