using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public void RemoveMe()
    {
        if (gameObject.GetComponent<Ion>().CheckFormula())
        {
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
        }
        Destroy(gameObject);
    }
}
