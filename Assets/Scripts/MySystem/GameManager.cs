using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    static public bool gameStart;
    static public bool gamePause;
    static public bool gameOver;
    static public bool gameClear;

    static public string stageName;
    static public int limitGold;
    static public StageData nowStageData;

    static public bool debugMode;
	void Awake () {
        gameStart = false;
        gamePause = false;
        gameOver = false;
        gameClear = false;
        Application.targetFrameRate = 60;

        if (StageTempData.instance == null)
        {
            debugMode = true;
            limitGold = 100;
        }
        else                                                        // 데이터를 받아서 스테이지 생성.
        {
            debugMode = false;
            stageName = StageTempData.instance.stageName;
            nowStageData = FindNowStage();
            if (nowStageData == null)
                Debug.LogError("Stage를 찾을 수 없습니다. Stage Name을 확인해주세요.");
            limitGold = nowStageData.gold;
        }
	}

    static public StageData FindNowStage()
    {
        foreach (var it in StageManager.instance.stageAllData)
            if (it.stageName == stageName)
                return it;
        return null;
    }
}
