using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMapCont : MonoBehaviour {
    public Animator m_animator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewMapButton()
    {
        StageManager.instance.m_isStage = false;

        SceneLoader.instance.SceneLoad("StageEditor");
    }
}
