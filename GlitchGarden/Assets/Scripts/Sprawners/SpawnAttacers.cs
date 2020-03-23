using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnAttacers : MonoBehaviour
{
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
 
    public bool Sprawn
    {
        get { return spawn; }
        set { spawn = value; }
    }

    IEnumerator Start()
    {
        if (!foundCounter)
        {
            count = FindObjectOfType<CountEnemy>().GetComponent<CountEnemy>();           
            foundCounter = true;
        }
        
       while(spawn)
        {
            float timeToWait = UnityEngine.Random.Range(1f, 5f);
            yield return new WaitForSeconds(timeToWait);
            attackerType = (int) Mathf.Round(UnityEngine.Random.Range(0, (ThisSpawnPool.Length)));
            //GameObject lizard = Instantiate(gameAttaker[attackerType], transform.position, transform.rotation) as GameObject;

            Transform attacker = ThisSpawnPool[attackerType].GenerateFromPool(transform.position, transform.rotation);
            if (AttackerSpawned != null)
            {
                AttackerSpawned();
            }
            nomberOfMyAttackers++;
            //lizard.transform.parent = transform;
            count.IncreaseEnemy(transform.position);
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

    //public void DecreaseAttackerNumber()
    //{
    //    nomberOfMyAttackers--;
    //    if (nomberOfMyAttackers == 0)
    //    {
    //        if (AttackersRanOut != null)
    //        {
    //            AttackersRanOut();
    //        }
    //    }
    //}
}
