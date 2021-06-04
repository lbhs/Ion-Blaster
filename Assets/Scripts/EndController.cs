/*
 * EndController.cs
 * Gavin Gee
 * 
 * This script handles the buttons and displays the score in the final End Card
 * scene.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    string score;
    public Text t;

    // Start is called before the first frame update
    void Start()
    {
        score = ScorePackage.main.currentScore.ToString();
        t.text = score;
    }

    // This function restarts the player at the level that they lost at
    public void RestartLevel()
    {
        Destroy(ScorePackage.main);
        SceneManager.LoadScene(ScorePackage.main.lastLevel - 1);
    }

    // This function returns the player to the ion selection screen.
    public void ChooseDifferentIons()
    {
        Destroy(ScorePackage.main);
        Destroy(GameObject.Find("SpawnerHelper"));
        SceneManager.LoadScene(1);
    }
}
