using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade : Contract
{
    private Trader seller;
    private Settlement buyer;
    private Desire desire;
    private Vector3 position;
    int price;

    public Trade(Trader seller, Settlement buyer, Vector3 position, Desire desire, int price)
    {
        this.seller = seller;
        this.buyer = buyer;
        this.position = position;
        this.desire = desire;
        this.price = price;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public int ActionTime()
    {
        return 4;
    }

    public bool DoAgreement()
    {
        if (buyer.GetWallet().CanPay(price) && desire.Satisfy(seller.GetStorage()))
        {
            buyer.GetWallet().Pay(price, seller.GetWallet());
            buyer.Satisfy(desire.GetPositiveImpact());
            return true;
        }
        else
            return false;
    }

    public string GetBuyer()
    {
        return buyer.GetName();
    }

    public string GetSeller()
    {
        return seller.GetName();
    }

    public int GetPrice()
    {
        return price;
    }

    public string GetAgrement()
    {
        return desire.GetName();
    }
}
