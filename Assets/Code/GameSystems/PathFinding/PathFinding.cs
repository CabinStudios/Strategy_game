using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class PathFinding : MonoBehaviour
{
    [SerializeField] Sprite map;
    [SerializeField] SpriteRenderer spriteRenderer;

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
    }

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
                        if (newG < ngb.gCost)
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

    private int CalculateHCost(PathNode node, PathNode endNode)
    {
        
        int hgt = Mathf.Abs(endNode.Cords().y - node.Cords().y);
        int wdt = Mathf.Abs(endNode.Cords().x - node.Cords().x);

        int ret = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(wdt, 2) + Mathf.Pow(hgt, 2))) * 10;

        return ret;
    }

    #region Get positions
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
    #endregion
}
