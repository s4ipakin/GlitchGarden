using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelOperate : MonoBehaviour
{
    DataManager levelHolder;
    [SerializeField] SpawnAttackers[] spawnAttacers;
    [SerializeField] LevelTimer levelTimer;
    public int currentLevel;

    private void Awake()
    {
        levelHolder = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        currentLevel = levelHolder.GetLevel();
        if (currentLevel == 0)
        {
            currentLevel = 1;
            levelHolder.SetLevel(currentLevel);
        }
        if (currentLevel ==1)
        {
            levelTimer.LevelTime = 90;
        }
        else
        {
            levelTimer.LevelTime = 60 + currentLevel * 30;
        }
        levelTimer.LevelTime = currentLevel * 30;
        if ((levelHolder.IsLoading) && (levelHolder.GetRows() > 0))
        {
            SetSavedRaws();
        }
        else
        {
            SetRandomRows();
        }
        levelTimer.LevelFinished += LoadNext;
    }

    private void SetRandomRows()
    {
        List<SpawnAttackers> UnsetSpawners = new List<SpawnAttackers>();
        int rowsSetUp = 0;
        //foreach (SpawnAttacers spawn in spawnAttacers)
        //{
        //    UnsetSpawners.Add(spawn);
        //}
        for (int k = 0; k < spawnAttacers.Length; k++)
        {
            UnsetSpawners.Add(spawnAttacers[k]);
        }
        for (int i = 0; i < currentLevel; i++)
        {
            int index = (int)Mathf.Round(Random.Range(0, (UnsetSpawners.Count)));
            UnsetSpawners[index].Sprawn = true;
            UnsetSpawners.Remove(UnsetSpawners[index]);           
        }
        for (int j = 0; j < spawnAttacers.Length; j++)
        {
            if (spawnAttacers[j].Sprawn)
            {
                rowsSetUp = rowsSetUp | (1 << j);               
            }
        }
        levelHolder.SetRaws(rowsSetUp);
    }

    private void SetSavedRaws()
    {
        int rowsSetUp = levelHolder.GetRows();
        for (int i = 0; i < spawnAttacers.Length; i++)
        {
            if ((rowsSetUp & (1 << i)) != 0)
            {
                spawnAttacers[i].Sprawn = true;
            }
        }
    }

    private void LoadNext()
    {
        levelHolder.SetRaws(0);
        levelHolder.IsLoading = false;
        levelHolder.SetLevel(currentLevel + 1);
        SceneManager.LoadScene(3);
    }
}
