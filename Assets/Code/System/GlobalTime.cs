using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    [SerializeField] private static Observable observable = new ConcreteObserver();

    [SerializeField] private float hourLength = 0.1f;

    private float counter = 0;

    private void FixedUpdate()
    {
        counter += Time.fixedDeltaTime;

        if(counter > hourLength)
        {
            observable.NotifyObservers();
            counter = 0;
        }
    }

    public static void RegisterTimeObserver(Observer observer)
    {
        observable.RegisterObserver(observer);
    }

    public static void UnregisterTimeObserver(Observer observer)
    {
        observable.UnregisterObserver(observer);
    }
}
