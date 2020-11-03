using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface Observable
{
    void RegisterObserver(Observer observer);
    void UnregisterObserver(Observer observer);
    void UnregisterAllObservers();
    void NotifyObservers();
}
