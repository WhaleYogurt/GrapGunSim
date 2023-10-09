using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int Score;
    public CircularSpeedometer speedometer;
    private float multiplier = 1.0f;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (speedometer != null)
        {
            multiplier = speedometer.scoreMultiplier;
        }
        scoreText.text = Score.ToString();
    }

    public void EarnPoint(int points)
    {
        Score += (int)(points * multiplier);
    }
}
