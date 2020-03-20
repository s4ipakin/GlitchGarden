using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eliminater : MonoBehaviour
{
    public int Collisions;
    float health = 5f;
    SliderTowerHealth healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<SliderTowerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        
        Collisions = Collisions + 1;
        Health enemy = collision.GetComponent<Health>();
        if (enemy)
        {
            enemy.TakeHealth(enemy.StartHealth);
        }
        //collision.gameObject.SetActive(false);
        float normalizedDamage = (health - Collisions) / health;

        
        healthBar.SetHealthBar(normalizedDamage);
        if (Collisions >= 5)
        {
            DataManager dataManager = FindObjectOfType<DataManager>().GetComponent<DataManager>();
            dataManager.IsLoading = false;
            SceneManager.LoadScene(4);
        }
    }
}
