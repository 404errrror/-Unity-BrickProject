using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_SceneLoader : MonoBehaviour {
    public Animator m_animator;
	// Use this for initialization

    public void SceneLoad(string sceneName)
    {
        StopCoroutine("SceneLoad_");
        StartCoroutine("SceneLoad_", sceneName);
    }

    IEnumerator SceneLoad_(string sceneName)
    {
        m_animator.SetTrigger("End");

        for(;;)
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                SceneLoader.instance.SceneLoad(sceneName);
                yield break;
            }
            yield return null;
        }
    }

}
