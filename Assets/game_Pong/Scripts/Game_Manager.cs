using System.Collections;
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
            Debug.Log("Game OVER");
            GameObject.Find("Ball").SetActive(false);
            endGameContainer.SetActive(true);
        }
        
    }

}
