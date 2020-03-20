using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    

    protected virtual void Update()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }
    protected void SetDamage(GameObject target, float damage)
    {
        Health targetHealth = target.GetComponent<Health>();
        targetHealth.TakeHealth(damage);
    }
}
