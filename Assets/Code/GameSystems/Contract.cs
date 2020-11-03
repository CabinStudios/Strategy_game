using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Contract
{
    Vector3 GetPosition();
    bool DoAgreement();
    int ActionTime();
    string GetBuyer();
    string GetSeller();
    int GetPrice();
    string GetAgrement();
}
