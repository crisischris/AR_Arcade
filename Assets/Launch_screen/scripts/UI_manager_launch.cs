using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_manager_launch : MonoBehaviour
{
    //this object should be shared across all Scenes
    public GameObject DoNotDestroy;
    public GameObject screen1;
    public Vector2 screen1Starting;
    public GameObject screen2;
    public Vector2 screen2Starting;


    bool isAsteroids = false;
    bool isPong = false;

    public Text Title;
    public Text highScoreTextAsteroids;
    public Text highScoreTextPong;
    public Text createdBy;

    public Button asteroidsButton;
    public Button pongButton;
    public Button playGameButton;
    public Button playTutorialButton;
    public Button backButton;

    private AudioSource source;
    public AudioClip buttonClick;
    private int hs_Asteroids = 0, hs_Pong = 0;

    public bool moveToScreen1 = false;
    public bool moveToScreen2 = false;

    // Start is called before the first frame update

    private void Awake()
    {
        //lock the orientation
        Screen.orientation = ScreenOrientation.Portrait;

        //DoNotDestroy = GameObject.Find("DontDestroyOnLoad");
        DoNotDestroy = GameObject.Find("DoNotDestroy");
        source = GameObject.Find("Manager_Audio").GetComponent<AudioSource>();
        //query the existing HS on the device if available, defaul = 0;
        hs_Asteroids = PlayerPrefs.GetInt("Asteroids_high_score", 0);
        hs_Pong = PlayerPrefs.GetInt("Pong_high_score", 0);

        asteroidsButton.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        pongButton.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - (Screen.height / 5));

        playGameButton.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        playTutorialButton.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - (Screen.height / 10));
        backButton.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - (2 * (Screen.height / 10)));

        //lock the orientation
        Screen.orientation = ScreenOrientation.Portrait;
        screen1 = GameObject.Find("Screen_1");
        screen1Starting = screen1.transform.position;
        screen2 = GameObject.Find("Screen_2");
        screen2.transform.position = new Vector2(screen1.transform.position.x + Screen.width, screen1.transform.position.y);
        screen2Starting = screen2.transform.position;
    }

    void Start()
    {
        //position the UI relative to the screen resolution
        PositionUI();
    }


    // Update is called once per frame
    void Update()
    {

        //raise the UI up and send the displayUp button down
        if (moveToScreen1)
        {
            screen1.transform.position = Vector2.Lerp(screen1.transform.position, new Vector2(screen1Starting.x, screen1Starting.y), Time.deltaTime * 10);
            screen2.transform.position = Vector2.Lerp(screen2.transform.position, new Vector2(screen2Starting.x, screen2Starting.y), Time.deltaTime * 10);
        }

        //move to screen 2
        if (moveToScreen2)
        {
            screen1.transform.position = Vector2.Lerp(screen1.transform.position, new Vector2(screen1Starting.x - Screen.width, screen1Starting.y), Time.deltaTime * 10);
            screen2.transform.position = Vector2.Lerp(screen2.transform.position, new Vector2(screen1Starting.x, screen1Starting.y), Time.deltaTime * 10);

        }
    }


    //This method aligns the UI relative to the user's phone resolution / aspect ratio
    private void PositionUI()
    {
        highScoreTextAsteroids.text = "high score: " + hs_Asteroids.ToString();
        highScoreTextPong.text = "high score: " + hs_Pong.ToString();

        Title.text = "AR ARCADE";
        Title.transform.position = new Vector2(Screen.width / 2, Screen.height - Screen.height / 10);

        createdBy.text = "created by: NewRetro";
        createdBy.transform.position = new Vector2(Screen.width / 2, Screen.height / 12);

        highScoreTextAsteroids.transform.position = new Vector2(asteroidsButton.transform.position.x, asteroidsButton.transform.position.y - 175);
        highScoreTextPong.transform.position = new Vector2(pongButton.transform.position.x, pongButton.transform.position.y - 175);
    }


    //This method loads the scene to Asteroids
    public void MoveToAsteroids()
    {
        PlayOneSound(buttonClick);

        moveToScreen2 = true;
        moveToScreen1 = false;
        isAsteroids = true;
        isPong = false;

    }

    //This method loads the scene to Pong
    public void MoveToPong()
    {
        PlayOneSound(buttonClick);

        moveToScreen2 = true;
        moveToScreen1 = false;
        isAsteroids = false;
        isPong = true;
    }

    public void PlayTutorial()
    {
        PlayOneSound(buttonClick);

        if (isAsteroids)
            DoNotDestroy.GetComponent<DoNotDestroy>().asteroidsTutorial = true;
<<<<<<< HEAD
            DoNotDestroy.GetComponent<DoNotDestroy>().PlayButtonUI();
            SceneManager.LoadScene("Tutorial");
        }
=======
>>>>>>> Pong_Tutorial
        else
            DoNotDestroy.GetComponent<DoNotDestroy>().pongTutorial = true;
<<<<<<< HEAD
            DoNotDestroy.GetComponentInChildren<DoNotDestroy>().PlayButtonUI();
            SceneManager.LoadScene("Tutorial");
        }
=======

        DoNotDestroy.GetComponent<DoNotDestroy>().PlayButtonUI();
        SceneManager.LoadScene("Tutorial");
>>>>>>> Pong_Tutorial
    }

    public void PlayGame()
    {
        //reset the tut. vars 
        DoNotDestroy.GetComponent<DoNotDestroy>().asteroidsTutorial = false;
        DoNotDestroy.GetComponent<DoNotDestroy>().pongTutorial = false;

        PlayOneSound(buttonClick);
        if (isAsteroids)
        {
            DoNotDestroy.GetComponent<DoNotDestroy>().PlayButtonUI();
            SceneManager.LoadScene("Asteroids");
        }
        else
        {
            DoNotDestroy.GetComponentInChildren<DoNotDestroy>().PlayButtonUI();
            SceneManager.LoadScene("Pong");
        }
    }

    public void Back()
    {
        PlayOneSound(buttonClick);

        moveToScreen1 = true;
        moveToScreen2 = false;
    }

    public void PlayOneSound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    //public void LerpUILeft()
    //{
    //    location.transform.position = new Vector3(location.transform.position.x, location.transform.position.y + .1f, location.transform.position.z);
    //}

    //public void LerpUIRight()
    //{
    //    location.transform.position = new Vector3(location.transform.position.x, location.transform.position.y + .1f, location.transform.position.z);
    //}
}