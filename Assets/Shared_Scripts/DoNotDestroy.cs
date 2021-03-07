using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoNotDestroy : MonoBehaviour
{
    static int times_loaded = 0;
    private AudioSource source;
    public AudioClip[] clips;
    public bool asteroidsTutorial = false;
    public bool pongTutorial = false;

    private void Awake()
    {
        if(times_loaded == 0)
            DontDestroyOnLoad(gameObject);
        times_loaded++;
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("times_loaded");
        Debug.Log(times_loaded);

        var sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);

        //asteroids bg music
        if ((sceneName == "Asteroids" && source.clip.name != "Asteroids") || (sceneName == "Tutorial" && source.clip.name != "Asteroids"))
            PlayAsteroidsTheme();
        else if (sceneName == "Pong" && source.mute == false)
            PlayPongTheme();
        else if(sceneName == "Launch_screen" && (!source.clip || source.clip.name != "Launch"))
            PlayLaunchTheme();
    }

    public void PlayButtonUI()
    {
        source.PlayOneShot(clips[2]);
    }

    private void PlayAsteroidsTheme()
    {
        source.clip = clips[1];
        source.Play();
        source.loop = true;
        source.mute = false;
    }

    private void PlayPongTheme()
    {
        source.clip = null;
        source.mute = true;
    }

    private void PlayLaunchTheme()
    {
        source.clip = clips[0];
        source.Play();
        source.loop = true;
        source.mute = false;
    }

    public bool isAsteroidsTutorial()
    {
        if (asteroidsTutorial == true)
            return true;
        else
            return false;
    }
    public bool isPongTutorial()
    {
        if (pongTutorial == true)
            return true;
        else
            return false;
    }

}