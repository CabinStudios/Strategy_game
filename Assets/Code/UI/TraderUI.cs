using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderUI : MonoBehaviour, Observer
{
    private ITrader trader;
    [SerializeField] Text nameUI;
    [SerializeField] Text moneyUI;
    [SerializeField] Text maxStorageUI;
    [SerializeField] SimpleMenu simpleMenu;
    [SerializeField] GameObject resourceTemp;
    [SerializeField] GameObject contractTemp;

    public ITrader GetTrader()
    {
        return trader;
    }

    public void SetUI()
    {
        nameUI.text = trader.GetName();
        moneyUI.text = "Money: " + trader.GetMoney();
        maxStorageUI.text = "Max storage: " + trader.GetMaxStorage();
    }

    public void SetUI(ITrader trader)
    {
        this.trader = trader;
        SetUI();
    }

    public void DisplayResorces()
    {
        simpleMenu.CloseWindow();
        if (trader == null) return;
        List<string> resources = trader.GetResources();
        List<ColapsedObject<string>> colapsedResources = UiMethods.ColapseResources(resources);
        List<GameObject> objectList = new List<GameObject>();

        for (int i = 0; i < colapsedResources.Count; i++)
        {
            GameObject obj = Instantiate(resourceTemp, simpleMenu.GetContent());
            obj.GetComponent<ResourceUI>().Display(colapsedResources[i].ammount + "x " + colapsedResources[i].obj);
            objectList.Add(obj);
        }
        simpleMenu.Display(objectList, "Storage");
    }

    public void DisplayContract()
    {
        simpleMenu.CloseWindow();
        if (trader == null) return;
        List<Contract> contracts = trader.GetContracts();
        List<GameObject> objectList = new List<GameObject>();

        for (int i = 0; i < contracts.Count; i++)
        {
            GameObject obj = Instantiate(contractTemp, simpleMenu.GetContent());
            obj.GetComponent<ContractUI>().SetContract(trader, contracts[i], i);
            objectList.Add(obj);
        }
        simpleMenu.Display(objectList, "Contracts");
    }

    public void UpdateObserver()
    {
        if (trader != null) SetUI();
    }
}