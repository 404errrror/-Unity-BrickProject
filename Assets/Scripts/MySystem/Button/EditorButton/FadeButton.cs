using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeButton : MyButton {
    public override void ButtonUp()
    {
        base.ButtonUp();
        MyButtonAnimation.instance.SetBool("isBrickBtn", false);
    }
	
}
