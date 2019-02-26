using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInfo : MonoBehaviour {
    static public BackgroundInfo instance;

    float minX, maxX, minY, maxY;
	// Use this for initialization
	void Start () {
        instance = GetComponent<BackgroundInfo>();
        SetInfo();
	}

    // Background의 정보를 갱신합니다.
    void SetInfo()
    {
        Vector2 scale = transform.lossyScale;
        Vector2 position = transform.position;

        minX = position.x - scale.x * 0.5f;
        maxX = position.x + scale.x * 0.5f;
        minY = position.y - scale.y * 0.5f;
        maxY = position.y + scale.y * 0.5f;
    }

    /// <summary>
    /// 입력된 위치가 현재 오브젝트의 X좌표 범위 안에 있다면 true를 반환합니다.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public bool IsInX(float x)
    {
        if (x >= minX && x <= maxX)
            return true;
        else
            return false;
    }

	/// <summary>
	/// 입력된 위치가 현재 오브젝트의 안에 있다면 true를 반환합니다.
	/// </summary>
    public bool IsIn(Vector2 position)
    {
        if (
            position.x >= minX && position.x <= maxX &&
            position.y >= minY && position.y <= maxY
            )
            return true;
        else
            return false;
    }
}
