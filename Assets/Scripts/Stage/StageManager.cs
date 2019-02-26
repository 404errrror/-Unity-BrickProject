using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class StageManager : MonoBehaviour {
    static public StageManager instance;
    public Animator m_animator;
    public bool m_isStage = false;

   StageData m_stageDataList;
    List<StageData> m_stageAllData;
    List<string>    m_nameData;
	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = GetComponent<StageManager>();
            m_stageDataList = new StageData();
            m_nameData = new List<string>();

            m_stageAllData = JsonParser(LoadJson());

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (SceneLoader.GetActiveSceneName() != "MyMap" &&
                SceneLoader.GetActiveSceneName() != "StageMode")
            {
                m_isStage = instance.m_isStage;
            }

            Destroy(instance.gameObject);

            instance = GetComponent<StageManager>();
            m_stageDataList = new StageData();
            m_nameData = new List<string>();

            m_stageAllData = JsonParser(LoadJson());

            DontDestroyOnLoad(gameObject);
            return;
        }
    }
	

    public void SaveStageData()
    {
        stageData.brickData = new List<BrickData>();
        foreach(var it in FindObjectsOfType<BrickItem>())           // 블럭 찾기.
        {
            if (it.isSetup)
            {
                BrickData temp = new BrickData(it.brickId, it.pivot.x, it.pivot.y);
                stageData.brickData.Add(temp);
            }
        }

        if (SaveStageDataReturn())
            m_animator.SetTrigger("Save");
        return;
    }

    public bool SaveStageDataReturn()
    {
        // 값들을 제대로 입력했는지 체크.
        if (
             stageData.stageName == "" ||
              stageData.gold == 0 ||
              stageData.brickData.Count == 0
           )
        {
            Debug.LogError("Save Failed!!");
            return false;
        }
        //List<StageData> allData = JsonParser(LoadJson());
        m_stageAllData.Add(stageData);
        JsonData jsonAllData = JsonMapper.ToJson(m_stageAllData);

        Debug.Log(m_stageAllData.Count);

        if(m_isStage)
            File.WriteAllText(Application.dataPath + "/Resources/StageData.json", jsonAllData.ToString());     // 파일 만들기.
        else
            File.WriteAllText(Application.dataPath + "/Resources/User_StageData.json", jsonAllData.ToString());     // 파일 만들기.

        return true;
    }
    public JsonData LoadJson()
    {

        string loadString;

        if (m_isStage)
            loadString = File.ReadAllText(Application.dataPath + "/Resources/StageData.json");
        else
            loadString = File.ReadAllText(Application.dataPath + "/Resources/User_StageData.json");

        return JsonMapper.ToObject(loadString);
    }

    public List<StageData> JsonParser(JsonData jsonData)
    {
        List<StageData> dataList = new List<StageData>();
        for (int dataCount = 0; dataCount < jsonData.Count; ++dataCount)
        {
            JsonData nowJsonData = jsonData[dataCount];
            StageData tempData = new StageData();

            tempData.stageName = nowJsonData["stageName"].ToString();
            int.TryParse(nowJsonData["difficulty"].ToString(), out tempData.difficulty);
            int.TryParse(nowJsonData["gold"].ToString(), out tempData.gold);

            for (int i = 0; i < nowJsonData["brickData"].Count; ++i)                        // 벽돌 데이터 가져오기.
            {
                BrickData tempBrickData = new BrickData(
                    int.Parse(nowJsonData["brickData"][i]["id"].ToString()),
                    double.Parse(nowJsonData["brickData"][i]["x"].ToString()),
                    double.Parse(nowJsonData["brickData"][i]["y"].ToString())
                    );
                tempData.brickData.Add(tempBrickData);
            }

            for (int i = 0; i < nowJsonData["lockBullet"].Count; ++i)                       // 총알 잠금데이터 가져오기.
            {
                tempData.lockBullet = new List<int>();
                tempData.lockBullet.Add(
                    int.Parse(nowJsonData["lockBullet"][i].ToString())
                    );
            }
            dataList.Add(tempData);                 // 데이터 리스트에 저장.
        }

        return dataList;
    }

    public StageData stageData
    {
        get { return m_stageDataList; }
    }

    public List<StageData> stageAllData
    {
        get { return m_stageAllData; }
    }

}
