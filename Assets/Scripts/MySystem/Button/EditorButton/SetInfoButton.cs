using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInfoButton : MonoBehaviour {
    static Animator m_animator;
    static GameObject m_fade;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        m_fade = transform.FindChild("Fade").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetInfoShow()
    {
        m_animator.enabled = true;
        m_animator.SetBool("ShowSetInfo", true);
        m_fade.SetActive(true);
    }

    public void SetInfoHide()
    {
        m_animator.SetBool("ShowSetInfo", false);
        StartCoroutine("FadeObjectHide");
    }

    IEnumerator FadeObjectHide()
    {
        for(;;)
        {
            if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("BoolCheck"))
            {
                m_fade.SetActive(false);
                m_animator.enabled = false;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
