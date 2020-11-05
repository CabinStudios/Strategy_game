using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPathFind : MonoBehaviour
{
    [SerializeField] PathFinding pathFinding;
    [SerializeField] List<Transform> test;
    [SerializeField] WalkSpeeds walkSpeed;
    [SerializeField] GameObject token;
    [SerializeField] Color[] tradeRoutColor;
    List<List<Vector3>> routes = new List<List<Vector3>>();
    int c = 0;
    int r = 0;

    private void Start()
    {
        for (int i = 0; i < test.Count - 1; i++)
        {
            for (int j = i + 1; j < test.Count; j++)
            {
                routes.Add(new List<Vector3> { test[i].position, test[j].position });
            }
        }
    }

    private void Update()
    {

        if (r < routes.Count)
        {
            List<Vector3Int> tst = pathFinding.PathFind(routes[r][0], routes[r][1], walkSpeed);

            foreach (Vector3Int p in tst)
            {
                Vector3 pos = pathFinding.GetRealPos(p.x, p.y);
                GameObject obj = GameObject.Instantiate(token, new Vector3(pos.x, pos.y, 0), transform.rotation, this.transform) as GameObject;

                obj.GetComponent<SpriteRenderer>().color = tradeRoutColor[c];
            }

            c++;
            if (c > tradeRoutColor.Length - 1)
            {
                c = 0;
            }

            r++;
        }
    }
}
