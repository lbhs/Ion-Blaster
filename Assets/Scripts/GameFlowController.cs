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

    // Life vars
    private int playerLifeCounter;
    public Text LifeText;

    // Score vars
    public Text ScoreText;
    private int score; // the player's current score    
    private int scoreGoal; // the score needed to get to the next level.

    private int buildIndex; // build index of the current scene.

    // the audiosources that handle the correct/incorrect answer sounds.
    public AudioSource correct;
    public AudioSource incorrect;

    // the fader object in this scene
    public Fader fader;

    private bool ending = false; // this bool stops the player from losing life/gaining points while the scene fades.
    public bool endless = false; // this bool is whether the game is in endless mode or not (set by `SetScoreGoal`).

    void Start()
    {
        main = this;

        buildIndex = SceneManager.GetActiveScene().buildIndex;

        score = ScorePackage.main.startingScore; // this gets the score from the previous scene ...
        ScoreText.text = "Score: " + score.ToString(); // ... and this updates the score text accordingly.
        SetScoreGoal();

        playerLifeCounter = 3;
    }

    /* This function will add `i` to the player's life. Ironically, it is only 
     * used by `Destroyable.cs` to remove life from the player.
     */
    public void EditLife(int i)
    {
        if (!ending) // if the game is ending, no more life should be removed.
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
        if (!ending) // if the game is ending, no more points should be awarded.
        {
            score += i;
            PlaySound(true);
            if (score >= scoreGoal && !endless) // the player should not advance to the next scene by earning points if the game is in endless mode.
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

    /* This function ends the game, sending the player to the End card and giving
     * them their score. It keeps track of the player's score and what level they
     * were on, in the case that they would like to restart that level.
     */
    private void EndGame()
    {
        ScorePackage.main.currentScore = score;
        ScorePackage.main.lastLevel = buildIndex;
        fader.FadeToEnd();
    }

    /* This function handles sending the player to the next level. It keeps track
     * of their score, and also sets their score as the starting score for the
     * next level.
     */
    private void NextScene()
    {
        ScorePackage.main.currentScore = score;
        ScorePackage.main.startingScore = score;
        fader.FadeSceneChange();
        DontDestroyOnLoad(ScorePackage.main);
    }

    /* This function handles the beginning of a playable scene and is called
     * by `Fader.cs`. The function being passed into `Ionspawner.main.Setup` is
     * the speed at which ions will be spawned.
     */
    public void BeginGame()
    {
        IonSpawner.main.Setup(70 - 3 * (buildIndex - 1)); 
        IonSpawner.main.Activate();
    }

    /* This function plays a sound depending on whether the player got their
     * answer correct (c = true) or incorrect (c = false)
     */
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

    /* This function sets the score goal for the player depending on the scene
     * that they are in. (This operation could be simplified by simply adding 5
     * to ScorePackage.main.startingScore, but I have a feeling that method could
     * be exploited somehow).
     */
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
