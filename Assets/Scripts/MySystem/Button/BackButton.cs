using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {
    public enum State
    {
        Ignore, StageMode, NetworkMode, Game, StageEditor, MyMap
    }

    public static BackButton instance;
    public State m_state;
    public Animator m_animator;

	// Use this for initialization
	void Start () {
            instance = transform.GetComponent<BackButton>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackButtonOn();
	}

    public void BackButtonOn()
    {
        //if(m_state != State.Ignore)
        //    SceneLoader.instance.SceneBack();  // 차후에 만들기. 너무 오래걸림.

        switch (m_state)
        {
            case State.Game:
                if (StageManager.instance.m_isStage)
                    SceneLoader.instance.SceneLoad("StageMode");
                else
                    SceneLoader.instance.SceneLoad("MyMap");
                break;

            case State.StageEditor:
                SceneLoader.instance.SceneLoad("MyMap");
                break;

            case State.MyMap:
                SceneLoader.instance.SceneLoad("NetworkMode");
                break;

            case State.NetworkMode:
            case State.StageMode:
                SceneLoader.instance.SceneLoad("SelectMode");
                break;

            case State.Ignore:
            default:
                break;
        }
    }
}
