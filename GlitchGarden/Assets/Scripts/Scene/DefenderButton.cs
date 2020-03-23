using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defender;
    [SerializeField] GameObject hightLighter;
    Bank bank;
    int _accaunt;
    //public int Accaunt
    //{ 
    //    set
    //    {
    //        _accaunt = value;
    //        if (_accaunt < defender._Price)
    //        {
    //            this.GetComponent<SpriteRenderer>().color = new Color32(84, 91, 85, 255);
    //        }
    //    }
    //}
    
    private void Start()
    {
        hightLighter.SetActive(false);
        bank = FindObjectOfType<Bank>();
        _accaunt = bank._Account;
        SetEnabled();
        bank.SummChanged += CheckAccount;
        Text priceTag = GetComponentInChildren<Text>();
        priceTag.text = defender._Price.ToString();
    }

    private void CheckAccount(int points)
    {
        _accaunt = points;
        SetEnabled();
    }

    private void SetEnabled()
    {
        if (_accaunt >= defender._Price)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(84, 91, 85, 255);
            //hightLighter.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        var allButtons = FindObjectsOfType<DefenderButton>();
        foreach(DefenderButton button in allButtons)
        {
            //button.GetComponent<SpriteRenderer>().color = new Color32(84, 91, 85, 255); 
            button.Unhightlight();
        }
        if(_accaunt >= defender._Price)
        {
            //this.GetComponent<SpriteRenderer>().color = Color.white;
            hightLighter.SetActive(true);
            FindObjectOfType<SpawnDefender>().SetDefender(defender);
        }       
    }

    public void Unhightlight()
    {
        hightLighter.SetActive(false);
    }



}
