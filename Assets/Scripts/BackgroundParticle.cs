using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticle : MonoBehaviour {
    const float X_Range = 5;
    const float Y_Range = 8;

    static float objectsAlpha;

    public Sprite[] sprite;
    public int amount;

    GameObject[] particleObject;
    Vector2[] velocity;
    SpriteRenderer[] renderer;
    float[] speed;
    float[] colorEffect;
	// Use this for initialization
	void Start () {
        particleObject = new GameObject[amount];
        velocity = new Vector2[amount];
        renderer = new SpriteRenderer[amount];
        speed = new float[amount];
        colorEffect = new float[amount];
        objectsAlpha = 0;
        for (int i = 0; i < velocity.Length; ++i)
        {
            velocity[i] = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            speed[i] = Random.Range(1.0f, 2.0f);

            particleObject[i] = new GameObject();
            particleObject[i].transform.parent = transform;
            particleObject[i].transform.position = new Vector2(Random.Range(-X_Range, X_Range), Random.Range(-Y_Range,Y_Range));

            renderer[i] = particleObject[i].AddComponent<SpriteRenderer>();
            renderer[i].sprite = sprite[Random.Range(0, sprite.Length)];
            renderer[i].color = new Color(1, 1, 1, Random.Range(0.01f, 0.25f) * objectsAlpha);


            colorEffect[i] = Random.Range(0.5f,1.0f);
            if (Random.Range(0, 2) == 0)
                colorEffect[i] *= -1;
        }

        StartCoroutine("StartEffect");
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < amount; ++i)
        {
            if(
                particleObject[i].transform.position.x < -X_Range || 
                particleObject[i].transform.position.x > X_Range)
                particleObject[i].transform.position = new Vector3(
                    particleObject[i].transform.position.x * -1,
                    particleObject[i].transform.position.y,
                    particleObject[i].transform.position.x
                    );

            if(
                particleObject[i].transform.position.y < -Y_Range ||
                particleObject[i].transform.position.y > Y_Range
                )
                particleObject[i].transform.position = new Vector3(
                    particleObject[i].transform.position.x,
                    particleObject[i].transform.position.y * -1, 
                    particleObject[i].transform.position.x
                    );
            if (renderer[i].color.a < 0 )
            {
                renderer[i].color = new Color(renderer[i].color.r, renderer[i].color.g, renderer[i].color.b, 0);
                colorEffect[i] *= -1;
            }
            else if (renderer[i].color.a > 0.25f)
            {
                renderer[i].color = new Color(renderer[i].color.r, renderer[i].color.g, renderer[i].color.b, 0.25f);
                colorEffect[i] *= -1;
            }

            particleObject[i].transform.position += (Vector3)(velocity[i] * speed[i] * Time.deltaTime * 0.5f);
            renderer[i].color += new Color(0, 0, 0, colorEffect[i] * objectsAlpha * Time.deltaTime * 0.3f);

        }
	}

    IEnumerator StartEffect()
    {
        for (;;)
        {
            if (objectsAlpha >= 1)
                yield break;
            else
            {
                objectsAlpha += Time.deltaTime;
                yield return null;
            }
        }
    }
}
