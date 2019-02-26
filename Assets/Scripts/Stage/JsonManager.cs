using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;



public class JsonManager : MonoBehaviour {

    public List<BrickData> brickJsonList;
    public List<BrickData> stage;


	void Start () {
        brickJsonList = new List<BrickData>();

        brickJsonList.Add(new BrickData(0, 0.5f, 0.5f));
	}
	

	void SaveFunc () {
        JsonData jsonData = JsonMapper.ToJson(brickJsonList);

        File.WriteAllText(Application.dataPath + "/Resources/StageData.json", jsonData.ToString());
	}

    void LoadFunc()
    {
        string loadString = File.ReadAllText(Application.dataPath + "/Resources/StageData.json");

        Debug.Log(loadString);
        JsonData brickData = JsonMapper.ToObject(loadString);
        ParsingJson(brickData);
    }


    void ParsingJson(JsonData jsonData)
    {
        for (int i = 0; i < jsonData.Count; ++i)
            Debug.Log(jsonData[i]["id"]);
    }
}
