using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_manager_tutorial : MonoBehaviour
{
    //this object should be shared across all Scenes
    public GameObject DoNotDestroy;
    private int side_padding = 40;
    public Image transition;
    private float newAlpha;


    //ASTEROIDS screen
    public GameObject screen1_Asteroids;
    public GameObject screen2_Asteroids;
    public GameObject screen3_Asteroids;
    public GameObject screen4_Asteroids;

    //screen vector2s
    Vector2 screen1_AsteroidsStarting;
    Vector2 screen2_AsteroidsStarting;
    Vector2 screen3_AsteroidsStarting;
    Vector2 screen4_AsteroidsStarting;

    public GameObject Radar_UI;
    public List<GameObject> radars = new List<GameObject>();
    private float idle_alpha = 70f / 255f;

    public Image tut_ar_main_asteroids;
    public Image tut_tap;
    public Image tut_radar_arrow;
    public Image tut_lives_arrow;

    public Text explainer1_asteroids;
    public Text explainer2_asteroids;
    public Text explainer3_asteroids;
    public Text explainer4_asteroids;
    public Text score;
    public Text lives;

    //setting up score / lives UI
    private static int x_padding = 325;
    private static int y_padding_score = 300;
    private static int y_padding_lives = y_padding_score + 100;
    private static int y_padding_asteroids_count = y_padding_lives + 100;
    private static string life_symbol = "▐ ▐ ▐ ▐ ▐ ▐ ▐ ";


    public GameObject screen1_Pong;
    public GameObject screen2_Pong;


    public Image tut_ar_main_pong;
    public Image tut_score_arrow;

    public Text explainer1_pong;
    public Text explainer2_pong;
    public Text playerScore_pong;
    public Text aiScore_pong;

    Vector2 screen1_PongStarting;
    Vector2 screen2_PongStarting;

    public int countPos = 1;


    public bool isAsteroids = false;
    public bool isPong = false;


    public Button backButton;
    public Button nextButton;
    public Button playButton;


    private AudioSource source;
    public AudioClip buttonClick;
    private int hs_Asteroids = 0, hs_Pong = 0;

    public bool moveToAsteroidsScreen1 = false;
    public bool moveToAsteroidsScreen2 = false;
    public bool moveToAsteroidsScreen3 = false;
    public bool moveToAsteroidsScreen4 = false;

    public bool moveToPongScreen1 = false;
    public bool moveToPongScreen2 = false;


    // Start is called before the first frame update

    private void Awake()
    {
        //lock the orientation
        Screen.orientation = ScreenOrientation.Portrait;

        DoNotDestroy = GameObject.Find("DoNotDestroy");
        source = DoNotDestroy.GetComponent<AudioSource>();

        backButton.transform.position = new Vector2(Screen.width / 5, Screen.height / 6);
        nextButton.transform.position = new Vector2(Screen.width - Screen.width / 5, Screen.height / 6);
        playButton.transform.position = new Vector2(Screen.width - Screen.width / 5, Screen.height / 6);
        playButton.gameObject.SetActive(false);

        //lock the orientation
        Screen.orientation = ScreenOrientation.Portrait;

        //transition
        transition = GameObject.Find("transition").GetComponent<Image>();
        newAlpha = transition.color.a;
    }

    void Start()
    {
        //determin which tutorial we are on
        if (DoNotDestroy.GetComponent<DoNotDestroy>().isAsteroidsTutorial())
            isAsteroids = true;
        else
            isPong = true;

        //position the UI relative to the screen resolution
        PositionUI();
        backButton.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
        if(isAsteroids)
        {

            //Very hacky way of moving screen.  could be refactored to simpler
            //raise the UI up and send the displayUp button down
            if (moveToAsteroidsScreen1)
            {
                screen1_Asteroids.transform.position = Vector2.Lerp(screen1_Asteroids.transform.position, new Vector2(screen1_AsteroidsStarting.x, screen1_AsteroidsStarting.y), Time.deltaTime * 10);
                screen2_Asteroids.transform.position = Vector2.Lerp(screen2_Asteroids.transform.position, new Vector2(screen2_AsteroidsStarting.x, screen2_AsteroidsStarting.y), Time.deltaTime * 10);
                screen3_Asteroids.transform.position = Vector2.Lerp(screen3_Asteroids.transform.position, new Vector2(screen3_AsteroidsStarting.x, screen3_AsteroidsStarting.y), Time.deltaTime * 10);
                screen4_Asteroids.transform.position = Vector2.Lerp(screen4_Asteroids.transform.position, new Vector2(screen4_AsteroidsStarting.x, screen4_AsteroidsStarting.y), Time.deltaTime * 10);
            }

            //move to screen 2
            else if (moveToAsteroidsScreen2)
            {
                screen1_Asteroids.transform.position = Vector2.Lerp(screen1_Asteroids.transform.position, new Vector2(screen1_AsteroidsStarting.x - Screen.width, screen1_AsteroidsStarting.y), Time.deltaTime * 10);
                screen2_Asteroids.transform.position = Vector2.Lerp(screen2_Asteroids.transform.position, new Vector2(screen2_AsteroidsStarting.x - Screen.width, screen2_AsteroidsStarting.y), Time.deltaTime * 10);
                screen3_Asteroids.transform.position = Vector2.Lerp(screen3_Asteroids.transform.position, new Vector2(screen3_AsteroidsStarting.x - Screen.width, screen3_AsteroidsStarting.y), Time.deltaTime * 10);
                screen4_Asteroids.transform.position = Vector2.Lerp(screen4_Asteroids.transform.position, new Vector2(screen4_AsteroidsStarting.x - Screen.width, screen4_AsteroidsStarting.y), Time.deltaTime * 10);
            }

            else if (moveToAsteroidsScreen3)
            {
                screen1_Asteroids.transform.position = Vector2.Lerp(screen1_Asteroids.transform.position, new Vector2(screen1_AsteroidsStarting.x - 2 * Screen.width, screen1_AsteroidsStarting.y), Time.deltaTime * 10);
                screen2_Asteroids.transform.position = Vector2.Lerp(screen2_Asteroids.transform.position, new Vector2(screen2_AsteroidsStarting.x - 2 * Screen.width, screen2_AsteroidsStarting.y), Time.deltaTime * 10);
                screen3_Asteroids.transform.position = Vector2.Lerp(screen3_Asteroids.transform.position, new Vector2(screen3_AsteroidsStarting.x - 2 * Screen.width, screen3_AsteroidsStarting.y), Time.deltaTime * 10);
                screen4_Asteroids.transform.position = Vector2.Lerp(screen4_Asteroids.transform.position, new Vector2(screen4_AsteroidsStarting.x - 2 * Screen.width, screen4_AsteroidsStarting.y), Time.deltaTime * 10); 

            }

            else if(moveToAsteroidsScreen4)
            {
                screen1_Asteroids.transform.position = Vector2.Lerp(screen1_Asteroids.transform.position, new Vector2(screen1_AsteroidsStarting.x - 3 * Screen.width, screen1_AsteroidsStarting.y), Time.deltaTime * 10);
                screen2_Asteroids.transform.position = Vector2.Lerp(screen2_Asteroids.transform.position, new Vector2(screen2_AsteroidsStarting.x - 3 * Screen.width, screen2_AsteroidsStarting.y), Time.deltaTime * 10);
                screen3_Asteroids.transform.position = Vector2.Lerp(screen3_Asteroids.transform.position, new Vector2(screen3_AsteroidsStarting.x - 3 * Screen.width, screen3_AsteroidsStarting.y), Time.deltaTime * 10);
                screen4_Asteroids.transform.position = Vector2.Lerp(screen4_Asteroids.transform.position, new Vector2(screen4_AsteroidsStarting.x - 3 * Screen.width, screen4_AsteroidsStarting.y), Time.deltaTime * 10);
            }
        }

        else
        {

            //Very hacky way of moving screen.  could be refactored to simpler
            //raise the UI up and send the displayUp button down
            if (moveToPongScreen1)
            {
                screen1_Pong.transform.position = Vector2.Lerp(screen1_Pong.transform.position, new Vector2(screen1_PongStarting.x, screen1_PongStarting.y), Time.deltaTime * 10);
                screen2_Pong.transform.position = Vector2.Lerp(screen2_Pong.transform.position, new Vector2(screen2_PongStarting.x, screen2_PongStarting.y), Time.deltaTime * 10);
            }

            //move to screen 2
            else if (moveToPongScreen2)
            {
                screen1_Pong.transform.position = Vector2.Lerp(screen1_Pong.transform.position, new Vector2(screen1_PongStarting.x - Screen.width, screen1_PongStarting.y), Time.deltaTime * 10);
                screen2_Pong.transform.position = Vector2.Lerp(screen2_Pong.transform.position, new Vector2(screen2_PongStarting.x - Screen.width, screen2_PongStarting.y), Time.deltaTime * 10);
            }

        }


        //reset the radar bar colors if not being raycasted
        foreach (GameObject r in radars)
        {
            var image = r.GetComponent<Image>();
            var curColor = image.color;
            image.color = new Color(curColor.r, curColor.g, curColor.b, idle_alpha);
        }

        //change the alpha of the screen transition object
        if (newAlpha > 0)
        {
            newAlpha -= .025f;
            if (newAlpha <= 0)
                transition.gameObject.SetActive(false);

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, newAlpha);
        }
    }


    //This method aligns the UI relative to the user's phone resolution / aspect ratio
    private void PositionUI()
    {
        if (isAsteroids)
        {
            screen1_Pong.SetActive(false);
            screen2_Pong.SetActive(false);



            //position all UI relative to screen
            //Screen1
            tut_ar_main_asteroids.transform.position = new Vector2(Screen.width / 2, Screen.height / 1.7f);
            explainer1_asteroids.transform.position = new Vector2(Screen.width / 2, Screen.height / 3.25f);

            //Screen2
            tut_tap.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
            explainer2_asteroids.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - 180);

            //Screen3
            SetupRadar();
            tut_radar_arrow.transform.position = new Vector2(Screen.width / 2, Screen.height - Screen.height / 7 - 50);
            explainer3_asteroids.transform.position = new Vector2(Screen.width / 2, Screen.height / 1.6f);

            //Screen4
            tut_lives_arrow.transform.position = new Vector2(Screen.width / 2, Screen.height - Screen.height / 4 - 50);
            explainer4_asteroids.transform.position = new Vector2(Screen.width / 2, Screen.height / 1.7f);
            score.transform.position = new Vector2(x_padding, Screen.height - y_padding_score);
            lives.transform.position = new Vector2(x_padding, Screen.height - y_padding_lives);
            lives.text = life_symbol;



            //move screens according to their postion using their gameObject empty host
            screen2_Asteroids.transform.position = new Vector2(Screen.width + Screen.width / 2, Screen.height / 2);
            screen3_Asteroids.transform.position = new Vector2(Screen.width * 2 + Screen.width / 2, Screen.height / 2);
            screen4_Asteroids.transform.position = new Vector2(Screen.width * 3 + Screen.width / 2, Screen.height / 2);

            screen1_AsteroidsStarting = screen1_Asteroids.transform.position;
            screen2_AsteroidsStarting = screen2_Asteroids.transform.position;
            screen3_AsteroidsStarting = screen3_Asteroids.transform.position;
            screen4_AsteroidsStarting = screen4_Asteroids.transform.position;
        }
        else
        {
            screen1_Asteroids.SetActive(false);
            screen2_Asteroids.SetActive(false);
            screen3_Asteroids.SetActive(false);
            screen4_Asteroids.SetActive(false);

            //position all UI relative to screen
            //Screen1
            tut_ar_main_pong.transform.position = new Vector2(Screen.width / 2, Screen.height / 1.7f);
            explainer1_pong.transform.position = new Vector2(Screen.width / 2, Screen.height / 3.25f);

            //Screen2
            tut_score_arrow.transform.position = new Vector2(Screen.width / 2, Screen.height - Screen.height / 3f);
            explainer2_pong.transform.position = new Vector2(Screen.width / 2, Screen.height / 2.2f);
            playerScore_pong.transform.position = new Vector2(x_padding, Screen.height - y_padding_score);
            aiScore_pong.transform.position = new Vector2(Screen.width - x_padding, Screen.height - y_padding_score);

            //move screens according to their postion using their gameObject empty host
            screen2_Pong.transform.position = new Vector2(Screen.width + Screen.width / 2, Screen.height / 2);
            screen1_PongStarting = screen1_Pong.transform.position;
            screen2_PongStarting = screen2_Pong.transform.position;
        }
    }

    public void PlayGame()
    {
        PlayOneSound(DoNotDestroy.GetComponent<DoNotDestroy>().clips[3]);
        if (isAsteroids)
        {
            isAsteroids = false;
            SceneManager.LoadScene("Asteroids");
        }
        else
        {
            isPong = false;
            SceneManager.LoadScene("Pong");
        }
    }


    public void Next()
    {
        PlayOneSound(buttonClick);
        countPos++;
        ChangeScreens(countPos);
    }

    public void Back()
    {
        PlayOneSound(buttonClick);
        countPos--;
        ChangeScreens(countPos);
    }
   

    private void ChangeScreens(int countPos)
    {
        if (countPos == 1)
            backButton.gameObject.SetActive(false);
        else
        {
            backButton.gameObject.SetActive(true);
            //prevent buttons from scaling out of sync
            //backButton.GetComponent<Text>().GetComponent<Button_Animation>().t_size = nextButton.GetComponent<Text>().GetComponent<Button_Animation>().t_size;
        }


        if (isAsteroids)
        {
            if (countPos == 4)
            {
                nextButton.gameObject.SetActive(false);
                playButton.gameObject.SetActive(true);
                //prevent buttons from scaling out of sync
                //playButton.GetComponent<Text>().fontSize = backButton.GetComponent<Text>().fontSize;

            }

            else
            {
                nextButton.gameObject.SetActive(true);
                //prevent buttons from scaling out of sync
                //nextButton.GetComponent<Text>().fontSize = backButton.GetComponent<Text>().fontSize;
                playButton.gameObject.SetActive(false);
            }


            switch (countPos)
            {
                case 1:
                    moveToAsteroidsScreen1 = true;
                    moveToAsteroidsScreen2 = false;
                    moveToAsteroidsScreen3 = false;
                    moveToAsteroidsScreen4 = false;
                    break;

                case 2:
                    moveToAsteroidsScreen1 = false;
                    moveToAsteroidsScreen2 = true;
                    moveToAsteroidsScreen3 = false;
                    moveToAsteroidsScreen4 = false;
                    break;

                case 3:
                    moveToAsteroidsScreen1 = false;
                    moveToAsteroidsScreen2 = false;
                    moveToAsteroidsScreen3 = true;
                    moveToAsteroidsScreen4 = false;
                    break;

                case 4:
                    moveToAsteroidsScreen1 = false;
                    moveToAsteroidsScreen2 = false;
                    moveToAsteroidsScreen3 = false;
                    moveToAsteroidsScreen4 = true;
                    break;
            }
        }
        else
        {
            if (countPos == 2)
            {
                nextButton.gameObject.SetActive(false);
                playButton.gameObject.SetActive(true);

            }

            else
            {
                nextButton.gameObject.SetActive(true);
                playButton.gameObject.SetActive(false);
            }

            switch (countPos)
            {
                case 1:
                    moveToPongScreen1 = true;
                    moveToPongScreen2 = false;
                    break;

                case 2:
                    moveToPongScreen1 = false;
                    moveToPongScreen2 = true;
                    break;
            }
        }
    }
   

    public void PlayOneSound(AudioClip clip)
    {
        source.PlayOneShot(clip);
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
        int sideBarSize = (topPadding - (spacing * 2)) / 3;

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
        int[] screenValues = CalculateScreenRatios();



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
        GameObject R2 = GameObject.Find("R2_UI");
        R2.transform.position = new Vector2(Screen.width - screenValues[2] / 2, screenValues[0] / 2 + screenValues[1] / 2);
        radars.Add(R2);

        GameObject R1 = GameObject.Find("R1_UI");
        R1.transform.position = new Vector2(Screen.width - screenValues[2] / 2, R2.transform.position.y + screenValues[3] / 3 + screenValues[4]);
        radars.Add(R1);

        GameObject R3 = GameObject.Find("R3_UI");
        R3.transform.position = new Vector2(Screen.width - screenValues[2] / 2, R2.transform.position.y - screenValues[3] / 3 - screenValues[4]);
        radars.Add(R3);


        //Set up the left radar chunk
        //set middle first to key off of
        GameObject L2 = GameObject.Find("L2_UI");
        L2.transform.position = new Vector2(screenValues[2] / 2, screenValues[0] / 2 + screenValues[1] / 2);
        radars.Add(L2);

        GameObject L1 = GameObject.Find("L1_UI");
        L1.transform.position = new Vector2(screenValues[2] / 2, L2.transform.position.y + screenValues[3] / 3 + screenValues[4]);
        radars.Add(L1);

        GameObject L3 = GameObject.Find("L3_UI");
        L3.transform.position = new Vector2(screenValues[2] / 2, L2.transform.position.y - screenValues[3] / 3 - screenValues[4]);
        radars.Add(L3);
    }
}