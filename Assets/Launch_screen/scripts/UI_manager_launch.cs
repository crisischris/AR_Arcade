using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_manager_launch : MonoBehaviour
{
    //this object should be shared across all Scenes
    public GameObject DoNotDestroy;

    public Text Title;
    public Text highScoreTextAsteroids;
    public Text highScoreTextPong;
    public Text createdBy;

    public Button asteroidsButton;
    public Button pongButton;

    private AudioSource source;
    public AudioClip buttonClick;
    private int hs_Asteroids = 0, hs_Pong = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        //lock the orientation
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start()
    {
        //DoNotDestroy = GameObject.Find("DontDestroyOnLoad");
        DoNotDestroy = GameObject.Find("DoNotDestroy");
        source = GameObject.Find("Manager_Audio").GetComponent<AudioSource>();
        //query the existing HS on the device if available, defaul = 0;
        hs_Asteroids = PlayerPrefs.GetInt("Asteroids_high_score", 0);
        hs_Pong = PlayerPrefs.GetInt("Pong_high_score", 0);

        asteroidsButton.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        pongButton.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - (Screen.height / 5));

        //position the UI relative to the screen resolution
        PositionUI();
    }


    // Update is called once per frame
    void Update()
    {
       
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
    public void PlayAsteroids()
    {
        DoNotDestroy.GetComponent<DoNotDestroy>().PlayButtonUI();
        SceneManager.LoadScene("Asteroids");
    }

    //This method loads the scene to Pong
    public void PlayPong()
    {
        DoNotDestroy.GetComponentInChildren<DoNotDestroy>().PlayButtonUI();
        SceneManager.LoadScene("Pong");
    }
}
