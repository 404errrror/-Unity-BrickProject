using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorFadeButton : MyButton {

    SetInfoButton m_setInfo;

	// Use this for initialization
	public override void Start () {
        base.Start();
        m_setInfo = transform.parent.GetComponent<SetInfoButton>();
        gameObject.SetActive(false);
	}

    //public override void ButtonClick()
    //{
    //    base.ButtonClick();
    //    m_setInfo.SetInfoHide();
    //}
}
