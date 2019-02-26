using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButtonAnimation : MonoBehaviour {
    static public MyButtonAnimation instance;

    Animator m_animator;
	// Use this for initialization
	void Start () {
        instance = GetComponent<MyButtonAnimation>();
        m_animator = GetComponent<Animator>();
	}

    public void SetBool(string name,bool value)
    {
        m_animator.SetBool(name, value);
    }

    public bool GetBool(string name)
    {
        return m_animator.GetBool(name);
    }

    /// <summary>
    /// 애니메이션 컴포넌트를 활성화 시킵니다.
    /// </summary>
    public void ActiveAnimation()
    {
        m_animator.enabled = true; 
    }

    /// <summary>
    /// 애니메이션 컴포넌트를 비활성화 시킵니다. (현재 애니메이션이 끝날 때)
    /// </summary>
    public void InactiveAnimation()
    {
        StartCoroutine("InactiveAni");
    }
    IEnumerator InactiveAni()
    {
        for (;;)
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("ButtonCheck"))
            {
                m_animator.enabled = false;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

   
}
