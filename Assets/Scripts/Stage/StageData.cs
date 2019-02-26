using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageData{
    public string stageName;
    public int difficulty;
    public int gold;
    public List<BrickData> brickData = new List<BrickData>();
    public List<int> lockBullet = new List<int>();
}

public class BrickData
{
    public int id;
    public double x;
    public double y;

    public BrickData(int id, double x, double y)
    {
        this.id = id;
        this.x = x;
        this.y = y;
    }
}