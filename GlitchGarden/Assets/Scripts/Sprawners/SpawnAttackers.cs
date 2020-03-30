using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnAttackers : MonoBehaviour
{
    #region Variables
    [SerializeField] bool spawn;
    [SerializeField] GameObject[] gameAttaker;
    Coroutine SpawnLizard;
    public delegate void AttackerSpawne();
    public delegate void NoAttacker();
    public event AttackerSpawne AttackerSpawned;
    public event NoAttacker AttackersRanOut;
    bool foundCounter;
    int attackerType;
    CountEnemy count;
    int nomberOfMyAttackers;
    [SerializeField] SpawnPool[] ThisSpawnPool;
    public static event Action<SpawnAttackers> AttackerSwawned;

    public bool Sprawn
    {
        get { return spawn; }
        set { spawn = value; }
    }
    #endregion


    #region MonoBehaviour Methods

    IEnumerator Start()
    {
       

       while(spawn)
        {
            float timeToWait = UnityEngine.Random.Range(1f, 5f);
            yield return new WaitForSeconds(timeToWait);
            attackerType = (int) Mathf.Round(UnityEngine.Random.Range(0, (ThisSpawnPool.Length)));
            Transform attacker = ThisSpawnPool[attackerType].GenerateFromPool(transform.position, transform.rotation);
            if (AttackerSpawned != null)
            {
                AttackerSpawned();
            }
            nomberOfMyAttackers++;
            if (AttackerSwawned != null)
            {
                AttackerSwawned(this);
            }
        }
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ProjectileOperate>())
        {
            if (AttackersRanOut != null)
            {
                AttackersRanOut();
            }
        }
    }
    #endregion
}
