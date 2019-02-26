using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyField : MonoBehaviour {
    Dropdown m_dropDown;
	// Use this for initialization
	void Start () {
        m_dropDown = GetComponent<Dropdown>();
	}
	
	public void StateChange()
    {
        StageManager.instance.stageData.difficulty = m_dropDown.value;
    }
}
