using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class Fps : MonoBehaviour {
    TextMesh m_textMesh;
    float m_fps;
	// Use this for initialization
	void Start () {
        m_textMesh = GetComponent<TextMesh>();
        m_fps = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_fps = 1 / Time.deltaTime;
        m_textMesh.text = "Fps : " + m_fps;
	}
}
