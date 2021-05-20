using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public void RemoveMe()
    {
        Ion i = gameObject.GetComponent<Ion>();
        if (i.CheckFormula()) // Correct Answer
        {
            GameFlowController.main.PlaySound(true);
            IonSpawner IS = IonSpawner.main;
            //IS.LevelUp();
            if (IS.freq > 20)
            {
                IS.freq -= 2;
                //Debug.Log(IS.freq);
            }
        }
        else
        {
            GameFlowController.main.EditLife(-1);
            GameFlowController.main.PlaySound(false);
        }
        Destroy(gameObject);
    }
}
