using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTowerHealth : MonoBehaviour
{
    //Health health;
    Transform bar;
    void Awake()
    {
        //health = GetComponentInChildren<Health>();
        bar = transform.Find("Bar");
    }

    public void SetHealthBar(float normalizedHealth)
    {
        bar.localScale = new Vector3(normalizedHealth, 1f);
    }
}
