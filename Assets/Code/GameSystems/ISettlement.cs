using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettlement
{
    string GetName();
    int GetPopulation();
    int GetSatisfaction();
    //int GetMaxStorage();
    int GetMoney();
    //List<string> GetStorage();
    List<IDesire> GetDesires();
    List<IWorkshop> GetWorkshops();
    Vector3 GetPosition();
    Wallet GetWallet();
}
