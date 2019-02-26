using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {
    public Brick brickId0;
    public Brick brickId1;

    Transform m_stage;

    void Start()
    {
        m_stage = GameObject.Find("Stage").transform;
        if(GameManager.debugMode == false)
            GenerateStage(GameManager.nowStageData.brickData);
    }

    public void GenerateStage(List<BrickData> brickData)
    {
        foreach(var it in brickData)
            GenerateBrick(it.id, (float)it.x, (float)it.y);
    }

    public bool GenerateBrick(int id, float x, float y)
    {
        switch(id)
        {
            case 0:
                Instantiate(brickId0.gameObject,new Vector2(x,y),Quaternion.identity,m_stage);
                return true;
            case 1:
                Instantiate(brickId1.gameObject, new Vector2(x, y), Quaternion.identity, m_stage);
                return true;
            default:
                return false;
        }
    }
}
