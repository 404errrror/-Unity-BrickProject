using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNameText : MonoBehaviour {

    static InputField m_field;

    void Start()
    {
        m_field = GetComponent<InputField>();
    }

    public void ChangeText(string temp)
    {
        string text = m_field.text;
        StageManager.instance.stageData.stageName = text;
    }
}
