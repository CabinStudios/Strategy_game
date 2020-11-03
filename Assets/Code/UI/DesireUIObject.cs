using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesireUIObject : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text priceText;
    [SerializeField] Text positiveText;
    [SerializeField] Text negativeText;
    [SerializeField] Text resourceText;
    [SerializeField] int contentDisplace = 10;

    IDesire desire;
    ITrader trader;
    ISettlement settlement;


    public void Display(IDesire desire, ITrader trader, ISettlement settlement)
    {
        this.desire = desire;
        this.trader = trader;
        this.settlement = settlement;
        nameText.text = desire.GetName();
        priceText.text = "Cost: " + desire.GetPrice() + "c";
        positiveText.text = "Positive impact: "+desire.GetPositiveImpact();
        negativeText.text = "Negative impact: "+desire.GetNegativeImpact();

        UiMethods.ListResources(UiMethods.ColapseResources(desire.GetResources()), resourceText, contentDisplace, this.gameObject);
    }

    public void AddContract()
    {
        if (desire == null) return;
        if (trader == null) return;
        if (settlement == null) return;

        List<Contract> contracts = trader.GetContracts();
        contracts.Add(new Trade(trader as Trader, settlement as Settlement, settlement.GetPosition(), desire as Desire, desire.GetPrice()));
        trader.SetContracts(contracts);
    }
}
