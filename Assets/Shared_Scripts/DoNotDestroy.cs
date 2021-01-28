using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoNotDestroy : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
        var sceneName = SceneManager.GetActiveScene().name;
        //asteroids bg music
        if (sceneName == "Asteroids" && source.clip.name != "Asteroids")
            PlayAsteroidsTheme();
        else if (sceneName == "Pong" && source.clip.name != "Pong")
            PlayPongTheme();

        else if(sceneName == "Launch_screen" && source.clip.name != "Launch")
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
    }

    private void PlayPongTheme()
    {
        source.clip = clips[1];
        source.Play();
    }

    private void PlayLaunchTheme()
    {
        source.clip = clips[0];
        source.Play();
    }

}