using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    bool game_ended = false;
    public GameObject endGameContainer;
    public void EndGame()
    {
        if (game_ended == false)
        {
            game_ended = true;
            Debug.Log("Game OVER");
            endGameContainer.SetActive(true);
        }
        
    }
}
