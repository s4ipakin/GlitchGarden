using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    #region Variables
    [SerializeField] int damage = 2;
    [SerializeField]
   
    protected float speed = 0.4f;
    
    public int hits;

    protected Defender defender;
    protected int GetDifender;
    protected Coroutine attack;
    [SerializeField]
    protected float AttackPeriodTime = 1f;
    protected bool ActiveOld;
    protected Health health;
    Animator animator = null;
    int eatHush = 0;
    int jumpHush = 0;
    public static event Action<Enemy> SayImDead;
    #endregion


    #region MonoBehaviour Methods
    protected void Awake()
    {
        health = GetComponent<Health>();
        health.OnSwitchedOff += OnInactivated;
        animator = GetComponent<Animator>();
        eatHush = Animator.StringToHash("Eat");
        jumpHush = Animator.StringToHash("Jump");
    }

    protected void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        var ifDefender = collision.GetComponent<Defender>();
        if (ifDefender)
        {
            SetBehevior(ifDefender);           
        }
    }


    public void OnInactivated(Transform transform)
    {
        if (SayImDead != null)
        {
            SayImDead(this);
        }
        gameObject.SetActive(false);
    }
    #endregion



    #region Costom Methods

    protected virtual void SetBehevior(Defender defender)
    {
        

    }

    protected void Jump()
    {
        animator.SetTrigger(jumpHush);
    }

    protected void Attack(Defender defender)
    {
        defender.OnFinished += StopAttacking;
        animator.SetBool(eatHush, true);
        StartCoroutine(Atacking(defender));
    }

    protected void StopAttacking()
    {
        StopCoroutine(Atacking(defender));
        if (!this) { return; }
        animator.SetBool(eatHush, false);
    }

    protected IEnumerator Atacking(Defender defender)
    {
        
        while (defender)
        {
            defender.GetComponent<Health>().TakeHealth(damage);
            yield return new WaitForSeconds(AttackPeriodTime);
        }
        
    }
 

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    #endregion
}
