using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour {
    Animator animator;
    ParticleSystem particle;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        particle = transform.FindChild("BreakEffect").GetComponent<ParticleSystem>();
	}
	
	public void Break()
    {
        animator.SetTrigger("Break");
        particle.gameObject.SetActive(true);
    }

    public void Reset()
    {
        animator.ResetTrigger("Break");
        animator.SetTrigger("Reset");
        particle.Stop();
        particle.gameObject.SetActive(false);
    }
}
