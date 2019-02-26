using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour {
    static public GameStartManager instance;
    public bool m_isMain = false;

    Animator m_setBulletAni;
    TextMesh m_setBulletText;
    GameObject m_fadeObj;

	// Use this for initialization
	void Start () {
        instance = GetComponent<GameStartManager>();
        m_setBulletAni = GameObject.Find("SetBullet").GetComponent<Animator>();
        m_setBulletText = GameObject.Find("SetBullet").transform.Find("Text").GetComponent<TextMesh>();
        if(m_isMain == false)
            m_fadeObj = m_setBulletAni.transform.FindChild("Fade").gameObject;
	}
	
	public void GameStart()
    {
        if (GameManager.gameStart)              // 중복 스타트 방지.
            return;

        GameManager.gameStart = true;
        Brick.activeNum = 0;

        // 총알 쏘기
        foreach (var it in Bullet.m_activeList)                            
        {
            it.arrow = false;
            it.Shoot();
        }
        if(Brick.activeList != null)
            foreach (var it in Brick.activeList)                // 총알이 통과 못하도록.
                it.GameStart();

        // SetBullet 창 없애기
        m_setBulletAni.enabled = true;
        m_setBulletAni.SetBool("GameStart",true);
        if(CountDown.instance != null)
        CountDown.instance.animator.SetBool("GameStart", true);
    }

    public void GamePause()
    {
        GameManager.gamePause = true;

        foreach (var it in Bullet.m_activeList)            // 총알 일시정지.
            it.Pause();

        m_fadeObj.SetActive(true);
        m_setBulletAni.SetBool("Pause",true);
        m_setBulletText.text = "Restart";
    }

    public void GamePlay()
    {
        GameManager.gamePause = false;

        foreach (var it in Bullet.m_activeList)            // 총알 다시 쏘기.
            it.Play();


        m_setBulletAni.SetBool("Pause", false);
        StartCoroutine(InactiveFade("BulletSetButton_GameStart"));

    }

    public void GameRestart()
    {
        GameManager.gameStart = false;
        GameManager.gamePause = false;
        GameManager.gameOver = false;
        CountDown.instance.seconds = 10;

        foreach (var it in Bullet.m_activeList)
            it.Restart();
        if(Brick.activeList != null)
            foreach (var it in Brick.activeList)
                it.Restart();

        m_setBulletAni.SetBool("Pause", false);
        m_setBulletAni.SetBool("GameStart", false);
        CountDown.instance.animator.SetBool("GameStart", false);
        StartCoroutine("RestartText");
    }

    IEnumerator RestartText()
    {
        for(;;)
        {
            if (m_setBulletAni.GetCurrentAnimatorStateInfo(0).IsName("BulletSetButton_Restart"))
            {
                m_setBulletText.text = "Set Bullet";
                m_fadeObj.SetActive(false);
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator InactiveFade(string nextStateName)
    {
        for(;;)
        {
            if (m_setBulletAni.GetCurrentAnimatorStateInfo(0).IsName(nextStateName))
            {
                m_fadeObj.SetActive(false);
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void TimeOver()
    {
        GameManager.gameOver = true;
        m_setBulletText.text = "TimeOver";

        m_fadeObj.SetActive(true);
        m_setBulletAni.SetBool("TimeOver", true);
        CountDown.instance.animator.SetBool("GameStart", false);
        foreach (var it in Bullet.m_activeList)
            it.Break();
    }

    public void TimeOverRestart()
    {
        GameManager.gameStart = false;
        GameManager.gamePause = false;
        GameManager.gameOver = false;
        CountDown.instance.seconds = 10;
        CenterButton.instance.ChangeImage(CenterButton.CenterButtonImage.Play);

        foreach (var it in Bullet.m_activeList)
            it.Restart();

        if (Brick.activeList != null)
            foreach (var it in Brick.activeList)
                it.Restart();

        m_setBulletAni.SetBool("Pause", false);
        m_setBulletAni.SetBool("TimeOver", false);
        m_setBulletAni.SetBool("GameStart", false);
        CountDown.instance.animator.SetBool("GameStart", false);
        StartCoroutine("RestartText");
    }

    void FadeButton()
    {
        if (GameManager.gamePause)
            CenterButton.instance.ButtonClick();
        else if (GameManager.gameOver)
            TimeOverRestart();
    }

    public void GameClear()
    {
        if (m_isMain == false)
        {
            GameManager.gameClear = true;
            m_setBulletAni.SetBool("GameClear", true);
            m_fadeObj.SetActive(true);
            m_setBulletText.text = "Clear";
        }
    }
}
