using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LoadStart : MonoBehaviour {

    #region Variables
    [SerializeField] float timeToWait = 6;
    Coroutine Loading;
    int sceneIndex;
    public static event Action<LoadStart> isLoading;
    public static event Action<LoadStart> isSaving;
    public static event Action<LoadStart> isLoadingFromStart;
    #endregion


    #region MonoBehaviour Methods

    void Start () 
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        Loading = StartCoroutine(WaitTillLoad());
	}
    #endregion



    #region Costom Methods

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
        if (isLoading != null)
        {
            isLoading(this);
        }
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
        if (isSaving != null)
        {
            isSaving(this);
        }
    }
    public void LoadFromBegining()
    {      
        if (isLoadingFromStart != null)
        {
            isLoadingFromStart(this);
        }
        SceneManager.LoadScene(2);
    }
    #endregion
}
