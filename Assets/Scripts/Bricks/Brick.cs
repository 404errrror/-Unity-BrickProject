using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BrickEffect))]
public class Brick : MonoBehaviour {
    static public List<Brick> activeList;
    static public int activeNum;
    public int m_maxHp = 5;
    public bool m_isDie = false;

    protected int m_hp;
    protected Collider2D m_coll;
    protected BrickEffect m_effect;
    // Use this for initialization
	public virtual void Start () {
        if (activeList == null)
            activeList = new List<Brick>();

        activeList.Add(this);
        m_hp = m_maxHp;

        m_coll = GetComponent<Collider2D>();
        m_effect = GetComponent<BrickEffect>();
    }

    public virtual void GameStart()
    {
        m_coll.isTrigger = false;
        m_isDie = false;
        ++activeNum;
    }

    public virtual void Restart()
    {
        m_coll.isTrigger = true;
        m_isDie = false;
        m_hp = m_maxHp;
        m_effect.Reset();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 블럭의 hp가 감소합니다.
    /// </summary>
    /// <param name="attack"> 총알의 공격력 </param>
    /// <param name="collPoint"> 충돌된 위치 </param>
    public virtual void Hitted(int attack, Vector2 collPoint)
    {
        m_hp -= attack;
        m_effect.HitEffectOn(collPoint);
        if (m_hp <= 0)
        {
            m_effect.DieEffectOn();
            m_isDie = true;
            m_coll.isTrigger = true;
            --activeNum;
            if (activeNum <= 0 && GameManager.gameOver == false)
                GameStartManager.instance.GameClear();
        }
    }
    static public void Quit()
    {
        activeList.Clear();
    }

}
