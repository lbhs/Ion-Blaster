using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score main;
    int CurrentScore;
    int pointsToGive;
    public Text CurrentScoreText;

    // public AudioSource VictorySoundEffect_Short;
    private void Start()
    {
        main = this;
    }

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

        CurrentScore += pointsToGive;
        CurrentScoreText.text = "score: " + CurrentScore;
    }

    private void OnMouseDown()
    {
        ObjectClicked(gameObject.tag);
    }

    private void Update()
    {
        
    }

}
