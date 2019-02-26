using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterButton : MyButton {
    static public CenterButton instance;

    public Sprite m_playButton;
    public Sprite m_pauseButton;

    SpriteRenderer m_render;

    public enum CenterButtonImage{
        Play,Pause
    }

    public override void Start()
    {
        base.Start();
        instance = GetComponent<CenterButton>();
        m_render = GetComponent<SpriteRenderer>();
    }

    public override void ButtonClick()
    {
        base.ButtonClick();
        if (GameManager.gameStart == false)         // GameStart.
        {
            GameStartManager.instance.GameStart();
            m_render.sprite = m_pauseButton;
        }
        else                                        // 시작된 상태에서 버튼을 눌렀을 때.
        {
            if (GameManager.gamePause == false)     // Pause.
            {
                GameStartManager.instance.GamePause();
                m_render.sprite = m_playButton;
            }
            else                                    // Play
            {
                GameStartManager.instance.GamePlay();
                m_render.sprite = m_pauseButton;
            }

        }
    }

    public void ChangeImage(CenterButtonImage imageEnum)
    {
        switch (imageEnum)
        {
            case CenterButtonImage.Play:
                m_render.sprite = m_playButton;
                break;
            case CenterButtonImage.Pause:
                m_render.sprite = m_pauseButton;
                break;
        }
    }
}
