using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyBackground : MonoBehaviour {
    static public DontDestroyBackground instance;
	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}
}
