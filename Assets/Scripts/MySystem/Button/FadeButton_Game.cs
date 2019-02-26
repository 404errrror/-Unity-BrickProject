using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeButton_Game : MyButton {
    GameObject m_board;
    GameObject m_gameStartManager;

    public override void Start()
    {
        base.Start();
        m_board = GameObject.Find("SetBullet").transform.FindChild("Board").gameObject;
        m_gameStartManager = GameObject.Find("GameStart");
    }

    public override void ButtonClick()
    {
        base.ButtonClick();
        if (GameManager.gameStart == false)
            m_board.SendMessage("InactiveSetBullet");
        else
            m_gameStartManager.SendMessage("FadeButton", SendMessageOptions.DontRequireReceiver);

    }
}
