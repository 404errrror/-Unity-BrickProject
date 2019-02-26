using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectModeClick : MonoBehaviour {
    Animator m_animator;

	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
	}
	
	public void ClickStageMode()
    {
        m_animator.SetTrigger("ClickStageMode");
        StartCoroutine(StartScene("StageMode"));
    }

    public void ClickNetworkMode()
    {
        m_animator.SetTrigger("ClickNetworkMode");
        StartCoroutine("StartScene_NetworkMode");
    }
    IEnumerator StartScene(string sceneName)
    {
        for(;;)
        {
            if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                SceneLoader.instance.SceneLoad(sceneName);
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
	IEnumerator StartScene_NetworkMode()
	{
		for(;;)
		{
			if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("ClickNetworkModeEnd"))
			{
				SceneLoader.instance.SceneLoad("NetworkMode");
				yield break;
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
