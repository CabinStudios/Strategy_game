using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityClickable : MonoBehaviour
{
    ISettlement settlement;
    MainCityPanel mainCityPanel;

    private void Start()
    {
        settlement = gameObject.GetComponent<Settlement>();
        mainCityPanel = GameObject.Find("CityInfo").GetComponent<MainCityPanel>();
    }

    private void OnMouseDown()
    {
        mainCityPanel.SetUI(settlement);
    }
}
