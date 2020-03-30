using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    #region Variables
    [SerializeField] GameObject Projectile;
    [SerializeField] GameObject Gun;
    SpawnPool ProjectileSpawner;
    
    CountEnemy countEnemy;
    SpawnAttackers SpawnerOnLine;
    Animator animator;
    public float yOfEnemy;
    public bool flgEnemyOnTheWay;
    #endregion


    #region MonoBehaviour Methods

    private void Start()
    {
        GetSpawnerOnLine();
        animator = GetComponent<Animator>();
        ProjectileSpawner = GetComponent<SpawnPool>();
        SpawnerOnLine.AttackerSpawned += StartShooting;
        SpawnerOnLine.AttackersRanOut += StopShooting;
        GetComponent<Defender>().SetType(0);
        FirstCheckEnemyOnLine();
    }

    public void OnDestroy()
    {
        SpawnerOnLine.AttackerSpawned -= StartShooting;
        SpawnerOnLine.AttackersRanOut -= StopShooting;
    }
    #endregion


    #region Costom Methods

    private void FirstCheckEnemyOnLine()
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
        var attackerSpawners = FindObjectsOfType<SpawnAttackers>();
        foreach (SpawnAttackers spawnAttacer in attackerSpawners)
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
        return  Mathf.Abs(transform.position.y - yPosition) < 0.6f;
    }

    public void Shoot()
    {
        if (flgEnemyOnTheWay)
        {
            ProjectileSpawner.GenerateFromPool(Gun.transform.position, transform.rotation);
        }
    }
    #endregion
}
