using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePackage : MonoBehaviour
{
    public int startingScore;
    public int currentScore = 0;
    public static ScorePackage main;
    public int lastLevel;
    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }
}
