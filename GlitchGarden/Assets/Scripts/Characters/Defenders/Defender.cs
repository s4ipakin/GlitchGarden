using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Defender : MonoBehaviour
{

    #region Variables
    [SerializeField] int cost = 10;
    private int _type;
    public int Type { get { return _type; } }
    public int _Price { get { return cost; } }
    public delegate void StopAttacking();
    public event StopAttacking OnFinished;
    Health health;
    public static event Action<Defender> SayImDead;
    #endregion

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnSwitchedOff += LeavePlace;
    }


    #region Costom Methods

    public void SetType(int type)
    {
        _type = type;
    }

    private void LeavePlace(Transform transform)
    {
        
        if (SayImDead != null)
        {
            SayImDead(this);
        }

        if (OnFinished != null)
        {
            OnFinished();
        }
        Destroy(gameObject);
    }
    #endregion
}
