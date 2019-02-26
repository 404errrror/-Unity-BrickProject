using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MyButton : MonoBehaviour {
    static public List<MyButton> buttonList;
    public int m_sortingOrder;
    BoxCollider2D m_buttonColl;

    public virtual void Awake()
    {
        if (buttonList == null)
            buttonList = new List<MyButton>();
        buttonList.Add(this);
    }

	public virtual void Start () {
        m_buttonColl = GetComponent<BoxCollider2D>();
	}

    public virtual void Update()
    {

    }

    /// <summary>
    /// 버튼안에 마우스가 있을경우 true를 반환합니다.
    /// </summary>
    public bool IsMouseIn()
    {
        Vector2 myScale = transform.lossyScale;
        Vector2 collSize = m_buttonColl.size;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 사각형이 왼쪽 아래 기준으로 생성되기 때문에 약간 이동된 상태로 저장되야함.
        Rect buttonRange = new Rect(
            (Vector2)transform.position - new Vector2(myScale.x * collSize.x * 0.5f, myScale.y * collSize.y * 0.5f),
            new Vector2(collSize.x * myScale.x, collSize.y * myScale.y)
            );

        if (buttonRange.xMin <= mousePos.x &&
            buttonRange.xMax >= mousePos.x &&
            buttonRange.yMin <= mousePos.y &&
            buttonRange.yMax >= mousePos.y)
            return true;
        else
            return false;
    }

    public virtual void ButtonDown()
    {
    }

    public virtual void ButtonUp()
    {
    }

    public virtual void ButtonClick()
    {
    }

    static public void Quit()
    {
        buttonList.Clear();
    }
}
