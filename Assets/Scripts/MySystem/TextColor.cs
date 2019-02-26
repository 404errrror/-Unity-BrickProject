using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class TextColor : MonoBehaviour {
    public Color m_textColor = Color.white;
    TextMesh m_textMesh;

	void Start () {
        m_textMesh = GetComponent<TextMesh>();
	}
	
	void Update () {
        m_textMesh.color = m_textColor;
	}
}
