﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    bool game_ended = false;
    public bool game_started = false;
    //public Ball ball;
    public GameObject endGameContainer;
    public GameObject CountDown;
    public GameObject Ball;
    public Logic_Manager logic_Manager;
    public Vector3 ballStartPosition;
    public GameObject ArenaSlider;
    public int highScore = 0;

   

    public void StartGame()
    {
        Debug.Log("StartGameFunctionCalled");
        CountDown.gameObject.SetActive(true);
        //GameObject.Find("Arena").GetComponent<Arena_Controls>().enabled = false;
        ArenaSlider.gameObject.SetActive(false);
        logic_Manager.BeginGameStartCountDown();
        ballStartPosition = Ball.transform.position;
        game_started = true;
        //Ball.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        if (game_ended == false)
        {
            game_ended = true;
            //compare score to high score
            int highScore = PlayerPrefs.GetInt("Pong_high_score", 0);
            //set the new highscore
            if (logic_Manager.player_score > highScore)
                PlayerPrefs.SetInt("Pong_high_score", logic_Manager.player_score);
            Debug.Log("Game OVER");
            GameObject.Find("Ball").SetActive(false);
            GameObject.Find("Logic_Manager").GetComponent<Logic_Manager>().WinnerText();
            endGameContainer.SetActive(true);
        }
        
    }

}
