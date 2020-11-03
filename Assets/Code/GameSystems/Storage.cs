using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Storage
{
    [SerializeField] private List<string> resources = new List<string>();
    [SerializeField] private int maxStorage;

    public Storage(int maxStorage)
    {
        this.maxStorage = maxStorage;
    }

    public bool StoreItems (string resource)
    {
        if (resources.Count < maxStorage)
        {
            resources.Add(resource);
            return true;
        } else
        {
            return false;
        }
    }

    public bool StoreItems (List<string> toStore)
    {
        if(resources.Count - maxStorage <= toStore.Count)
        {
            resources.AddRange(toStore);
            return true;
        } else
        {
            return false;
        }
    }

    public List<string> GetItems ()
    {
        List<string> ret = new List<string>();
        ret.AddRange(resources);
        return ret;
    }

    public bool RemoveItems (string item)
    {
        bool tst = HasItems(item);
        if (tst)
        {
            resources.Remove(item);
        }
        return tst;
    }

    public bool RemoveItems (List<string> items)
    {
        if (!HasItems(items)) return false;

        foreach (string item in items)
        {
            resources.Remove(item);
        }

        return true;
    }

    public bool HasItems(string item)
    {
        return resources.Contains(item);
    }

    public bool HasItems(List<string> items)
    {
        foreach (string item in items)
        {
            if (!resources.Contains(item))
            {
                return false;
            }
        }
        return true;
    }

    public bool SetMaxStorage (int newMax)
    {
        if (newMax < resources.Count) return false;

        maxStorage = newMax;
        return true;
    }

    public int GetMaxStorageCapacity()
    {
        return maxStorage;
    }

    public bool HasSpace (int space)
    {
        return ((maxStorage - resources.Count) > space);
    }
}
