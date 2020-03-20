using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelTimer : MonoBehaviour
{
    float levelTime;
    public float LevelTime { get { return levelTime; } set { levelTime = value; } }
    public delegate void FinishLevel();
    public event FinishLevel LevelFinished;
    private float timeLeft;
    void Update()
    {
        timeLeft = Time.timeSinceLevelLoad / levelTime;
        GetComponent<Slider>().value = timeLeft;
        if (timeLeft >= 1 )
        {
            if (LevelFinished != null)
            {
                LevelFinished();
            }
        }
    }
}
