using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    static public MyCamera instance;

    float m_length, m_strenth;
	void Start () {
        instance = GetComponent<MyCamera>();
        m_length = m_strenth = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Shake();

    }

    public void Shake(float length, float strenth)
    {
        m_length = length;
        m_strenth = strenth;
    }
    void Shake()
    {
        if(m_length > 0)
        {
            m_length -= Time.deltaTime;
            if (m_length <= 0)
                transform.position = new Vector3(0, 0, -10);
            else
            {
                Vector2 shakeDir = Random.insideUnitCircle * m_strenth * Time.deltaTime;
                transform.position += new Vector3(shakeDir.x, shakeDir.y);
            }
        }
    }
}
