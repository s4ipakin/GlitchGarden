using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Eliminater : MonoBehaviour
{
    public int Collisions;
    float health = 5f;
    SliderTowerHealth healthBar;
    public static event Action<Eliminater> gameOver;

    private void Start()
    {
        healthBar = GetComponentInChildren<SliderTowerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        Collisions = Collisions + 1;
        Health enemy = collision.GetComponent<Health>();
        if (enemy)
        {
            enemy.TakeHealth(enemy.StartHealth);
        }
        float normalizedDamage = (health - Collisions) / health;

        
        healthBar.SetHealthBar(normalizedDamage);
        if (Collisions >= 5)
        {
            if (gameOver != null)
            {
                gameOver(this);
            }
            SceneManager.LoadScene(4);
        }
    }
}
