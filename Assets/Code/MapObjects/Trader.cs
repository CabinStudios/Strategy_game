using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trader : MonoBehaviour, Observer, ITrader
{
    [SerializeField] private string name = "unnamed";
    [SerializeField] private Storage storage = new Storage(10);
    [SerializeField] private Wallet wallet = new Wallet(0);

    [SerializeField] private Move movement;

    private bool trading = false;
    private int objective = 0;
    private Timer timer = null;


    private List<Contract> contracts = new List<Contract>();

    private void Update()
    {
        if (contracts.Count > 0)
        {
            if(movement.AtTarget() && !trading)
            {
                Debug.Log("Trading");
                trading = true;
                if (contracts.Count > objective)
                {
                    contracts[objective].DoAgreement();
                    timer = new Timer(contracts[objective].ActionTime(), true, this);
                } else
                {
                    objective = 0;
                }
            } 
        }
    }

    public void UpdateObserver()
    {
        NextObjective();
    }

    public Storage GetStorage()
    {
        return storage;
    }

    public Wallet GetWallet()
    {
        return wallet;
    }

    private void NextObjective ()
    {
        Debug.Log("Stoped Trading");
        if (contracts.Count == 0) return;
        objective++;
        if (objective > contracts.Count-1)
            objective = 0;

        Debug.Log("Next Objective " + objective);

        movement.Walk(contracts[objective].GetPosition());
        trading = false;
    }

    #region ITrader
    public string GetName()
    {
        return name;
    }

    public int GetMoney ()
    {
        return wallet.GetMoney();
    }

    public int GetMaxStorage()
    {
        return storage.GetMaxStorageCapacity();
    }

    public List<string> GetResources()
    {
        return storage.GetItems();
    }

    public List<Contract> GetContracts()
    {
        List<Contract> ret = new List<Contract>(contracts);
        return ret;
    }

    public void SetContracts(List<Contract> contracts)
    {
        this.contracts = contracts;

        if (contracts.Count == 1)
            movement.Walk(contracts[0].GetPosition());
    }
    #endregion
}
