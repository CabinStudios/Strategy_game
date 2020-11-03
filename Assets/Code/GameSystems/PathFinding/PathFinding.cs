using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class PathFinding : MonoBehaviour
{
    [SerializeField] Sprite map;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<Transform> test;

    /*[SerializeField] WalkSpeeds walkSpeed;
    [SerializeField] GameObject token;
    [SerializeField] Color[] tradeRoutColor;
    List<List<Vector3>> routes = new List<List<Vector3>>();
    int c = 0;
    int r = 0;*/

    private float ratio;
    private int width;
    private int height;
    Vector3 origin;

    private PathNode [,] pathMap;
    private List<PathNode> openList;
    private List<PathNode> closedList;


    private void Start()
    {
        ratio = map.pixelsPerUnit / 10;
        width = map.texture.width;
        height = map.texture.height;
        origin.Set(ratio * width / 2, ratio * height / 2, 0);

        spriteRenderer.sprite = map;
        pathMap = new PathNode[width, height];
        
       /* for (int i = 0; i < test.Count-1; i++)
        {
            for (int j = i+1; j < test.Count; j++)
            {
                routes.Add(new List<Vector3> {test[i].position, test[j].position});
            }
        }*/
    }

    /*private void Update()
    {
        
        if (r < routes.Count)
        {
            List<Vector3Int> tst = PathFind(routes[r][0], routes[r][1], walkSpeed);

            foreach (Vector3Int p in tst)
            {
                Vector3 pos = GetRealPos(p.x, p.y);
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
    }*/


    public List<Vector3Int> PathFind(Vector3 from, Vector3 to, WalkSpeeds walkSpeed)
    {
        Debug.Log("Pathfinding from: " + from + " to: " + to);
        Vector2Int fromPix = GetMapPos(from);
        Vector2Int toPix = GetMapPos(to);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                 pathMap[x, y] = new PathNode(x, y);
            }
        }

        PathNode startNode = pathMap[fromPix.x, fromPix.y];
        PathNode endNode = pathMap[toPix.x, toPix.y];
        startNode.gCost = 0;
        startNode.hCost = CalculateHCost(startNode, endNode);
        startNode.CalculateFValue();

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        while(true)
        {
            PathNode currentNode = openList[0];
            foreach (PathNode node in openList)
            {
                if (node.fCost < currentNode.fCost)
                    currentNode = node;
            }

            closedList.Add(currentNode);
            openList.Remove(currentNode);
            if (currentNode == endNode)
            {
                break;
            }

            List<PathNode> closeNeighbor = GetClose(currentNode);

            foreach (PathNode ngb in closeNeighbor)
            {
                if (!closedList.Contains(ngb))
                {
                    int dist;
                    if (currentNode.Cords().x != ngb.Cords().x && currentNode.Cords().y != ngb.Cords().y)
                        dist = 14;
                    else
                        dist = 10;
                    int newG = currentNode.gCost + walkSpeed.GetSpeed((Color)(map.texture.GetPixel(ngb.Cords().x, ngb.Cords().y)), dist);
                    if (openList.Contains(ngb))
                    {
                        if (newG < ngb.gCost || (newG == ngb.gCost && ngb.cameFromNode != null &&  CloseToCentre(ngb.Cords(), ngb.cameFromNode.Cords(),currentNode.Cords(), startNode.Cords(), endNode.Cords())))
                        {
                            ngb.gCost = newG;
                            ngb.CalculateFValue();
                            ngb.cameFromNode = currentNode;
                        }
                    } else
                    {
                        ngb.gCost = newG;
                        ngb.hCost = CalculateHCost(ngb, endNode);
                        ngb.CalculateFValue();
                        ngb.cameFromNode = currentNode;
                        openList.Add(ngb);
                    }
                }
            }
        }

        PathNode findNode = endNode;
        List<Vector3Int> track = new List<Vector3Int>();

        while(true)
        {
            int x = findNode.Cords().x;
            int y = findNode.Cords().y;
            track.Add(new Vector3Int(x, y, walkSpeed.GetSpeed(map.texture.GetPixel(x, y), 10)));
            if (findNode.cameFromNode == null) break;
            findNode = findNode.cameFromNode;
        }

        track.Reverse();
        Debug.Log("Pathfinding finnished");
        return track;
    }

    #region get neighbours 
    private List<PathNode> GetClose (PathNode node)
    {
        int x = node.Cords().x;
        int y = node.Cords().y;

        List<PathNode> ret = new List<PathNode>();

        if (x + 1 < width)
            ret.Add(pathMap[x + 1, y]);
        if(x-1 >= 0)
            ret.Add(pathMap[x - 1, y]);
        if (y + 1 < height)
            ret.Add(pathMap[x, y+1]);
        if (y - 1 >= 0)
            ret.Add(pathMap[x, y - 1]);

        if (x + 1 < width && y + 1 < height)
            ret.Add(pathMap[x + 1, y + 1]);
        if (x - 1 >= 0 && y + 1 < height)
            ret.Add(pathMap[x - 1, y + 1]);
        if (y + 1 < height && x - 1 >= 0)
            ret.Add(pathMap[x - 1, y + 1]);
        if (y - 1 >= 0 && x + 1 < width)
            ret.Add(pathMap[x + 1, y - 1]);
        return ret;
    }
    #endregion

    private bool CloseToCentre(Vector2 me, Vector2 prev, Vector2 cur, Vector2 frm, Vector2 to)
    {
        Vector2 vOrg;
        if (Vector2.Distance(me, frm) > Vector2.Distance(me, to))
            vOrg = new Vector2(me.x - frm.x, me.y - frm.y);
        else
            vOrg = new Vector2(to.x - me.x, to.y - me.y);
        Vector2 vMe = new Vector2(me.x - cur.x, me.y - cur.y);
        Vector2 vPrev = new Vector2(me.x - prev.x, me.y - prev.y);

        vOrg = new Vector2(vOrg.x / vOrg.magnitude, vOrg.y / vOrg.magnitude);
        vMe = new Vector2(vMe.x / vMe.magnitude, vMe.y / vMe.magnitude);
        vMe = new Vector2(vPrev.x / vPrev.magnitude, vPrev.y / vPrev.magnitude);

        Vector2 difMe = new Vector2(vMe.x - vOrg.x, vMe.y - vOrg.y);
        Vector2 difPrev = new Vector2(vPrev.x - vOrg.x, vPrev.y - vOrg.y);

        return (vMe.magnitude < vPrev.magnitude);
    }

    private int CalculateHCost(PathNode node, PathNode endNode)
    {
        int ret = 0;
        int hgt = Mathf.Abs(endNode.Cords().y - node.Cords().y);
        int wdt = Mathf.Abs(endNode.Cords().x - node.Cords().x);
        if (wdt <= hgt)
            ret = 10 * (wdt - hgt) + 14 * hgt;
        else
            ret = 10 * (hgt - wdt) + 14 * wdt;
        return ret;
    }

    private Vector2Int GetMapPos(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x * ratio + width / 2);
        int y = Mathf.RoundToInt(position.y * ratio + height / 2);

        return new Vector2Int(x, y);
    }

    public Vector3 GetRealPos(int x, int y)
    {
        float newX = x / ratio - width/(2*ratio);
        float newY = y / ratio - height/(2*ratio);

        return new Vector3(newX, newY, 0);
    }
}
