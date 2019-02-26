using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitGoldText : MonoBehaviour {
    static public LimitGoldText instance;

    static InputField m_field;
    static Text m_text;
    static string m_backup; 
	// Use this for initialization
	void Start () {
        instance = GetComponent<LimitGoldText>();

        m_field = GetComponent<InputField>();
	}
	
	public void ChangeText(string temp)
    {
        int gold;
        bool isSuccess;
        string text = m_field.text;

        isSuccess = int.TryParse(text, out gold);
        if (isSuccess)
        {
            StageManager.instance.stageData.gold = gold;
            m_field.text = text;
            m_backup = text;
        }
        else
            m_field.text = m_backup;
    }
}
