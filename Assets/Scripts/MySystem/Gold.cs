using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {
    static public Gold instance;

    TextMesh m_textMesh;
    int m_maxGold;
    int m_gold;
	// Use this for initialization
	void Start () {
        instance = transform.GetComponent<Gold>();
        m_textMesh = transform.GetComponent<TextMesh>();

        m_maxGold = GameManager.limitGold;
        m_gold = m_maxGold;
        m_textMesh.text = m_gold.ToString();
	}

    public void SetMaxGold(int maxGold)
    {
        m_maxGold = maxGold;
    }

    public void ShowCost(int cost)
    {
        m_textMesh.text += " - " + cost;
    }

    public void RefreshGold()
    {
        m_textMesh.text = m_gold.ToString();
    }

    public int gold
    {
        get { return m_gold; }
        set 
        { 
            m_gold = value;
            m_textMesh.text = m_gold.ToString();
        }
    }
}
