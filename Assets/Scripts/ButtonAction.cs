/*
 * ButtonAction.cs
 * Gavin Gee & Liela Andringa
 * 
 * This script handles the reset buttons in the playable scenes.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{ 
    public void LoadMain()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        if (GameObject.Find("SpawnerHelper"))
        {
            Destroy(GameObject.Find("SpawnerHelper"));
        }
        if (GameObject.Find("ScorePackage"))
        {
            Destroy(GameObject.Find("ScorePackage"));
        }

        SceneManager.LoadScene(2);

    }
}
