using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseScript : MonoBehaviour
{
    private int sceneindex;
    public Text t1;
    public Text t2;

    private void Start()
    {
        sceneindex = SceneManager.GetActiveScene().buildIndex;
        SpawnIons();
        //SetupText();
    }

    private void SpawnIons()
    {
        IonSpawner IS = GameObject.Find("Ion Spawner").GetComponent<IonSpawner>();
        IS.Setup();
        IS.SpawnIntroIons();
    }

    // add a third text for the levels
    private void SetupText()
    {
        if (sceneindex == 2)
        {
            t1.text = "You will be BLASTING the following ions:";
            t2.text = "Start!";
        }
        else
        {
            t1.text = "You will continue to BLAST the following ions:";
            t2.text = "Continue";
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneindex + 1);
    }
}
