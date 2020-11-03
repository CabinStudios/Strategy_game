using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkshop
{
    List<string> GetProduced();
    List<string> GetNeeded();
    int GetTime();
    int GetCost();
    string GetName();
}
