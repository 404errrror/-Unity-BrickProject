using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailColor : MonoBehaviour {
    public Color startColor = Color.white;
    public Color endColor = Color.white;
    TrailRenderer trail;
	// Use this for initialization
	void Start () {
        trail = GetComponent<TrailRenderer>();
	}
	
	void Update()
    {
        trail.startColor = startColor;
        trail.endColor = endColor;
    }
}
