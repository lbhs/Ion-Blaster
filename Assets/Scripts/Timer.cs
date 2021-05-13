using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int timerValue;
    private float initialTime;
    public Text timerText;
    private bool timing;

    // Start is called before the first frame update
    void Start()
    {
        initialTime = Time.time;
        timing = true;
    }

    private void Update()
    {
        if (!timing) return;
        timerValue = (int)(Time.time - initialTime);
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timerText.text = "Score: " + timerValue.ToString();
    }

    private void StopTimer()
    {
        timing = false;
    }

    public int GetScore()
    {
        return timerValue;
    }

}
