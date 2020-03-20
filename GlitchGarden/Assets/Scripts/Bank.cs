using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    DataManager pointsHolder;
    int allPoints = 100;
    Text pointText;
    public delegate void ChangeSumm(int points);
    public event ChangeSumm SummChanged;
    AudioSource audioSource;
    public int _Account
    { get
        { return allPoints;
            
        }
    }
    

    private void Start()
    {
        pointsHolder = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        allPoints = pointsHolder.GetPoints();
        if ((allPoints == 0) && (pointsHolder.GetLevel() == 1))
        {
            allPoints = 100;
            pointsHolder.SetPoints(allPoints);
        }
        pointText = GetComponent<Text>();
        pointText.text = allPoints.ToString();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void AddPoints(int points)
    {          
        allPoints += points;
        pointText.text = allPoints.ToString();
        if(SummChanged != null)
        {
            SummChanged(allPoints);
        }
        pointsHolder.SetPoints(allPoints);
    }
    public void TakePoints(int points)
    {
        if (allPoints >= points)
        {
            allPoints -= points;
        }
        else
        {
            allPoints = 0;
        }
        if (SummChanged != null)
        {
            SummChanged(allPoints);
        }
        audioSource.Play();
        pointText.text = allPoints.ToString();
        pointsHolder.SetPoints(allPoints);
    }

    private void OnDestroy()
    {
        pointsHolder.SetPoints(allPoints);
    }
}
