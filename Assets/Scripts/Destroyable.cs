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
