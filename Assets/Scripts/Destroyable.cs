/*
 * Destroyable.cs
 * Liela Andringa and Gavin Gee
 * 
 * This script handles the destruction of ions and also instructs `GameFlowController`
 * to award points or remove life from a playaer.
 * 
 * In endless mode, this script also increases the speed at which ions are spawned
 * at (lines 23-26).
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public void RemoveMe()
    {
        if (gameObject.GetComponent<Ion>().CheckFormula()) // Correct Answer
        {
            GameFlowController.main.EditScore(1);
            
            if (GameFlowController.main.endless && IonSpawner.main.freq > 20)
            {
                IonSpawner.main.freq -= 2;
            }
        }
        else // Incorrect Answer
        {
            GameFlowController.main.EditLife(-1);
            GameFlowController.main.PlaySound(false);
        }
        Destroy(gameObject);
    }
}
