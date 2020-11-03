using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Desire : IDesire
{
    [SerializeField] string name;
    [SerializeField] int positiveImpact = 1;
    [SerializeField] int negativeImpact = 1;
    [SerializeField] int price = 1;
    [SerializeField] List<string> resources = new List<string>();
    private bool satisfied = false;


    public bool Satisfy(Storage storage)
    {
        satisfied = storage.HasItems(resources);
        return (storage.RemoveItems(resources));
    }

    public List<string> GetResources ()
    {
        return resources;
    }

    public int GetPositiveImpact()
    {
        return positiveImpact;
    }

    public int GetNegativeImpact()
    {
        return negativeImpact;
    }

    public int GetPrice()
    {
        return price;
    }

    public bool IsSatisfied()
    {
        return satisfied;
    }

    public void ResetSatisfaction ()
    {
        satisfied = false;
    }

    public string GetName()
    {
        return name;
    }
}
