using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMethods { 

    public static void ListResources(List<ColapsedObject<string>> resources, Text temp, int contentDisplace, GameObject gameObject)
    {
        if (resources.Count == 0)
            temp.gameObject.SetActive(false);
        else if (resources.Count == 0)
            temp.text = resources[0].ammount + "x " + resources[0].obj;
        else
        {


            temp.text = resources[0].ammount + "x " + resources[0].obj;
            for (int i = 1; i < resources.Count; i++)
            {
                GameObject obj = GameObject.Instantiate(temp.gameObject, gameObject.GetComponent<RectTransform>()) as GameObject;
                obj.GetComponent<Text>().text = resources[i].ammount + "x " + resources[i].obj;
                RectTransform rt = obj.GetComponent<RectTransform>();
                rt.localPosition += new Vector3(0, (rt.sizeDelta.y + contentDisplace) * (-i), 0);

                if (gameObject.GetComponent<RectTransform>().sizeDelta.y < -rt.localPosition.y + rt.sizeDelta.y + contentDisplace)
                    gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, -rt.localPosition.y + rt.sizeDelta.y + contentDisplace);
            }
        }
    }


    public static List<ColapsedObject<string>> ColapseResources(List<string> resources)
    {
        List<string> colapsedResources = new List<string>();
        List<int> ammount = new List<int>();
        for (int i = 0; i < resources.Count; i++)
        {
            if (colapsedResources.Contains(resources[i]))
            {
                int j = 0;
                while (true)
                {
                    if (colapsedResources[j].Equals(resources[i]))
                        break;
                    j++;
                }

                ammount[j]++;
            }
            else
            {
                colapsedResources.Add(resources[i]);
                ammount.Add(1);
            }
        }

        List<ColapsedObject<string>> ret = new List<ColapsedObject<string>>();

        for (int i = 0; i < colapsedResources.Count; i++)
        {
            ret.Add(new ColapsedObject<string> { obj = colapsedResources[i], ammount = ammount[i] });
        }
        return ret;
    }
}

public struct ColapsedObject<T>
{
    public T obj;
    public int ammount;
}

