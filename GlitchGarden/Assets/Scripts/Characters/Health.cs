using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startHealth = 50;
    public float StartHealth { get { return startHealth; } }
    public delegate void BeingSwitchOff(Transform transform);
    public event BeingSwitchOff OnSwitchedOff;
    float health;
    bool coroutineStarted;
    
    SliderTowerHealth healthBar;
    private void Awake()
    {
        healthBar = GetComponentInChildren<SliderTowerHealth>();
    }
    
    private void Start()
    {
        health = startHealth;
        healthBar.gameObject.SetActive(false);
    }

    

    public void TakeHealth(float damage)
    {
        health -= damage;
              
        if (health <= 0)
        {
            ExplosionOperate explosion = FindObjectOfType<ExplosionOperate>().GetComponent<ExplosionOperate>();
            explosion.Explode(transform.position, Quaternion.identity);
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
    
}
