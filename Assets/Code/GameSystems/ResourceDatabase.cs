using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDatabase : MonoBehaviour
{
    [SerializeField] private Dictionary<string, Image> resourceImages;

    public Image GetImage(string resource)
    {
        return resourceImages[resource]; 
    }
}
