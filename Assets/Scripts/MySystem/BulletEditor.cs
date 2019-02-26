using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEditor : MonoBehaviour {


    List<Bullet>        m_bulletList;
    Bullet              m_edittingBullet;
    ControlType         controlType;
    enum ControlType { None, Move, Angle };

	void Start () {
        m_bulletList = new List<Bullet>();
        foreach (var it in GameObject.FindObjectsOfType<Bullet>())
        {
            m_bulletList.Add(it);
            if (it.m_isInit == false)
                it.Start();
            it.gameObject.SetActive(false);
        }
        controlType = ControlType.None;
	}
	
	void Update () {

#if(UNITY_EDITOR)
        MouseInput();
#elif(UNITY_ANDROID || UNITY_IPHONE)
        TouchInput();
#else
        MouseInput();
#endif

    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            InputDown();
        else if (Input.GetMouseButtonUp(0))
            InputUp();
        else if (Input.GetMouseButton(0))
            InputMove();
    }

    void TouchInput()
    {
        foreach(var touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
                InputDown();

            else if (touch.phase == TouchPhase.Ended)
                InputUp();

            else if (touch.phase == TouchPhase.Moved)
                InputMove();

        }   // End foreach.
    }


    void InputDown()
    {
        foreach (var it in m_bulletList)            // 수정할 총알 찾기. 찾았다면 컨트롤타입은 Move.
        {

            if (it.IsMouseIn())
            {
                m_edittingBullet = it;
                it.m_isEditting = true;
                controlType = ControlType.Move;
                return;
            }

        }

        if (m_edittingBullet == null)               // 수정할 총알이 없을 때
            return;
        else if (SetBulletButton.instance.IsMouseIn() || CenterButton.instance.IsMouseIn())        // SetBullet버튼이나 GameStart 버튼 눌렀을 경우.
        {
            m_edittingBullet = null;
            return;
        }

        else
        {
            controlType = ControlType.Angle;
            foreach (var it in m_bulletList)
                if (it.IsMouseIn())
                {
                    controlType = ControlType.None;
                    break;
                }
        }

    }

    void InputUp()
    {
        if (m_edittingBullet != null)
        {
            if (m_edittingBullet.isRemove == true)
            {
                m_edittingBullet.Remove();
                return;
            }

            m_edittingBullet.m_isLineup = true;
            m_edittingBullet.m_isEditting = false;
        }
    }

    void InputMove()
    {
        if (m_edittingBullet != null)
        {
            switch (controlType)
            {
                case ControlType.None:
                    break;
                case ControlType.Move:
                    m_edittingBullet.Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    break;
                case ControlType.Angle:
                    m_edittingBullet.EditBulletDir(((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_edittingBullet.transform.position)).normalized);
                    break;
                default:
                    break;
            }

        }
    }
}