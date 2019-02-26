using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickEditor : MonoBehaviour {
    static public BrickEditor instance;

    BrickItem m_editBrick;
    bool m_isEdit;

    void Start()
    {
        instance = GetComponent<BrickEditor>();
        m_editBrick = null;
        m_isEdit = false;
    }

    void Update()
    {
        InputUp();         // 버튼 외의 마우스 업을 감지해야하므로.
        BrickMove();
    }

    void InputUp()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
#elif UNITY_ANDROID || UNITY_IPHONE
        if(Input.touchCount == 0)
#else
        if (Input.GetMouseButtonUp(0))
#endif
        {
            if (m_editBrick != null)
            {
                if(BackgroundInfo.instance.IsIn(m_editBrick.transform.position))
                    m_editBrick.CompleteSetup();
                else
                    m_editBrick.Remove();

                MyButtonAnimation.instance.SetBool("isBrickEdit", false);
                m_editBrick = null;
            }
        } // end Input
    } // end Function

    public void BrickDown(BrickItem brick)
    {
        m_editBrick = brick;
        m_isEdit = true;
        MyButtonAnimation.instance.SetBool("isBrickEdit", true);
    }

    void BrickMove()
    {
        if (m_editBrick != null)
        {
#if UNITY_EDITOR
            m_editBrick.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IPHONE
            m_editBrick.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(Input.touchCount - 1).position);
#else
            m_editBrick.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif
            if(BackgroundInfo.instance.IsIn(m_editBrick.transform.position) == false)
                m_editBrick.RemoveReady();
            else
                m_editBrick.RemoveCancel();
        }
    }
}
