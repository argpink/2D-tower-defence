using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    private void Awake()
    { main = this; }

    private void Start()
    { currency = 100; }

    public void IncreaseCurrency(int amountToPay)
    { currency += amountToPay; }

    public bool SpendCurrency(int amountToPay)
    {
        if (amountToPay <= currency)
        {
            currency -= amountToPay;
            return true;
            // BUY ITEM
        }
        else
        {
            Debug.Log("You do not have enough money to purchase this item.");
            return false;
        }

    }
}
