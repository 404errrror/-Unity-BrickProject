using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfo{

	static public Vector2 inputPosition
    {
        get
        {
#if UNITY_EDITOR
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IPHONE
            return Camera.main.ScreenToWorldPoint(Input.GetTouch(Input.touchCount - 1).position);
#else
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif  
        }
    }

    /// <summary>
    /// 블럭에 맞게 계산된 위치를 반환합니다.
    /// </summary>
    /// <param name="scale"> 블럭의 크기 배율</param>
    /// <returns></returns>
    static public Vector2 TilePosition(float scale)
    {
        Vector2 origin = inputPosition + new Vector2(0.5f,0.5f);
        Vector2 tempInt = new Vector2((int)origin.x,(int)origin.y);
        Vector2 tempFloat = origin - tempInt;
        Vector2 result = new Vector2();

        if (tempFloat.x > 0.5f)
            tempInt.x += 1;
        else if (tempFloat.x < -0.5f)
            tempInt.x -= 1;
        if (tempFloat.y > 0.5f)
            tempInt.y += 1;
        else if (tempFloat.y < -0.5f)
            tempInt.y -= 1;

        result = tempInt - new Vector2(0.5f, 0.5f);


        //result = new Vector2((int)result.x, (int)result.y);         // 소숫점 빼기.
        //result -= temp;
        return result;
    }
}
