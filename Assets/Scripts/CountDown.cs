using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour {
    static public CountDown instance;
    TextMesh m_textMesh;
    Animator m_animator;

    float m_tempTime;         // 1초 세는 변수.
    int m_nowSeconds;
	// Use this for initialization
	void Start () {
        instance = GetComponent<CountDown>();
        m_textMesh = GetComponent<TextMesh>();
        m_animator= GetComponent<Animator>();
        m_tempTime = 0;
        m_nowSeconds = 10;
	}

    void Update()
    {
        if (GameManager.gameStart && GameManager.gamePause == false && GameManager.gameClear == false)
        {
            m_tempTime += Time.deltaTime;
            if (m_tempTime >= 1)
            {
                m_tempTime -= 1;
                m_nowSeconds -= 1;
                if (m_nowSeconds <= -1)
                {
                    GameStartManager.instance.TimeOver();
                    return;
                }
                m_textMesh.text = m_nowSeconds.ToString();

            }
        }
    }

    public int seconds
    {
        get { return m_nowSeconds; }
        set
        {
            m_nowSeconds = value;
            m_textMesh.text = m_nowSeconds.ToString();
        }
    }

    public Animator animator
    {
        get { return m_animator; }
    }
}
