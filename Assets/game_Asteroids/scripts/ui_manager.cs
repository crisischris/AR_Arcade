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
    public Text highScoreText;
    public Text finalScoreText;
    public Text liveScoreText;
    public Text lives;
    public Text AR_session_state;
    public Text gameOver;
    public Button button_playAgain;
    public Button button_exit;

    /// <summary>
    /// The radar UI
    /// </summary>
    public GameObject Radar_UI;
    public List<GameObject> radars = new List<GameObject>();

    private int side_padding = 40;
    private int top__bottom_padding  = 40;

    //TODO
    //some sort of adaptive number here
    //that is responsive to the actual device
    private int distance_apart = 200;

    private float idle_alpha = 70f/255f;



    //////
    private string string_amount_lives;

    private GameObject user;
    private int lives_left;

    public int score = 0;
    private static int x_padding = 325;
    private static int y_padding_score = 300;

   

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
        liveScoreText.transform.position = new Vector2(x_padding, Screen.height - y_padding_score);
        lives.transform.position = new Vector2(x_padding, Screen.height - y_padding_lives);
        AR_session_state.transform.position = new Vector2(Screen.width / 2, 100);


        gameOver.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        button_playAgain.gameObject.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - 200);
        button_exit.gameObject.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - 400);


        TurnOffGameOverUI();

    }

    // Start is called before the first frame update
    void Start()
    {
        //hook into radar
        SetupRadar();

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

        liveScoreText.text = score.ToString();
        lives.text = string_amount_lives;

        //reset the radar bar colors if not being raycasted
        foreach(GameObject r in radars)
        {
            var image = r.GetComponent<Image>();
            var curColor = image.color;
            image.color = new Color(curColor.r, curColor.g, curColor.b, idle_alpha);
            //Debug.Log("curr color:" + curColor.ToString());
        }
    }



    public void GameOver()
    {

        //turn off / on UI
        TurnOffUI();
        TurnOnGameOverUI();

        //compare score to high score
        int highScore = PlayerPrefs.GetInt("Asteroids_high_score", 0);

        //set the new highscore
        if (score > highScore)
            PlayerPrefs.SetInt("Asteroids_high_score", score);

        //arrange high score to center
        finalScoreText.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 + 200);
        finalScoreText.alignment = TextAnchor.MiddleCenter;
        finalScoreText.text = "score: " + score.ToString();

        highScoreText.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 + 300);
        highScoreText.alignment = TextAnchor.MiddleCenter;
        highScoreText.text = "high score: " + highScore.ToString();
    }

    //turn of extranious UI for gameover
    private void TurnOffUI()
    {
        lives.gameObject.SetActive(false);
        liveScoreText.gameObject.SetActive(false);
        AR_session_state.gameObject.SetActive(false);
        Radar_UI.SetActive(false);

    }

    private void TurnOffGameOverUI()
    {
        gameOver.gameObject.SetActive(false);
        button_playAgain.gameObject.SetActive(false);
        button_exit.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
    }

    private void TurnOnGameOverUI()
    {
        //turn on game over and nav buttons
        gameOver.gameObject.SetActive(true);
        button_playAgain.gameObject.SetActive(true);
        button_exit.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        finalScoreText.gameObject.SetActive(true);
       
    }

    private void TurnOnPlayUI()
    {
        lives.gameObject.SetActive(true);
        liveScoreText.gameObject.SetActive(true);
        AR_session_state.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        //reset the static count of asteroids
        Asteroid.ResetCount();


        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ExitToHome()
    {
        SceneManager.LoadScene(0);
    }


    //This function attempts to resize the radar in a device agnostic way.
    //the basic assumption is 16:9 but should extend to others (18:9)
    private int[] CalculateScreenRatios()
    {
        //ret[0] == top padding
        //ret[1] == bottom padding
        //ret[2] == side padding
        //ret[3] == spacing between radar bars
        //ret[4] == side bar y value
        //ret[5] == top / bottom bar x value
        int[] ret = new int[6];

        int sHeight = Screen.height;
        int sWidth = Screen.width;

        //account for the stupid bar on screen at the top of the mobile phones 
        int sidePadding = sWidth / 12;
        int bottomPadding = side_padding * 2;
        int topPadding = sHeight - sidePadding * 2;
        int spacing = topPadding / 14;

        //overall height minus 7x spaces / 8 bars
        int sideBarSize = (topPadding - (spacing * 4)) / 5;

        //get width between side spaces - 3 spaces in between 4 bars / 4
        int topBottomBarSize = ((sWidth - sidePadding * 2) - spacing * 2) / 3;

        ret[0] = topPadding;
        ret[1] = bottomPadding;
        ret[2] = sidePadding;
        ret[3] = spacing;
        ret[4] = sideBarSize;
        ret[5] = topBottomBarSize;

        return ret;
    }
    //This function sets up the radar. It has a lot of manual setup but should extent
    //to all mobile devices nicely
    private void SetupRadar()
    {
        //structure of the return in the CalcScreen function
        //ret[0] == top padding
        //ret[1] == bottom padding
        //ret[2] == side padding
        //ret[3] == spacing between radar bars
        //ret[4] == side bar y value
        //ret[5] == top / bottom bar x value
        int [] screenValues = CalculateScreenRatios();



        Radar_UI.SetActive(true);

        //Set up the front radar chunk
        GameObject F1 = GameObject.Find("F1_UI");
        //position the bar at side padding + half bar length, top padding
        F1.transform.position = new Vector2(Screen.width / 2 - screenValues[3] - screenValues[5], screenValues[0]);
        radars.Add(F1.gameObject);

        GameObject F2 = GameObject.Find("F2_UI");
        F2.transform.position = new Vector2(Screen.width / 2, screenValues[0]);
        radars.Add(F2);

        GameObject F3 = GameObject.Find("F3_UI");
        F3.transform.position = new Vector2(Screen.width / 2 + screenValues[3] + screenValues[5], screenValues[0]);
        radars.Add(F3);


        //Set up the back radar chunk
        GameObject B1 = GameObject.Find("B1_UI");
        //position the bar at side padding + half bar length, top padding
        B1.transform.position = new Vector2(Screen.width / 2 - screenValues[3] - screenValues[5], screenValues[1]);
        radars.Add(B1.gameObject);

        GameObject B2 = GameObject.Find("B2_UI");
        B2.transform.position = new Vector2(Screen.width / 2, screenValues[1]);
        radars.Add(B2);

        GameObject B3 = GameObject.Find("B3_UI");
        B3.transform.position = new Vector2(Screen.width / 2 + screenValues[3] + screenValues[5], screenValues[1]);
        radars.Add(B3);


        //Set up the right radar chunk
        //set middle first to key off of
        GameObject R3 = GameObject.Find("R3_UI");
        R3.transform.position = new Vector2(Screen.width - screenValues[2] / 2, screenValues[0] / 2 + screenValues[1] / 2);
        radars.Add(R3);

        GameObject R2 = GameObject.Find("R2_UI");
        R2.transform.position = new Vector2(Screen.width - screenValues[2] / 2, R3.transform.position.y + screenValues[3] / 2 + screenValues[4]);
        radars.Add(R2);

        GameObject R1 = GameObject.Find("R1_UI");
        R1.transform.position = new Vector2(Screen.width - screenValues[2] / 2, R2.transform.position.y + screenValues[3] / 2 + screenValues[4]);
        radars.Add(R1);

        GameObject R4 = GameObject.Find("R4_UI");
        R4.transform.position = new Vector2(Screen.width - screenValues[2] / 2, R3.transform.position.y - screenValues[3] / 2 - screenValues[4]);
        radars.Add(R4);

        GameObject R5 = GameObject.Find("R5_UI");
        R5.transform.position = new Vector2(Screen.width - screenValues[2] / 2, R4.transform.position.y - screenValues[3] / 2 - screenValues[4]);
        radars.Add(R5);


        //Set up the left radar chunk
        //set middle first to key off of
        GameObject L3 = GameObject.Find("L3_UI");
        L3.transform.position = new Vector2(screenValues[2] / 2, screenValues[0] / 2 + screenValues[1] / 2);
        radars.Add(L3);

        GameObject L2 = GameObject.Find("L2_UI");
        L2.transform.position = new Vector2(screenValues[2] / 2, R3.transform.position.y + screenValues[3] / 2 + screenValues[4]);
        radars.Add(L2);

        GameObject L1 = GameObject.Find("L1_UI");
        L1.transform.position = new Vector2(screenValues[2] / 2, R2.transform.position.y + screenValues[3] / 2 + screenValues[4]);
        radars.Add(L1);

        GameObject L4 = GameObject.Find("L4_UI");
        L4.transform.position = new Vector2(screenValues[2] / 2, R3.transform.position.y - screenValues[3] / 2 - screenValues[4]);
        radars.Add(L4);

        GameObject L5 = GameObject.Find("L5_UI");
        L5.transform.position = new Vector2(screenValues[2] / 2, R4.transform.position.y - screenValues[3] / 2 - screenValues[4]);
        radars.Add(L5);
    }
}
