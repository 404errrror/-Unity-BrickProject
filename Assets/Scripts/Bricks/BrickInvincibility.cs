using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickInvincibility : Brick {

    public override void GameStart()
    {
        base.GameStart();
        --activeNum;
    }

    public override void Hitted(int attack, Vector2 collPoint)
    {
        m_effect.HitEffectOn(collPoint);
    }
}
