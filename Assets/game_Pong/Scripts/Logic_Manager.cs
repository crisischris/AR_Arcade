﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Logic_Manager : MonoBehaviour
{
    public Text display_player_score;
    public Text display_ai_score;
    public Text display_game_over;

    public int player_score;
    public int ai_score;

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
}

