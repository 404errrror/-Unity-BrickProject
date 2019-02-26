using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkModeClick : MonoBehaviour {
    Animator m_animator;

	void Start () {
        m_animator = GetComponent<Animator>();
	}

    public void ClickMyMap()
    {
        m_animator.SetTrigger("MyMapClick");
        StartCoroutine("StartMyMap");
    }
    IEnumerator StartMyMap()
    {
        for(; ; )
        {
            if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("MyMapClickEnd"))
            {
                SceneLoader.instance.SceneLoad("MyMap");
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
