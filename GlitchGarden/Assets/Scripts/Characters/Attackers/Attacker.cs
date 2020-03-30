using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attacker : MonoBehaviour
{
    #region Variables
    [SerializeField] float damage = 2;
    [Range(0f, 5f)]    
    float speed = 1;
    public int hits;
    SpawnAttackers mySpawner;
    Defender defender;
    int GetDifender;
    public static event Action<Attacker> SayImDead;
    #endregion

    #region MonoBehaviour Methods

    void Start()
    {
        mySpawner = GetComponentInParent<SpawnAttackers>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Colid = collision.GetComponent<ProjectileOperate>();
        var ifDefender = collision.GetComponent<Defender>();
        if (ifDefender)
        {
            defender = ifDefender;
            defender.OnFinished += StopEating;
            GetComponent<Animator>().SetBool("Eat", true);
        }
        if (Colid)
        {
            Destroy(collision.gameObject);
            GetComponent<Health>().TakeHealth(Colid.Damage);
        }
        
    }

    private void OnDestroy()
    {
        if (SayImDead != null)
        {
            SayImDead(this);
        }
    }
    #endregion


    #region Costom Methods
    private void StopEating()
    {
        defender.OnFinished -= StopEating;
        if (!this) { return; }
        GetComponent<Animator>().SetBool("Eat", false);
    }

    public void Attack()
    {
        if (defender == null) { return; }
        defender.GetComponent<Health>().TakeHealth(damage);
    }
    #endregion


}
