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

    public void RestartLevel()
    {
        Destroy(ScorePackage.main);
        SceneManager.LoadScene(ScorePackage.main.lastLevel - 1);
    }

    public void ChooseDifferentIons()
    {
        Destroy(ScorePackage.main);
        Destroy(GameObject.Find("SpawnerHelper"));
        SceneManager.LoadScene(1);
    }
}
