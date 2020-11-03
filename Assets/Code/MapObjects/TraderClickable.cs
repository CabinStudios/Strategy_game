using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderClickable : MonoBehaviour
{
    Trader trader;
    TraderUI ui;

    private void Start()
    {
        trader = gameObject.GetComponent<Trader>();
        ui = GameObject.Find("TraderInfo").GetComponent<TraderUI>();
    }

    private void OnMouseDown()
    {
        ui.SetUI(trader);
    }
}
