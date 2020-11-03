using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class WalkSpeeds
{
    [SerializeField] private ElementDictionary speedRatio;

    private Dictionary<float, float> trueRatio = new Dictionary<float, float>();

    public int GetSpeed(Color color, int dist)
    {
        if (!speedRatio.ContainsKey(color)) return 100;

        int ret = Mathf.RoundToInt(dist * speedRatio[color]);
        return ret;
    }

    public void SetSpeed(Color color, float ratio)
    {
        if(!speedRatio.ContainsKey(color))
        {
            speedRatio.Add(color, ratio);
        } else
        {
            speedRatio[color] = ratio;
        }
    }
}
