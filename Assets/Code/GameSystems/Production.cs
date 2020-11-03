using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : Contract
{
    private Vector3 position;
    Trader trader;
    Settlement producer;
    private Workshop workshop;
    private Wallet workshopWallet;

    public Production(Vector3 position,Settlement producer, Trader trader, Wallet workshopWallet, Workshop workshop)
    {
        this.position = position;
        this.trader = trader;
        this.producer = producer;
        this.workshopWallet = workshopWallet;
        this.workshop = workshop;
    }

    public int ActionTime()
    {
        return workshop.GetTime();
    }

    public bool DoAgreement()
    {
        return workshop.GenerateResource(trader.GetWallet(), workshopWallet, trader.GetStorage());
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public string GetSeller()
    {
        return producer.GetName();
    }

    public string GetBuyer()
    {
        return trader.GetName();
    }

    public int GetPrice()
    {
        return workshop.GetCost();
    }

    public string GetAgrement()
    {
        return workshop.GetName();
    }
}
