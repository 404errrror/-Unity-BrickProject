using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickButton : MyButton {
    public BrickItem m_target;
    public int amount;

    BrickItem[] m_brickPool;

    public override void Start()
    {
        base.Start();
    }
    public override void Awake()                // ButtonSensor에 감지되기 위해 ButtonSensor가 초기화되기 전에 인스턴스화 시켜주어야함.
    {
        base.Awake();
        m_brickPool = new BrickItem[amount];
        for (int i = 0; i < amount; ++i)
        {
            m_brickPool[i] = Instantiate(m_target, transform);
            m_brickPool[i].transform.localPosition = Vector2.zero;
            m_brickPool[i].transform.localScale = Vector2.one;
            m_brickPool[i].gameObject.SetActive(false);
        }
        m_brickPool[0].gameObject.SetActive(true);
//m_target.gameObject.SetActive(false);
    }
    public override void ButtonUp()
    {
    }

    public override void ButtonDown()
    {
    }

    public void ChildActivateOne()
    {
        foreach(var it in m_brickPool)
            if(it.gameObject.activeInHierarchy == false)
            {
                it.gameObject.SetActive(true);
                return;
            }
    }
}
