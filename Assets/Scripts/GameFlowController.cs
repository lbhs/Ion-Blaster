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
    public GameObject scorePack;
    public Timer t;

    void Start()
    {
        main = this;
        playerLifeCounter = 3;
    }

    public void EditLife(int i)
    {
        playerLifeCounter += i;
        if (playerLifeCounter < 1)
        {
            EndGame();
        }
        else
        {
            LifeText.text = "Lives: " + playerLifeCounter.ToString();
        }
    }

    private void EndGame()
    {
        scorePack.transform.position = Vector3.right * t.GetScore();
        Destroy(GameObject.Find("SpawnerHelper"));
        DontDestroyOnLoad(scorePack);
        SceneManager.LoadScene(3);
    }
}
