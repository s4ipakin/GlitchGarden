﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    #region Variables
    public static DataManager instance;
    int level;
    int points;
    int prefubCount;
    int rowsSetUp;
    public bool IsLoading { get; set; }
    #endregion


    #region MonoBehaviour Methods

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        LoadStart.isLoading += LoadStart_isLoading;
        LoadStart.isSaving += LoadStart_isSaving;
        LoadStart.isLoadingFromStart += LoadStart_isLoadingFromStart;
        Eliminater.gameOver += Eliminater_gameOver;
    }

    

    #endregion


    #region EventsHandlers

    private void LoadStart_isLoading(LoadStart obj)
    {
        IsLoading = true;
        LoadSavedData();
    }

    private void LoadStart_isSaving(LoadStart obj)
    {
        SaveData();
    }

    private void LoadStart_isLoadingFromStart(LoadStart obj)
    {
        SetPoints(0);
        SetLevel(0);
        IsLoading = false;
    }

    private void Eliminater_gameOver(Eliminater obj)
    {
        IsLoading = false;
    }

    #endregion

    #region GetSet Data
    public void SetRaws(int rowsSetUp)
    {
        this.rowsSetUp = rowsSetUp;
    }

    public int GetRows()
    {
        return rowsSetUp;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetPoints(int points)
    {
        this.points = points;
    }

    public int GetPoints()
    {
        return points;
    }

    public int GetPrefubsCount()
    {
        return prefubCount;
    }
    #endregion


    #region SaveLoad data

    public void SaveData()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("rows", rowsSetUp);
        SavePrefubsPos();
        Debug.Log("saved");

    }

    public void LoadSavedData()
    {
        level = PlayerPrefs.GetInt("level");
        points = PlayerPrefs.GetInt("points");
        prefubCount = PlayerPrefs.GetInt("prefubCount");
        rowsSetUp = PlayerPrefs.GetInt("rows");
    }

    public Vector2 LoadPrefubsPos(int Id)
    {
        float x = PlayerPrefs.GetFloat(Id.ToString() + "x");
        float y = PlayerPrefs.GetFloat(Id.ToString() + "y");
        return new Vector2(x, y);
    }

    public int LoadPrefubType(int Id)
    {
        return PlayerPrefs.GetInt(Id.ToString() + "type");
    }

    public void SavePrefubsPos()
    {
        var allPrefubs = FindObjectsOfType<Defender>();
        int Id = 0;
        prefubCount = allPrefubs.Length;
        PlayerPrefs.SetInt("prefubCount", prefubCount);
        if (prefubCount > 0)
        {
            Id = 1;
            foreach (Defender defender in allPrefubs)
            {

                PlayerPrefs.SetFloat(Id.ToString() + "x", defender.transform.position.x);
                PlayerPrefs.SetFloat(Id.ToString() + "y", defender.transform.position.y);
                PlayerPrefs.SetInt(Id.ToString() + "type", defender.Type);
                Id++;
            }
        }
    }
    #endregion
}
