using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEnemy : MonoBehaviour
{
    SpawnAttacers EnemySpawner;
    int enemyNumber;
    public delegate void ChangeEnemyNomber(int enemyNumber);
    public delegate void EnemySprawn();
    public delegate void EnemyCleaned();
    public event ChangeEnemyNomber EnemyNumberChanged;
    public event EnemySprawn EnemyHere;
    public event EnemyCleaned NoEnemy;
    

    public void IncreaseEnemy(Vector2 position)
    {
        enemyNumber = enemyNumber + 1;
        if (EnemyNumberChanged != null)
        {
            EnemyNumberChanged(enemyNumber);
        }       
        if (enemyNumber > 0)
        {
            if (EnemyHere != null)
            EnemyHere();
        }        
    }

    public void DecreaseEnemy()
    {
        enemyNumber = enemyNumber - 1;
        if (EnemyNumberChanged != null)
        {
            EnemyNumberChanged(enemyNumber);
        }
        if (enemyNumber == 0)
        {   
            if (NoEnemy != null)
            NoEnemy();
        }
    }

}
