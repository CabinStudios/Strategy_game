using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractUI : MonoBehaviour
{
    [SerializeField] Text sellerUI;
    [SerializeField] Text buyerUI;
    [SerializeField] Text agrementUI;
    [SerializeField] Text timeUI;
    [SerializeField] Text priceUI;

    private Contract contract;
    private ITrader trader;
    private int place;
    
    private void SetContract()
    {

        sellerUI.text = "Seller: " + contract.GetSeller();
        buyerUI.text = "Buyer: " + contract.GetBuyer();
        agrementUI.text = contract.GetAgrement();
        timeUI.text = "Time: " + contract.ActionTime();
        priceUI.text = "Price: " + contract.GetPrice();
    }

    public void SetContract(ITrader trader, Contract contract, int place)
    {
        this.contract = contract;
        this.trader = trader;
        this.place = place;
        SetContract();
    }

    public void RemoveContract()
    {
        List<Contract> contracts = trader.GetContracts();
        contracts.Remove(contract);
        trader.SetContracts(contracts);

        GameObject.Find("TraderInfo").GetComponent<TraderUI>().DisplayContract();
    }

    public void MoveUp()
    {
        if (place == 0) return;
        List<Contract> contracts = trader.GetContracts();
        contracts[place] = contracts[place - 1];
        contracts[place - 1] = contract;
        trader.SetContracts(contracts);
        GameObject.Find("TraderInfo").GetComponent<TraderUI>().DisplayContract();
    }

    public void MoveDown()
    {
        List<Contract> contracts = trader.GetContracts();
        if (place == contracts.Count - 1) return;
        contracts[place] = contracts[place + 1];
        contracts[place + 1] = contract;
        trader.SetContracts(contracts);
        GameObject.Find("TraderInfo").GetComponent<TraderUI>().DisplayContract();
    }
}
