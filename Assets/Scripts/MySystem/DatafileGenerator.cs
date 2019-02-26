using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class DatafileGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FileCheckAndCreate_StageData(Application.dataPath + "/Resources/StageData.json");
        FileCheckAndCreate_StageData(Application.dataPath + "/Resources/User_StageData.json");
    }
	
	void FileCheckAndCreate(string path)
    {
        if (File.Exists(path) == false)
            File.Create(path);
    }

    void FileCheckAndCreate_StageData(string path)
    {
        if (File.Exists(path) == false)
        {
            List<StageData> tempList = new List<StageData>();
            StageData tempData = new StageData();
            tempData.stageName = "Initialize";
            tempList.Add(tempData);

            JsonData tempJson = JsonMapper.ToJson(tempList);
            File.WriteAllText(path, tempJson.ToString());
        }
    }
}
