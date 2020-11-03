using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ConcreteObserver : Observable
{
    List<Observer> observers = new List<Observer>();

    public void RegisterObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(Observer observer)
    {
        if (observers.Contains(observer)) observers.Remove(observer);
    }

    public void UnregisterAllObservers()
    {
        observers = new List<Observer>();
    }

    public void NotifyObservers()
    {
        List<Observer> tmp = new List<Observer>();
        tmp.AddRange(observers);
        foreach (Observer obs in tmp)
        {
            obs.UpdateObserver();
        }
    }
}
