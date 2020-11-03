using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCityPanel : MonoBehaviour, Observer
{
    [SerializeField] private Player player;
    [SerializeField] private ISettlement settlement;
    [SerializeField] private Text nameUI;
    [SerializeField] private Text populationUI;
    [SerializeField] private Text moneyUI;
    [SerializeField] private Text satisfactionUI;
    //[SerializeField] private Text maxStorageUI;
    [SerializeField] private SimpleMenu simpleMenu;
    [SerializeField] private GameObject desireDisplay;
    [SerializeField] private GameObject workshopDisplay;
    [SerializeField] private GameObject resourceDisplay;
    [SerializeField] private TraderUI traderUI;
    private Timer uiUpdateTimer = null;

    private void Start()
    {
        Timer uiUpdateTimer = new Timer(24, false, new List<Observer>() { this, traderUI});
    }
    public void UpdateObserver ()
    {
        if (settlement != null) SetUI();
    }

    public void SetUI()
    {
        nameUI.text = settlement.GetName();
        populationUI.text = "Population: " + settlement.GetPopulation();
        satisfactionUI.text = "Satisfaction: " + settlement.GetSatisfaction();
        //maxStorageUI.text = "Max storage: " + settlement.GetMaxStorage();
        moneyUI.text = "Money: " + settlement.GetMoney();
    }
    public void SetUI(ISettlement settlement)
    {
        this.settlement = settlement;
        SetUI();
    }

    public void DisplayWorkshops()
    {
        simpleMenu.CloseWindow();
        if (settlement == null) return;
        List<IWorkshop> workshops = settlement.GetWorkshops();
        List<GameObject> objectList = WorkshopToGameObject(workshops);
        simpleMenu.Display(objectList, "Workshops");
    }

    private List<GameObject> WorkshopToGameObject (List<IWorkshop> workshops)
    {
        List<GameObject> objectList = new List<GameObject>();

        for (int i = 0; i < workshops.Count; i++)
        {
            GameObject obj = Instantiate(workshopDisplay, simpleMenu.GetContent());
            IWorkshop workshop = workshops[i];
            obj.GetComponent<WorkshopUI>().Display(workshop, traderUI.GetTrader(), settlement);
            objectList.Add(obj);
        }
        return objectList;
    }

    /*
    public void DisplayResources()
    {
        simpleMenu.CloseWindow();
        if (settlement == null) return;
        List<string> resources = settlement.GetStorage();
        List<ColapsedObject<string>> colapsedResources = UiMethods.ColapseResources(resources);
        List<GameObject> objectList = new List<GameObject>();

        for (int i = 0; i < colapsedResources.Count; i++)
        {
            GameObject obj = Instantiate(resourceDisplay, simpleMenu.GetContent());
            obj.GetComponent<ResourceUI>().Display(colapsedResources[i].ammount + "x " + colapsedResources[i].obj);
            objectList.Add(obj);
        }
        simpleMenu.Display(objectList, "Storage");
    }*/

    public void DisplayDesire()
    {
        simpleMenu.CloseWindow();
        if (settlement == null) return;
        List<IDesire> desires = settlement.GetDesires();
        List<IDesire> colapsedDesires = new List<IDesire>();
        List<int> ammount = new List<int>();
        List<GameObject> objectList = new List<GameObject>();

        for (int i = 0; i < desires.Count; i++)
        {
            if(colapsedDesires.Contains(desires[i]))
            {
                int j = 0;
                while(true)
                {
                    if (colapsedDesires[j].Equals(desires[i]))
                        break;
                    j++;
                }

                ammount[j]++;
            } else
            {
                colapsedDesires.Add(desires[i]);
                ammount.Add(1);
            }
        }

        for (int i = 0; i < colapsedDesires.Count; i++)
        {
            GameObject obj = Instantiate(desireDisplay, simpleMenu.GetContent());
            IDesire desire = colapsedDesires[i];
            obj.GetComponent<DesireUIObject>().Display(desire,traderUI.GetTrader(),settlement);
            objectList.Add(obj);
        }

        simpleMenu.Display(objectList, "Desires");
    }
}
