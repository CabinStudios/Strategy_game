using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour, Observer, ISettlement
{
    [SerializeField] private string name = "unnamed";
    [SerializeField] private int population = 4;
    [SerializeField] private int satisfaction = 0;
    [SerializeField] private int populationCap = 10;
    [SerializeField] private Wallet wallet = new Wallet(0);
    //[SerializeField] private Storage storage = new Storage(10);
    [SerializeField] private List<Workshop> workshops = new List<Workshop>();
    [SerializeField] private List<Desire> desires = new List<Desire>();

    private Timer weekTimer;

    private void Start()
    {
        weekTimer = new Timer(7*24, false, this);
        name = gameObject.name;
    }


    public void UpdateObserver()
    {
        foreach (Desire desire in desires)
        {
            if(!desire.IsSatisfied())
            {
                satisfaction += desire.GetNegativeImpact();
            } else
            {
                desire.ResetSatisfaction();
            }
        }
    }

    public void Satisfy(int ammount)
    {
        satisfaction += ammount;
    }

    #region ISettlement
    public string GetName()
    {
        return name;
    }

    public int GetPopulation()
    {
        return population;
    }

    public int GetSatisfaction()
    {
        return satisfaction;
    }

    /*public int GetMaxStorage()
    {
        return storage.GetMaxStorageCapacity();
    }*/

    public int GetMoney()
    {
        return wallet.GetMoney();
    }

    /*List<string> ISettlement.GetStorage()
    {
        return storage.GetItems();
    }*/

    public List<IDesire> GetDesires()
    {
        List<IDesire> ret = new List<IDesire>(desires);
        return ret;
    }

    public List<IWorkshop> GetWorkshops()
    {
        List<IWorkshop> ret = new List<IWorkshop>(workshops);
        return ret;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    #endregion

    #region TradePartner
    public Wallet GetWallet()
    {
        return wallet;
    }

    /*public Storage GetStorage()
    {
        return storage;
    }*/

    #endregion
}
