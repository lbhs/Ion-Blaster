/*
 * GameFlowController.cs
 * Gavin Gee
 * 
 * This script handles the flow of the game in the main gameplay scenes (`main`, 
 * `Level 2`, `Level 3`, `Level 4`, `Level 5`)
 * 
 * It also keeps track of the players current score and life. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController main;
    private int playerLifeCounter;
    public Text LifeText;
    public Text ScoreText;
    private int buildIndex;

    private int score;
    private int scoreGoal;

    public AudioSource correct;
    public AudioSource incorrect;

    public Fader fader;

    private bool ending = false;
    public bool endless = false;


    void Start()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        score = ScorePackage.main.startingScore;
        ScoreText.text = "Score: " + score.ToString();

        SetScoreGoal();
        main = this;
        playerLifeCounter = 3;
        fader = GameObject.Find("Fader").GetComponent<Fader>();
    }

    public void EditLife(int i)
    {
        if (!ending)
        {
            playerLifeCounter += i;
            PlaySound(false);
            if (playerLifeCounter < 1)
            {
                LifeText.text = "Lives: " + playerLifeCounter.ToString();
                EndGame();
                ending = true;
            }
            else
            {
                LifeText.text = "Lives: " + playerLifeCounter.ToString();
            }
        }
    }

    public void EditScore(int i, bool sound = true)
    {
        if (!ending)
        {
            score += i;

            if (sound)
            {
                PlaySound(true);
            }
            if (score >= scoreGoal && !endless)
            {
                ScoreText.text = "Score: " + score.ToString();
                NextScene();
                ending = true;
            }
            else
            {
                ScoreText.text = "Score: " + score.ToString();
            }
        }
    }

    private void EndGame()
    {
        ScorePackage.main.currentScore = score;
        ScorePackage.main.lastLevel = buildIndex;
        fader.FadeToEnd();
    }

    private void NextScene()
    {
        ScorePackage.main.currentScore = score;
        ScorePackage.main.startingScore = score;
        fader.FadeSceneChange();
        DontDestroyOnLoad(ScorePackage.main);
    }

    public void BeginGame()
    {
        IonSpawner.main.Setup(70 - 3 * (buildIndex - 1)); 
        IonSpawner.main.Activate();
    }

    public void PlaySound(bool c)
    {
        if (c)
        {
            correct.Play();
        }
        else
        {
            incorrect.Play();
        }
    }

    private void SetScoreGoal()
    {
        switch (buildIndex)
        {
            case 3:
                scoreGoal = 5;
                return;
            case 5:
                scoreGoal = 10;
                return;
            case 7:
                scoreGoal = 15;
                return;
            case 9:
                scoreGoal = 20;
                return;
            default:
                endless = true;
                return;
        }
    }
}
