using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour, Observer
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private WalkSpeeds speeds;

    private PathFinding pathFinding;
    private List<Vector3Int> destPoints = new List<Vector3Int>();
    private int currentPoint = 0;
    private bool walking = true;
    private bool waiting = false;
    private int days = 0;

    public void Walk(Vector3 destination)
    {
        Debug.Log("Moving");
        destPoints = pathFinding.PathFind(transform.position, destination, speeds);
        walking = true;
        currentPoint = 0;
        days = 0;
    }

    public bool AtTarget()
    {
        return !walking;
    }

    void Awake()
    {
        pathFinding = GameObject.Find("GameManager").GetComponent<PathFinding>();
    }

    void Start()
    {
        GlobalTime.RegisterTimeObserver(this);
    }

    void Update()
    {
        if (walking && destPoints.Count > currentPoint)
        {
            Vector3 point = pathFinding.GetRealPos(destPoints[currentPoint].x, destPoints[currentPoint].y);
            if (!waiting)
            {
                transform.position = point;
                days = 0;
                waiting = true;
            } else
            {
                if (days >= destPoints[currentPoint].z * moveSpeed / 5) 
                    next();
            }
        }
    }

    public void UpdateObserver()
    {
        days++;
    }

    private void next()
    {
        currentPoint++;
        waiting = false;
        if (currentPoint == destPoints.Count - 1)
        {
            walking = false;
            Debug.Log("Stoped Walking");
        }
    }
}
