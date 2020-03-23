using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LoadStart : MonoBehaviour {

    [SerializeField] float timeToWait = 6;
    Coroutine Loading;
    int sceneIndex;
	// Use this for initialization
	void Start () {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        Loading = StartCoroutine(WaitTillLoad());
	}

    IEnumerator WaitTillLoad()
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(1);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadWithSavedData()
    {
        DataManager dataManager = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        dataManager.IsLoading = true;
        dataManager.LoadSavedData();
        SceneManager.LoadScene(2);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Save()
    {
        DataManager dataManager = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        dataManager.SaveData();
    }
    public void LoadFromBegining()
    {
        DataManager dataManager = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        dataManager.SetPoints(0);
        dataManager.SetLevel(0);
        SceneManager.LoadScene(2);
    }
}
