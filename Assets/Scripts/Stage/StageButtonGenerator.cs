using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtonGenerator : MonoBehaviour {
    public GameObject stageButton;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < StageManager.instance.stageAllData.Count; ++i)
        {
            StageData stageInfo = StageManager.instance.stageAllData[i];

            GameObject tempObject = Instantiate(stageButton,transform);
            RectTransform rectTransform = tempObject.GetComponent<RectTransform>();
            tempObject.transform.FindChild("Text").GetComponent<Text>().text = stageInfo.stageName;
            tempObject.transform. GetComponent<StageSelect>().m_stageName = stageInfo.stageName;

            if (i % 2 == 0)
            {
                rectTransform.localPosition = new Vector3(-236, -100 - ((int)(i * 0.5f) * 200));
            }
            else
            {
                rectTransform.localPosition = new Vector3(236, -100 - ((int)(i * 0.5f) * 200));

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
