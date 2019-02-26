using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Screen.SetResolution(Screen.width, Screen.width * 16 / 9 , false, 60);
	}
}
