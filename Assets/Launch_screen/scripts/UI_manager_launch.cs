using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_manager_launch : MonoBehaviour
{
    //this object should be shared across all Scenes
    public GameObject DoNotDestroy;


    public Text highScoreTextAsteroids;
    public Text highScoreTextPong;

    private AudioSource source;
    public AudioClip buttonClick;


    private int hs_Asteroids = 0, hs_Pong = 0;

    // Start is called before the first frame update
    void Start()
    {
        //DoNotDestroy = GameObject.Find("DontDestroyOnLoad");
        DoNotDestroy = GameObject.Find("DoNotDestroy");
        source = GameObject.Find("Manager_Audio").GetComponent<AudioSource>();
        //query the existing HS on the device if available, defaul = 0;
        hs_Asteroids = PlayerPrefs.GetInt("Asteroids_high_score", 0);
        hs_Pong = PlayerPrefs.GetInt("Pong_high_score", 0);

        highScoreTextAsteroids.text = "high score: " + hs_Asteroids.ToString();
        highScoreTextPong.text = "high score: " + hs_Pong.ToString();

        highScoreTextAsteroids.transform.position = new Vector2(Screen.width / 2, 2 * (Screen.height / 10));
        highScoreTextPong.transform.position = new Vector2(Screen.width / 2, highScoreTextAsteroids.transform.position.y - Screen.height/20);

    }
    // Update is called once per frame
    void Update()
    {
        int a = SceneManager.sceneCountInBuildSettings;
        Debug.Log(a);
        //int l = s.Length;

        //for (int i = 0; i < l; i++)
        //    Debug.Log(s[i].name);

        //Debug.Log(SceneManager.GetActiveScene().name);
        //Debug.Log(SceneManager.GetAllScenes())
    }

    public void PlayAsteroids()
    {
        DoNotDestroy.GetComponent<DoNotDestroy>().PlayButtonUI();
        //DoNotDestroy.GetComponentInChildren<DoNotDestroy>().PlayButtonUI();
        SceneManager.LoadScene("Asteroids");
    }

    public void PlayPong()
    {
        DoNotDestroy.GetComponentInChildren<DoNotDestroy>().PlayButtonUI();
        SceneManager.LoadScene("Pong");
    }
}
