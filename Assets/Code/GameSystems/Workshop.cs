using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Workshop : Observer, IWorkshop
{
    [SerializeField] private string name = "unnamed";
    [SerializeField] private List<string> needed = new List<string>();
    [SerializeField] private List<string> created = new List<string>();
    [SerializeField] private int creationTime = 1;
    [SerializeField] int cost = 1;


    Timer timer;
    Storage storage = null;

    private bool creating = false;

    public bool GenerateResource(Wallet buyer, Wallet myWallet, Storage storage)
    {
        if (creating) return false;
        if (!buyer.CanPay(cost)) return false;
        if (!storage.HasSpace(created.Count - needed.Count)) return false;
        if (needed.Count > 0)
        { 
            if (!storage.RemoveItems(needed))
            {
                return false;
            }
        }
        if (!buyer.Pay(cost, myWallet)) return false;
        creating = true;
        timer = new Timer(creationTime, true, this);
        this.storage = storage;
        return true;
    }

    public bool Produce(Storage storage)
    {
        if (creating) return false;
        if (needed.Count <= 0 || !storage.RemoveItems(needed))
        {
            this.storage = storage;
            creating = true;
            timer = new Timer(creationTime, true, this);
            return true;
        }
        return false;
    }

    public void UpdateObserver()
    {
        if (creating && storage != null)
        {
            if (storage.StoreItems(created))
            {
                creating = false;
                timer = null;
            } else
            {
                timer = new Timer(1, true, this);
            }
        }
    }

    public void ChangeCost(int newCost)
    {
        cost = newCost;
    }

    #region Getters
    public List<string> GetProduced ()
    {
        List<string> ret = new List<string>();
        ret.AddRange(created);
        return ret; ;
    }
    
    public List<string> GetNeeded()
    {
        List<string> ret = new List<string>();
        ret.AddRange(needed);
        return ret; ;
    }

    public bool IsCrafting()
    {
        return creating;
    }

    public int GetTime()
    {
        return creationTime;
    }

    public int GetCost()
    {
        return cost;
    }

    public string GetName()
    {
        return name;
    }
    #endregion
}
