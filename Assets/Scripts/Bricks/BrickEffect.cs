using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickEffect : MonoBehaviour {
    ParticleSystem[] m_hitParticle = new ParticleSystem[3];

    SpriteRenderer m_render;
    Animator m_animator;
	void Start () {
        for(int i = 0; i < m_hitParticle.Length; ++i)
            m_hitParticle[i] = Instantiate(transform.FindChild("Particle_Hit"), transform).GetComponent<ParticleSystem>();
        m_render = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

    }

    virtual public void Reset()
    {
        foreach (var it in m_hitParticle)
        {
            it.Stop();
            it.gameObject.SetActive(false);
        }
        m_animator.SetTrigger("Reset");
    }

    public void HitEffectOn(Vector2 point) { StartCoroutine("HitEffect",point); }
	IEnumerator HitEffect(Vector2 point)
    {
       // for(; ; )
            foreach(var it in m_hitParticle)
            {
                if(it.gameObject.activeInHierarchy == false)
                {
                    it.gameObject.SetActive(true);
                    it.transform.position = point;
                    yield return new WaitForSeconds(0.5f);
                    it.gameObject.SetActive(false);
                    yield break;
                }
            }
    }

    public void DieEffectOn() {
        m_animator.SetTrigger("DieEffect");
        if(MyCamera.instance != null)
            MyCamera.instance.Shake(0.3f, 1f);
    }
}
