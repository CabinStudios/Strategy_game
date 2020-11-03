using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Wallet
{
    [SerializeField] private int money;

    public Wallet (int money)
    {
        this.money = money;
    }

    public int GetMoney ()
    {
        return money;
    }

    public bool Pay (int cost, Wallet to)
    {
        if (cost > money) return false;

        money -= cost;
        to.AddMoney(cost);
        return true;
    }

    public bool CanPay(int cost)
    {
        return !(money < cost);
    }

    public void AddMoney(int add)
    {
        money += add;
    }
}
