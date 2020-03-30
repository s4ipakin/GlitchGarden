using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PouseOperate : MonoBehaviour
{

    #region Variables
    [SerializeField] GameObject pauseMenu;
    DataManager dataManager;
    bool _paused;
    AudioSource audioClip;
    public bool Paused { get { return _paused; } }
    #endregion


    #region MonoBehaviour Methods


    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        audioClip = GetComponent<AudioSource>();
    }
    #endregion


    #region Costom Methods

    public void SetOnPause()
    {
        audioClip.Play();
        pauseMenu.SetActive(true);
        _paused = true;
        Time.timeScale = 0f;
    }

    public void ResuneGame()
    {
        audioClip.Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _paused = false;
    }

    public void StartMenu()
    {
        audioClip.Play();        
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        _paused = false;
    }

    public void SaveGame()
    {
        audioClip.Play();
        dataManager.SaveData();
    }
    #endregion
}
