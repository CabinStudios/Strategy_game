using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMenu : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] RectTransform content;
    [SerializeField] int contentDisplace;

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < content.childCount; i++)
        {
            GameObject.Destroy(content.GetChild(i).gameObject);
        }
    }

    public void Display (List<GameObject> items, string name)
    {
        nameText.text = name;
        float ySize = contentDisplace;
        float xPos = content.parent.parent.GetComponent<RectTransform>().sizeDelta.x / 2 + content.parent.GetComponent<RectTransform>().sizeDelta.x/2;

        for (int i = 0; i < items.Count; i++)
        {
            float posY = 0;
            if (i > 0)
            {
                RectTransform rt = items[i-1].GetComponent<RectTransform>();
                posY = (rt.sizeDelta.y + contentDisplace);
            }
            items[i].GetComponent<RectTransform>().localPosition = new Vector2(xPos, posY * (-i) - contentDisplace);
            ySize += posY;
        }
        content.sizeDelta = new Vector2(content.sizeDelta.x, ySize);
        gameObject.SetActive(true);
    }

    public RectTransform GetContent()
    {
        return content;
    }
}
