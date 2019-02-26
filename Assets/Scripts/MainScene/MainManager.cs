using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach(var it in Bullet.m_activeList)
        {
            it.gameObject.SetActive(true);
            it.moveDir = new Vector2(Random.Range(-1.0f, 1.0f), 1).normalized;
        }
        GameStartManager.instance.GameStart();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
