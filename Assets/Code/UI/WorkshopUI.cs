using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopUI : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text timeText;
    [SerializeField] Text costText;
    [SerializeField] Text neededText;
    [SerializeField] Text producedText;

    [SerializeField] int contentDisplace = 10;

    private ITrader trader;
    private ISettlement settlement;
    private IWorkshop workshop;

    public void Display(IWorkshop workshop, ITrader trader, ISettlement settlement)
    {
        this.trader = trader;
        this.settlement = settlement;
        this.workshop = workshop;

        nameText.text = workshop.GetName();
        timeText.text = "Time: " + DisplayTime(workshop.GetTime());
        costText.text = "Cost: " + workshop.GetCost() + "c";
        UiMethods.ListResources(UiMethods.ColapseResources(workshop.GetNeeded()), neededText, contentDisplace, this.gameObject);
        UiMethods.ListResources(UiMethods.ColapseResources(workshop.GetProduced()), producedText, contentDisplace, this.gameObject);
    }

    private string DisplayTime(int time)
    {
        string prefix = "h";
        float newTime = time;
        if (time >= 48)
        {
            newTime = time / 24;
            prefix = "d";
        } else if (newTime > 30)
        {
            newTime = newTime / 30;
            prefix = "m";
        }
        newTime = Mathf.Round(newTime);

        return newTime + prefix;
    }

    public void AddContract()
    {
        if (trader == null) return;
        if (settlement == null) return;
        if (workshop == null) return;
        List<Contract> contracts = trader.GetContracts();
        contracts.Add(new Production(settlement.GetPosition(), settlement as Settlement, trader as Trader, settlement.GetWallet(), workshop as Workshop));
        trader.SetContracts(contracts);
    }
}