/* 
 * ScorePackage.cs
 * Gavin Gee
 * 
 * This class contains some useful information used by `EndController.cs` and
 * `GameFlowController.cs`, and the object that it is attached to is carried 
 * throughout all scenes until the end of the game.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePackage : MonoBehaviour
{
    public static ScorePackage main;

    public int startingScore; // the starting score of the current level.
    public int currentScore = 0; // the current score of the player.
    public int lastLevel; // the level that the player lost at. 

    void Start()
    {
        main = this;
    }
}
