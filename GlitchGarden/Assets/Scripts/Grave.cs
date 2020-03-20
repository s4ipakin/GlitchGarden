using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    void Start()
    {
        GetComponent<Defender>().SetType(2);
    }
    public void BiteMe(float damage)
    {
        GetComponent<Health>().TakeHealth(damage);       
    }
}
