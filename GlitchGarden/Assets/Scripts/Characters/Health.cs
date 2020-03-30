using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    #region Variables
    [SerializeField] float startHealth = 50;
    public float StartHealth { get { return startHealth; } }
    public delegate void BeingSwitchOff(Transform transform);
    public event BeingSwitchOff OnSwitchedOff;
    float health;
    bool coroutineStarted;
    public static event Action<Health> SayImDead;
    SliderTowerHealth healthBar;
    #endregion


    #region MonoBehaviour Methods

    private void Awake()
    {
        healthBar = GetComponentInChildren<SliderTowerHealth>();
    }
    
    private void Start()
    {
        health = startHealth;
        healthBar.gameObject.SetActive(false);
    }
    #endregion


    #region Costom Methods

    public void TakeHealth(float damage)
    {
        health -= damage;
              
        if (health <= 0)
        {
            if (SayImDead != null)
            {
                SayImDead(this);
            }
            health = startHealth;
            if (OnSwitchedOff != null)
            {
                OnSwitchedOff(this.transform);
            }
        }
        else
        {
            healthBar.gameObject.SetActive(true);
            healthBar.SetHealthBar(health / startHealth);
            if (!coroutineStarted)
            {
                coroutineStarted = true;
                StartCoroutine(SwitchOffHealthBar());
            }
        }        
    }

    private IEnumerator SwitchOffHealthBar()
    {
        yield return new WaitForSeconds(4f);
        healthBar.gameObject.SetActive(false);
        coroutineStarted = false;
    }
    #endregion
}
