using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBrickButton : MyButton {


    public override void Start()
    {
        base.Start();
    }

    public override void ButtonClick()
    {
        base.ButtonClick();
        MyButtonAnimation.instance.ActiveAnimation();
        MyButtonAnimation.instance.SetBool("isBrickBtn", true);
    }

    void InactiveBoard()
    {
        MyButtonAnimation.instance.SetBool("isBrickBtn", false);
        MyButtonAnimation.instance.InactiveAnimation();
    }


}
