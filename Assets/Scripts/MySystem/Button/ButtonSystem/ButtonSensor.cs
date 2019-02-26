using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSensor : MonoBehaviour {
    MyButton[] myButtonArr;
    MyButton[] searchTempArr;       // 터치 감지된 버튼을 임시로 담는 변수. 속도를 위해 List를 사용하지 않음.
    MyButton downButton;
    MyButton upButton;
    int searchTempIndex;

	void Start () {
        myButtonArr = MyButton.buttonList.ToArray();                // 속도를 위해서 변환.
        searchTempArr = new MyButton[myButtonArr.Length];
        searchTempIndex = 0;
        downButton = null;
        upButton = null;
	}

    void Update()
    {
        ButtonCheck();
    }

    void ButtonCheck()
    {

        
        if(Input.GetMouseButtonDown(0))
        {
            InitializeTempArray();

            // 눌러진 버튼을 모두 임시 배열에 넣습니다.
            foreach (var it in myButtonArr)
            {
                if (it.gameObject.activeInHierarchy && it.enabled && it.IsMouseIn())
                    AddTempArray(it);
            }
            // 임시 배열중에 가장 위에 있는 버튼을 찾습니다.
            if (searchTempIndex > 0)
            {
                downButton = SearchTopButton(searchTempArr);
                downButton.ButtonDown();
            }

        }

        else if(Input.GetMouseButtonUp(0))
        {
            InitializeTempArray();

            foreach(var it in myButtonArr)
                if (it.gameObject.activeInHierarchy && it.enabled && it.IsMouseIn())
                    AddTempArray(it);
            if (searchTempIndex > 0)
            {
                upButton = SearchTopButton(searchTempArr);
                if (upButton == downButton)
                    upButton.ButtonClick();
                upButton.ButtonUp();
            }

            downButton = null;
            upButton = null;
        }
    }


    /// <summary>
    /// 배열 중에서 가장 위에 있는 배열을 검색해서 반환합니다.
    /// </summary>
    /// <param name="buttonArr"> 검사할 버튼 배열</param>
    MyButton SearchTopButton(MyButton[] buttonArr)
    {
        MyButton topButton = searchTempArr[0];

        for (int i = 0; i < searchTempIndex; ++i)
            if (topButton.m_sortingOrder < searchTempArr[i].m_sortingOrder)
                topButton = searchTempArr[i];
        return topButton;
    }
    

    /// <summary>
    /// 가장 위에 있는 버튼을 검색하기 위한 임시변수를 초기화합니다.
    /// </summary>
    void InitializeTempArray()
    {
        searchTempArr.Initialize();
        searchTempIndex = 0;
    }

    void AddTempArray(MyButton button)
    {
        searchTempArr[searchTempIndex] = button;
        ++searchTempIndex;
    }
}
