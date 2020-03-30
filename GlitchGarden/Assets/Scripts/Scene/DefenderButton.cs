using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DefenderButton : MonoBehaviour
{
    #region Variables
    [SerializeField] Defender defender;
    [SerializeField] GameObject hightLighter;
    //Bank bank;
    public static event Action<Defender> inquiryToBuy;
    int _accaunt;
    #endregion


    #region MonoBehaviour Methods

    private void Start()
    {
        hightLighter.SetActive(false);
        Bank bank = FindObjectOfType<Bank>();
        _accaunt = bank._Account;
        SetEnabled();
        Bank.SummChanged += CheckAccount;
        Text priceTag = GetComponentInChildren<Text>();
        priceTag.text = defender._Price.ToString();
    }

    private void OnMouseDown()
    {
        var allButtons = FindObjectsOfType<DefenderButton>();
        foreach (DefenderButton button in allButtons)
        {
            button.Unhightlight();
        }
        if (_accaunt >= defender._Price)
        {
            hightLighter.SetActive(true);
            if (inquiryToBuy != null)
            {
                inquiryToBuy(defender);
            }
        }
    }


    private void OnDestroy()
    {
        Bank.SummChanged -= CheckAccount;
    }
    #endregion


    #region Costom Methods
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
        }
    }

    

    public void Unhightlight()
    {
        hightLighter.SetActive(false);
    }
    #endregion
}
