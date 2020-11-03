using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDesire
{
    string GetName();
    List<string> GetResources();
    int GetPositiveImpact();
    int GetNegativeImpact();
    int GetPrice();
}
