using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int cost = 10;
    private int _type;
    public int Type { get { return _type; } }
    public int _Price { get { return cost; } }
    public delegate void StopAttacking();
    public event StopAttacking OnFinished;
    Health health;
    //SliderTowerHealth healthBar;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnSwitchedOff += LeavePlace;
        //healthBar = GetComponentInChildren<SliderTowerHealth>();        
        //healthBar.gameObject.SetActive(false);

    }

    public void SetType(int type)
    {
        _type = type;
    }

    private void LeavePlace(Transform transform)
    {
        var spawn = FindObjectOfType<SpawnDefender>().GetComponent<SpawnDefender>(); //.FreePos(transform.position)
        if (spawn)
        {
            spawn.FreePos(transform.position);
        }
        if (OnFinished != null)
        {
            OnFinished();
        }
        Destroy(gameObject);
    }

    //protected void OnTriggerEnter2D(Collider2D collision)
    //{
    //    healthBar.gameObject.SetActive(true);
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    healthBar.gameObject.SetActive(false);
    //}

    //private void OnDestroy()
    //{
    //    if (OnFinished != null)
    //    {
    //        OnFinished();
    //    }
    //}
}
