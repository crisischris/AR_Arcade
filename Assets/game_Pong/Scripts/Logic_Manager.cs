using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Logic_Manager : MonoBehaviour
{
    public Text display_player_score;
    public Text display_ai_score;
    public Text display_game_over;

    public Button begin;

    public int player_score;
    public int ai_score;

    //handles the screen transition object
    public Image transition;
    private float newAlpha;

    public void score(string object_hit)
    {
        switch(object_hit)
        {
            case "Player Score Wall":
                ai_score += 1;
                display_ai_score.text = " AI:" + ai_score;
                return;
            case "Opp Score Wall":
                player_score += 1;
                display_player_score.text = "Player:" + player_score;
                return;
        }
    }
    public void ExitToHome()
    {
        SceneManager.LoadScene("Launch_screen");
    }
    public void PlayAgain()
    {
        
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Awake()
    {
        newAlpha = transition.color.a;
        transition = GameObject.Find("transition").GetComponent<Image>();
    }

    public void Update()
    {
        //change the alpha of the screen transition object
        if (newAlpha > 0)
        {
            newAlpha -= .025f;
            if (newAlpha <= 0)
                transition.gameObject.SetActive(false);

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, newAlpha);
        }
    }
    public void BeginGameButton()
    {
        Debug.Log("Start Button Pressed");
    }
}

