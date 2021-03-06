﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefender : MonoBehaviour
{

    #region Variables
    Defender defender1;
    Bank bank;
    List<Vector2> takenPositions = new List<Vector2>();
    [SerializeField] Defender cactusPrefub;
    [SerializeField] Defender trofyPrefub;
    [SerializeField] Defender gravePrefub;
    [SerializeField] Defender catapultPrefub;
    PouseOperate pouseOperate;
    #endregion



    #region MonoBehaviour Methods


    private void Start()
    {
        bank = FindObjectOfType<Bank>();
        DataManager dataManager = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        pouseOperate = FindObjectOfType<PouseOperate>().GetComponent<PouseOperate>();
        if (dataManager.IsLoading)
        {
            SetSavedPrefubs(dataManager);
        }
        Defender.SayImDead += Defender_SayImDead;
        DefenderButton.inquiryToBuy += DefenderButton_inquiryToBuy;
    }

    

    private void OnMouseDown()
    {
        Spawn(GetWorldPos());
    }

    private void OnDestroy()
    {
        Defender.SayImDead -= Defender_SayImDead;
        DefenderButton.inquiryToBuy -= DefenderButton_inquiryToBuy;
    }
    #endregion



    #region CostmMethods

    private void Defender_SayImDead(Defender obj)
    {
        FreePos(obj.transform.position);
    }

    private void DefenderButton_inquiryToBuy(Defender obj)
    {
        SetDefender(obj);
    }


    private void SetSavedPrefubs(DataManager dataManager)
    {
        Defender[] defenders = new Defender[4];
        defenders[0] = cactusPrefub;
        defenders[1] = trofyPrefub;
        defenders[2] = gravePrefub;
        defenders[3] = catapultPrefub;

        for (int i=0; i< dataManager.GetPrefubsCount(); i++)
        {
            Defender newDefender = Instantiate(defenders[dataManager.LoadPrefubType(i+1)],
                dataManager.LoadPrefubsPos(i+1), Quaternion.identity) as Defender;
        }
    }

    public void SetDefender(Defender defender)
    {
        defender1 = defender;
    }
 

    private Vector2 GetWorldPos()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        Vector2 newPos = new Vector2(newX, newY);
        return newPos;
    }

    private void Spawn(Vector2 worldPos)
    {
        if ((this.defender1 == null) || (bank._Account < defender1._Price) || 
            (takenPositions.Contains(worldPos)) || pouseOperate.Paused) { return; }
        bank.TakePoints(defender1._Price);
        Defender newDefender = Instantiate(defender1, worldPos, transform.rotation) as Defender;
        takenPositions.Add(worldPos);
    }

    public void FreePos(Vector2 pos)
    {
        takenPositions.Remove(pos);
    }
    #endregion

}
