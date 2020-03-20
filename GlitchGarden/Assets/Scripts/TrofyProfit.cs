using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrofyProfit : MonoBehaviour
{

    [SerializeField] int pointsForBonce = 3;
    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>().GetComponent<Bank>();
        GetComponent<Defender>().SetType(1);
    }

    public void GetPtofit()
    {
        bank.AddPoints(pointsForBonce);
    }
   
}
