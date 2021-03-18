using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int scoreAmount;
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreAmount = 0;
    }

    // Update is called once per frame
    // void Update()
    // {
        // scoreText.text = "Score: " + ScoreAmount;

        // if (Input.GetMouseButtonDown(0));
        // {
            // "correct":
                // Score.scoreAmount += 1;
            // Destroy(Collision.gameObject);
            // break;
            // case "incorrect":
                // Score.scoreAmount += 0;
            // Destroy(Collision.gameObject);
            // break;
        // }


}