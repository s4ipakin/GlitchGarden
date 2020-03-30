using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEnemy : MonoBehaviour
{
    SpawnAttackers EnemySpawner;
    public int enemyNumber;
    public delegate void ChangeEnemyNomber(int enemyNumber);
    public delegate void EnemySprawn();
    public delegate void EnemyCleaned();
    public event ChangeEnemyNomber EnemyNumberChanged;
    public event EnemySprawn EnemyHere;
    public event EnemyCleaned NoEnemy;

    private void Start()
    {
        Enemy.SayImDead += Enemy_SayImDead;
        SpawnAttackers.AttackerSwawned += SpawnAttackers_AttackerSwawned;
    }

    private void OnDestroy()
    {
        Enemy.SayImDead -= Enemy_SayImDead;
        SpawnAttackers.AttackerSwawned -= SpawnAttackers_AttackerSwawned;
    }

    #region EventsHandlers
    private void SpawnAttackers_AttackerSwawned(SpawnAttackers obj)
    {
        IncreaseEnemy();
    }

    private void Enemy_SayImDead(Enemy obj)
    {
        DecreaseEnemy();
    }
    #endregion


    #region CostomMethods
    public void IncreaseEnemy()
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
    #endregion

}
