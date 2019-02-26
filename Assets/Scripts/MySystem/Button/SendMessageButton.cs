using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageButton : MyButton {
    public GameObject[] target;
    public string message;
	// Use this for initialization

    public override void ButtonClick()
    {
        base.ButtonClick();

        foreach(var it in target)
        it.SendMessage(message,SendMessageOptions.DontRequireReceiver);
    }
}
