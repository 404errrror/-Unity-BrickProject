using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLockButton : MyButton {

    public override void ButtonClick()
    {
        base.ButtonClick();
        MyButtonAnimation.instance.ActiveAnimation();
        MyButtonAnimation.instance.SetBool("isLockBtn", true);
    }

    void InactiveBoard()
    {
        MyButtonAnimation.instance.SetBool("isLockBtn", false);
        MyButtonAnimation.instance.InactiveAnimation();
    }


    
}
