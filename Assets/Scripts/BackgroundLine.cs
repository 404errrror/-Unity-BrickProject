using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLine : MonoBehaviour {
    public GameObject linePrefab;
    [SerializeField] Vector2 space;
    [SerializeField] Vector2 maxSpace;
    [SerializeField] Vector2 offset;

    [HideInInspector][SerializeField]
    List<GameObject> listObject = new List<GameObject>();
	// Use this for initialization

    public void LineGenerate()
    {
        foreach (var it in listObject)
            DestroyImmediate(it);
        listObject.Clear();

        for (float x = space.x; x < maxSpace.x; x += space.x)
        {
            GameObject leftObject = Instantiate(linePrefab,transform.position,Quaternion.identity,transform);
            GameObject rightObject = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);

            leftObject.transform.position -= new Vector3(x,0, 0);
            rightObject.transform.position += new Vector3(x, 0, 0);
            leftObject.transform.position += new Vector3(offset.x, 0, 0);
            rightObject.transform.position += new Vector3(offset.x, 0, 0);

            leftObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(leftObject.transform.position.x, maxSpace.y));
            leftObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(leftObject.transform.position.x, -maxSpace.y));
            rightObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(rightObject.transform.position.x, maxSpace.y));
            rightObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(rightObject.transform.position.x, -maxSpace.y));

            listObject.Add(leftObject);
            listObject.Add(rightObject);
        }

        for (float y = space.y; y < maxSpace.y; y += space.y)
        {
            GameObject leftObject = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);
            GameObject rightObject = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);

            leftObject.transform.position -= new Vector3(0, y, 0);
            rightObject.transform.position += new Vector3(0, y, 0);
            leftObject.transform.position += new Vector3(0, offset.y, 0);
            rightObject.transform.position += new Vector3(0, offset.y, 0);

            leftObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(maxSpace.x, leftObject.transform.position.y));
            leftObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-maxSpace.x, leftObject.transform.position.y));
            rightObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(maxSpace.x, rightObject.transform.position.y));
            rightObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-maxSpace.x, rightObject.transform.position.y));

            listObject.Add(leftObject);
            listObject.Add(rightObject);
        }

        {
            GameObject horizontal = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);
            GameObject vertical = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);

            horizontal.transform.position += new Vector3(offset.x, 0, 0);
            vertical.transform.position += new Vector3(0, offset.y, 0);

            horizontal.GetComponent<LineRenderer>().SetPosition(0, new Vector3(horizontal.transform.position.x, maxSpace.y));
            horizontal.GetComponent<LineRenderer>().SetPosition(1, new Vector3(horizontal.transform.position.x, -maxSpace.y));
            vertical.GetComponent<LineRenderer>().SetPosition(0, new Vector3(maxSpace.x, vertical.transform.position.y));
            vertical.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-maxSpace.x, vertical.transform.position.y));

            listObject.Add(horizontal);
            listObject.Add(vertical);
        }

    }
}
