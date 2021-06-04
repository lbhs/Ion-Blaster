/* 
 * PauseScript.cs
 * Gavin Gee
 * 
 * This script spawns the ions seen in the loading screen between playable scenes.
 * 
 * It also contains functionality to change the text objects in the scene. This
 * wasn't utilized in the final build of the game from the ACR 2021 team, but
 * the functionality is left here in case it will become useful later.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseScript : MonoBehaviour
{
    private int sceneindex;
    public Text t1; // The top text in the loading scene
    public Text t2; // the bottom text in the loading scene.

    private void Start()
    {
        sceneindex = SceneManager.GetActiveScene().buildIndex;
        SpawnIons();
        //SetupText();
    }

    // This function spawns the Ions in the loading scene with the help of `IonSpawner.cs`
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
