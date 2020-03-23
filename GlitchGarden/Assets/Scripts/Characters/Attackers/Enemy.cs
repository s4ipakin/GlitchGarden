using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

        //var projectile = collision.gameObject.GetComponent(typeof(IProjectile));
        var ifDefender = collision.GetComponent<Defender>();
        if (ifDefender)
        {
            SetBehevior(ifDefender);           
        }
        //if (projectile)
        //{
        //    //Destroy(collision.gameObject);           
        //    GetComponent<Health>().TakeHealth((projectile as IProjectile).Damage);
        //    //collision.gameObject.SetActive(false);
        //}
    }


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
        //defender.OnFinished -= StopAttacking;
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

    public void OnInactivated(Transform transform)
    {
        CountEnemy countEnemy = FindObjectOfType<CountEnemy>().GetComponent<CountEnemy>();
        countEnemy.DecreaseEnemy();
        gameObject.SetActive(false);
    }
    

    

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
