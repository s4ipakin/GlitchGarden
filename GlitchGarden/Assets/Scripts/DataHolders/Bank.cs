using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Bank : MonoBehaviour
{
    #region Variables
    DataManager pointsHolder;
    int allPoints = 100;
    Text pointText;
    //public delegate void ChangeSumm(int points);
    //public event ChangeSumm SummChanged;
    public static event Action<int> SummChanged;
    AudioSource audioSource;
    #endregion

    public int _Account
    { get
        { return allPoints;
            
        }
    }
    

    private void Start()
    {
        GetSavedPoints();
        pointText = GetComponent<Text>();
        pointText.text = allPoints.ToString();
        audioSource = GetComponent<AudioSource>();
        TrofyProfit.PointsEarned += TrofyProfit_PointsEarned;
    }


    private void TrofyProfit_PointsEarned(int obj)
    {
        AddPoints(obj);
    }



    #region CostmMethods

    private void GetSavedPoints()
    {
        pointsHolder = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        allPoints = pointsHolder.GetPoints();
        if ((allPoints == 0) && (pointsHolder.GetLevel() == 1))
        {
            allPoints = 100;
            pointsHolder.SetPoints(allPoints);
        }      
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
    #endregion


    private void OnDestroy()
    {
        pointsHolder.SetPoints(allPoints);
        TrofyProfit.PointsEarned -= TrofyProfit_PointsEarned;
    }

}
