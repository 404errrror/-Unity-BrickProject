using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {                       // static public 으로 다 변경하기
    static public SceneLoader instance;
    public Animator m_animator;

    void Start()
    {
        instance = GetComponent<SceneLoader>();
    }

    public void SceneLoad(string sceneName)
    {
        if (MyButton.buttonList != null)
            MyButton.Quit();
        if (Brick.activeList != null)
            Brick.Quit();
        if (Bullet.m_activeList != null)
            Bullet.Quit();
        SceneManager.LoadScene(sceneName);
    }

    static public string GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }


    public void SceneBack()
    {
        StopCoroutine("SceneBack_");
        StartCoroutine("SceneBack_");
    }
    IEnumerator SceneBack_()
    {
        string nowScene = SceneManager.GetActiveScene().name;
        m_animator.SetTrigger("BackButton");
        for(;;)     // 나중에 DontDestoryOnLoad로 하고 이전씬 정보를 저장하도록 수정.
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Back_End"))
            {
                if (nowScene == "StageMode" || nowScene == "NetworkMode")
                    SceneManager.LoadScene("SelectMode");
                else if (nowScene == "MyMap")
                    SceneManager.LoadScene("NetworkMode");
                else if (nowScene == "TestScene")
                {
                    if (StageManager.instance.m_isStage)
                        SceneManager.LoadScene("StageMode");
                    else
                        SceneManager.LoadScene("MyMap");
                }
                else if (nowScene == "StageEditor")
                    SceneManager.LoadScene("MyMap");
                else
                    Debug.LogError("이전 씬을 불러올 수 없습니다. 씬 이름을 체크하세요.");
                yield break;
            }
            yield return null;
        }
    }


    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
