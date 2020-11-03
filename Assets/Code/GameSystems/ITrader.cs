using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrader
{
    string GetName();
    int GetMoney();
    int GetMaxStorage();
    List<string> GetResources();
    List<Contract> GetContracts();
    void SetContracts(List<Contract> contracts);
}
