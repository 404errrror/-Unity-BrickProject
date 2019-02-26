using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickItem : MyButton {
    static Transform stage;
    public int brickId = 0;

    BrickButton m_parent;
    SpriteRenderer m_Render;
    Color m_originColor;
    Vector2 m_pivot;
    bool m_isLine;
    bool m_isSetup;
	// Use this for initialization
	public override void Start () {
        base.Start();
        if(stage == null)
            stage = GameObject.Find("Stage").transform;
        m_parent = transform.parent.GetComponent<BrickButton>();
        m_Render = GetComponent<SpriteRenderer>();
        m_originColor = m_Render.color;
        m_pivot = transform.position;
        m_isLine = false;
	}

    public override void Update()
    {
        base.Update();
        if(m_isLine)
            Lining();
    }


    public override void ButtonDown()
    {
        base.ButtonDown();
        // 스테이지로 부모를 옮기고, 있던 자리를 매꾸기위해 자식을 Activate 합니다.
        transform.SetParent(stage);
        BrickEditor.instance.BrickDown(this);
        if (isSetup == false)
            m_parent.ChildActivateOne();
        m_isLine = false;
    }

    public void CompleteSetup()
    {
        m_Render.sortingOrder = 100;
        m_pivot = InputInfo.TilePosition(1);
       // transform.position = 
        m_isLine = true;
        m_isSetup = true;
    }

    public void RemoveReady()
    {
        m_Render.color += new Color(255, 0, 0);
    }

    public void RemoveCancel()
    {
        m_Render.color = m_originColor;
    }

    public void Remove()
    {
        m_Render.color = m_originColor;
        m_Render.sortingOrder = 1300;
        transform.SetParent(m_parent.transform);
        transform.localPosition = Vector2.zero;
        m_isSetup = false;
        gameObject.SetActive(false);                // 안되고 있는듯.
        
    }

    void Lining()
    {
        transform.position += new Vector3(m_pivot.x - transform.position.x,m_pivot.y - transform.position.y, 0) * Time.deltaTime * 15;
    }

    public Vector2 pivot
    {
        get { return m_pivot; }
    }

    public bool isSetup
    {
        get { return m_isSetup; }
    }
}
