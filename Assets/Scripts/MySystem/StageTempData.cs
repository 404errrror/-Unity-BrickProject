using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTempData : MonoBehaviour {
    static public StageTempData instance;
    string m_stageName;

	// Use this for initialization
	void Start () {
        if (instance == null)                           //Singleton
        {
            instance = GetComponent<StageTempData>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}

    public string stageName
    {
        get { return m_stageName; }
        set { m_stageName = value; }
    }
}
