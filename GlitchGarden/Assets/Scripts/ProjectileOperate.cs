﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOperate : ProjectileBase, IProjectile
{
    [SerializeField] float speed = 20f;
    [SerializeField] float damage = 10;
    // Start is called before the first frame update
    Vector2 curremtPosition;
    public float Damage { get { return damage; } }
    void Start()
    {
        
    }

    protected override void Update()
    {
        //transform.Translate(transform.position.x * speed * Time.deltaTime);
        curremtPosition = transform.position;
        curremtPosition.x = curremtPosition.x + (5 * Time.deltaTime);
        transform.position = curremtPosition;
        //transform.RotateAround(transform.position, Vector3.back, 500 * Time.deltaTime);
        transform.rotation *= Quaternion.AngleAxis(500 * Time.deltaTime, Vector3.back);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {        
        var enemy = collision.gameObject.GetComponent(typeof(Enemy));
        if (enemy)
        {
            SetDamage(enemy.gameObject, damage);
        }
        gameObject.SetActive(false);
    }

}
