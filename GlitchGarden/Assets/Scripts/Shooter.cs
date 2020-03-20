using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] GameObject Gun;
    SpawnPool ProjectileSpawner;
    
    CountEnemy countEnemy;
    SpawnAttacers SpawnerOnLine;
    Animator animator;
    public float yOfEnemy;
    public bool flgEnemyOnTheWay;
    private void Start()
    {
        //countEnemy = FindObjectOfType<CountEnemy>().GetComponent<CountEnemy>();
        //countEnemy.EnemyHere += StartShooting;
        //countEnemy.NoEnemy += StopShooting;
        GetSpawnerOnLine();
        animator = GetComponent<Animator>();
        ProjectileSpawner = GetComponent<SpawnPool>();
        SpawnerOnLine.AttackerSpawned += StartShooting;
        SpawnerOnLine.AttackersRanOut += StopShooting;
        GetComponent<Defender>().SetType(0);
        CheckEnemyOnLine();
    }

    

    private void CheckEnemyOnLine()
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,
            Mathf.Infinity, mask, -Mathf.Infinity, Mathf.Infinity);
        if (hit)
        {
            StartShooting();
        }
    }

    private void GetSpawnerOnLine()
    {
        var attackerSpawners = FindObjectsOfType<SpawnAttacers>();
        foreach (SpawnAttacers spawnAttacer in attackerSpawners)
        {
            float getY = spawnAttacer.transform.position.y;
            if (CheckEnemyPos(getY))
            {
                SpawnerOnLine = spawnAttacer;
            }
        }
    }


    private void StopShooting()
    {
        if (animator == null) { return; }
        animator.SetBool("Attack", false);
        flgEnemyOnTheWay = false;
    }

    private void StartShooting()
    {
        animator.SetBool("Attack", true);
        flgEnemyOnTheWay = true;
    }

    private bool CheckEnemyPos(float yPosition)
    {
        //return (((transform.position.y - 0.6f) < yPosition) && ((transform.position.y + 0.6f) > yPosition)); 
        return  Mathf.Abs(transform.position.y - yPosition) < 0.6f;
    }

    public void Shoot()
    {
        if (flgEnemyOnTheWay)
        {
            //Instantiate(Projectile, Gun.transform.position, transform.rotation);
            ProjectileSpawner.GenerateFromPool(Gun.transform.position, transform.rotation);
        }
        
    }
    public void OnDestroy()
    {
        SpawnerOnLine.AttackerSpawned -= StartShooting;
        SpawnerOnLine.AttackersRanOut -= StopShooting;
    }

    
}
