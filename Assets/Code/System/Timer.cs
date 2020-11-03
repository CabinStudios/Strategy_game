using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : Observer
{
    ConcreteObserver observable = new ConcreteObserver();
    int startTime;
    int time;
    bool singleUse;

    public Timer(int time, bool singleUse, Observer observer)
    {
        GlobalTime.RegisterTimeObserver(this);
        observable.RegisterObserver(observer);
        this.time = time;
        this.startTime = time;
        this.singleUse = singleUse;
    }

    public Timer(int time, bool singleUse, List<Observer> observers)
    {
        if (observers.Count < 1) throw new System.Exception();
        GlobalTime.RegisterTimeObserver(this);
        foreach (Observer obs in observers)
        {
            observable.RegisterObserver(obs);
        }
        this.time = time;
        this.startTime = time;
        this.singleUse = singleUse;
    }

    public void UpdateObserver()
    {
        time--;
        if (time <= 0)
        {
            observable.NotifyObservers();
            if (singleUse)
            {
                observable.UnregisterAllObservers();
                GlobalTime.UnregisterTimeObserver(this);
            } else
            {
                time = startTime;
            }
        }
    }
}
