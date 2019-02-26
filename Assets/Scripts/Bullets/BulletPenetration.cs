using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPenetration : Bullet
{

    List<Collider2D> ignoreList;

    public override void Start()
    {
        base.Start();
        ignoreList = new List<Collider2D>();
    }

    public override void Restart()
    {
        base.Restart();
        foreach (var it in ignoreList)
            Physics2D.IgnoreCollision(it, m_coll, false);
        ignoreList.Clear();
    }

    override public void OnCollisionEnter2D(Collision2D coll)
    {
        if (GameManager.gameStart)
        {
            if (coll.collider.CompareTag("Brick"))
            {
                coll.transform.GetComponent<Brick>().Hitted(m_attack, coll.contacts[0].point);
                Physics2D.IgnoreCollision(coll.collider, m_coll, true);
                ignoreList.Add(coll.collider);

                m_rigid.velocity = m_moveDir * m_speed;
            }
            else if(coll.collider.CompareTag("OutCollider") || coll.collider.CompareTag("Brick_Invincibility"))
            {
                Break();
            }
        }
    }
}
