using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBulletButton : MyButton {
    static public SetBulletButton instance;

    Animator m_animator;
    GameObject m_fadeObject;
    GameObject m_selectBullet;              // 총알이랑 충돌하는 것을 막기위해 Active false 해야함.
    public override void Start()
    {
        base.Start();
        instance            = GetComponent<SetBulletButton>();
        m_animator          = transform.parent.GetComponent<Animator>();
        m_fadeObject        = transform.parent.Find("Fade").gameObject;
        m_selectBullet      = transform.parent.Find("Select Bullet").gameObject;
        m_fadeObject.SetActive(false);
    }

    public override void ButtonClick()
    {
        base.ButtonClick();
        m_animator.enabled = true;

        if (GameManager.gameClear)              // 게임을 클리어 했을 때
        {
            GameManager.gameStart = false;
            GameManager.gameClear = false;
            GameManager.gameOver = false;
            GameManager.gamePause = false;
            Brick.Quit();
            MyButton.Quit();
            Bullet.Quit();
            if(StageManager.instance.m_isStage)
                SceneLoader.instance.SceneLoad("StageMode");
            else
                SceneLoader.instance.SceneLoad("MyMap");

        }

        if (GameManager.gameStart)         // 게임이 진행중일 때.
        {
            if(GameManager.gameOver  == true)             // 게임오버 후에 Restart.
            {
                GameStartManager.instance.TimeOverRestart();
                return;

            }
            else if (GameManager.gamePause)      // 게임이 일시정지 상태일 때. -> Restart버튼 누른거.
            {
                GameStartManager.instance.GameRestart();
                return;
            }
        }
        else                                // 게임이 진행중이 아닐 때.
        {
            m_animator.SetBool("SetBullet", true);
            m_fadeObject.SetActive(true);
            m_selectBullet.SetActive(true);
        }

 
    }
    void InactiveSetBullet()
    {
        if (m_animator.GetBool("SetBullet") == true)
        {
            m_animator.SetBool("SetBullet", false);
            StopCoroutine("InactiveFadeObject");
            StartCoroutine("InactiveFadeObject");
        }
    }

    IEnumerator InactiveFadeObject()
    {
        for(;;)
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("AnimationBoolCheck"))
            {
                m_fadeObject.SetActive(false);
                m_selectBullet.SetActive(false);
                m_animator.enabled = false;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
