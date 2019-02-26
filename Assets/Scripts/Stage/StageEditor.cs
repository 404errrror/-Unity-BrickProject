using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEditor : MonoBehaviour {
    static public StageEditor instance;

    public int gold;
    public string stageName;

	// Use this for initialization
	void Start () {
        instance = GetComponent<StageEditor>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
