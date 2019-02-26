using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTriggerOn : MonoBehaviour {
    public Animator targetAnimator;
    public string parameterName;

	// Use this for initialization
	void Start () {
        targetAnimator = GetComponent<Animator>();
	}
	
	public void On()
    {
        targetAnimator.SetTrigger(parameterName);
    }

	public void On(string parameterName_)
	{
		targetAnimator.SetTrigger(parameterName_);
	}

}
