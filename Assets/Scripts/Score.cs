using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int TotalPoints;
    int pointsToGive;
    public Text CurrentScore;

    // public AudioSource VictorySoundEffect_Short;

    public void ObjectClicked(string objectTag)
    {
        switch (objectTag)
        {
            case "Magnesium Ion":
                pointsToGive = 1;
                break;  

            case "Calcium Ion":
                pointsToGive = 1;
                break;

            case "Strontium Ion":
                pointsToGive = 2;
                break;
        }

        TotalPoints += pointsToGive;
        CurrentScore.text = "score: " + TotalPoints;
    }

    private void OnMouseDown()
    {
        ObjectClicked(gameObject.tag);
    }

}
