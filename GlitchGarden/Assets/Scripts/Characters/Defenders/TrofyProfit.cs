using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TrofyProfit : MonoBehaviour
{

    [SerializeField] int pointsForBonce = 3;   
    public static event Action<int> PointsEarned;


    void Start()
    {
        GetComponent<Defender>().SetType(1);
    }

    public void GetPtofit()
    {
        if (PointsEarned != null)
        {
            PointsEarned(pointsForBonce);
        }
    }
   
}
