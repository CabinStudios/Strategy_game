using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] Text resourceText;

    public void Display(string resource)
    {
        resourceText.text = resource;
    }
}
