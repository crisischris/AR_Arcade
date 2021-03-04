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
    public Text winner_text;
    //public Text ball_pos_text;

    public Button begin;

    public int player_score;
    public int ai_score;

    //handles the screen transition object
    public Image transition;
    private float newAlpha;

    public Game_Manager game_Manager;
    public Ball ball;
    //For Countdowns
    public int countDownTime;
    public Text countDownDisplay;
    public Text BallResetDisplay;
    public GameObject BallResetCountDown;
    public bool player_scored = false;
    public bool ai_scored = false;

    void Start()
    {
        
    }
    public void score(string object_hit)
    {
        ai_scored = false;
        player_scored = false;
        switch(object_hit)
        {
            case "Player Score Wall":
                ai_score += 1;
                display_ai_score.text = " AI:" + ai_score;
                ai_scored = true;
                return;
            case "Opp Score Wall":
                player_score += 1;
                display_player_score.text = "Player:" + player_score;
                player_scored = true;
                return;
        }
    }

    public void ballPosY()
    {
        //ball_pos_text.text = "Ball Position: " + transform.localPosition.z;
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
    
    public void BeginGameStartCountDown()
    {
        StartCoroutine(CountdownToStart());
    }
    IEnumerator CountdownToStart()
    {
        while (countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }
        countDownDisplay.text = "GO";
        // Wait 1 second and turn off GO
        yield return new WaitForSeconds(1f);
        countDownDisplay.gameObject.SetActive(false);
        ball.ResetBall();
    }
    public void BallResetCountdown()
    {
        //ball.gameObject.SetActive(false);
        BallResetCountDown.gameObject.SetActive(true);
        StartCoroutine(CountdownToBallReset());
        ball.gameObject.SetActive(true);
    }
    IEnumerator CountdownToBallReset()
    {
        int ResetCountdown = 3;
        while (ResetCountdown > 0)
        {
            BallResetDisplay.text = ResetCountdown.ToString();
            yield return new WaitForSeconds(1f);
            ResetCountdown--;
        }
        BallResetDisplay.text = "GO";
        // Wait 1 second and turn off GO
        yield return new WaitForSeconds(1f);
        BallResetDisplay.gameObject.SetActive(false);
        ball.ResetBall();
    }
    public void BeginGameButton()
    {
        Debug.Log("Start Button Pressed");
        begin.interactable = false;
        begin.gameObject.SetActive(false);
        game_Manager.StartGame();
    }

    public void WinnerText()
    {
        if (ai_score == 11)
            winner_text.text = "AI Knowledge +1";
        else
            winner_text.text = "Victory";
    }
}

