using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {
    public string m_stageName;
    static Animator m_animator;

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "StageMode")
            m_animator = transform.parent.GetComponent<Animator>();
        else if (sceneName == "MyMap")
            m_animator = transform.parent.parent.parent.parent.GetComponent<Animator>();
        else
            Debug.LogError("해당되는 Scene이 없습니다. Scene Name을 확인해주세요.");

    }

    public void StageClick()
    {
        m_animator.SetTrigger("StageClick");

        StageTempData.instance.stageName = m_stageName;
        StopCoroutine("ChangeScene");       // 중복 load 방지.
        StartCoroutine("ChangeScene");
    }

    public void UserStageClick()
    {
        m_animator.SetTrigger("StageClick");  //-------------> 나중에 씬 이동하는 Animation 만들기. GetComponent로 SetTrigger 제어.

        StageTempData.instance.stageName = m_stageName;
        StageManager.instance.m_isStage = false;
        //SceneLoader.instance.SceneLoad("TestScene");

        StopCoroutine("ChangeScene");       // 중복 load 방지.
        StartCoroutine("ChangeScene");
    }

    IEnumerator ChangeScene()
    {

        for (;;)
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                SceneLoader.instance.SceneLoad("TestScene");
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator SceneBack()
    {
        for (;;)
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                SceneLoader.instance.SceneLoad("TestScene");
                yield break;
            }
            yield return null;
        }
    }
}
