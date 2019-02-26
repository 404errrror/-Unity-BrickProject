using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBullet : MyButton {
    public GameObject   bulletObj;
    public int          maxAmount;

    Bullet[]        bulletPool;
    Collider2D[]    bulletCollArr;
    Bullet          dragObject;
    GameObject      visualObject;
    Transform       StageObject;
    MyButton        board;
    bool            returnBullet;
    public override void Start()
    {
        base.Start();

        visualObject        = new GameObject();
        StageObject         = GameObject.Find("Stage").transform;
        board               = GameObject.FindObjectOfType<SetBulletButton>() as MyButton;
        returnBullet        = false;
 
        InitVisualObject();
    }

    public override void Awake()
    {
        base.Awake();
        bulletPool      = new Bullet[maxAmount];
        bulletCollArr   = new Collider2D[maxAmount];
        for (int i = 0; i < bulletPool.Length; ++i)
        {
            bulletPool[i] = Instantiate(bulletObj, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bulletPool[i].transform.SetParent(transform);
            bulletCollArr[i] = bulletPool[i].GetComponent<Collider2D>();
            bulletPool[i].gameObject.SetActive(true);
        }
    }


    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0))
        {
            if (dragObject != null)
            {
                Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (BackgroundInfo.instance.IsIn(mousePos))
                {
                    dragObject.transform.position = mousePos;
                    visualObject.transform.position = dragObject.transform.position;
                }
            }
        }

        else if(Input.GetMouseButtonUp(0))       // 버튼내에서 Up했는지, 그 밖에서 Up했는지 체크하기 위해 모든 Up 이벤트를 가져옴.
        {
           // if (dragObject != null)
             ButtonUp();
        }

        // 비주얼 오브젝트를 다시 되돌려보냅니다.
        if (returnBullet)
            visualObject.transform.position += (transform.position - visualObject.transform.position) * Time.deltaTime * 10;
    }

    public override void ButtonDown()
    {
        base.ButtonDown();

        dragObject = SearchInactiveObject();
        if (dragObject == null)
            return;
                                                                    // 이하 총알을 클릭 했을 때.
        if (visualObject.activeInHierarchy == false)  
            visualObject.SetActive(true);
        returnBullet = false;
        dragObject.gameObject.SetActive(true);
        dragObject.transform.SetParent(StageObject);
        dragObject.m_isEditting = true;
        Gold.instance.ShowCost(dragObject.cost);

    }

    public override void ButtonUp()
    {
        base.ButtonUp();
        if (board.IsMouseIn())
        {
            if (dragObject != null)     
            {
                returnBullet = true;
                dragObject.Return();
                dragObject = null;
                Gold.instance.RefreshGold();            // 골드가 "10 - 1" 형태로 되어있기 때문에 다시 바꿔줌.
            }
        }

        else if(dragObject != null)     // 제대로 설치 했을 때.
        {
            if (dragObject.Setup())
            {
                //IgnoreBulletCollision(dragObject.GetComponent<Collider2D>());
                dragObject = null;
                visualObject.transform.position = transform.position;
            }
            else                                        // 골드가 부족할 때.
            {
                returnBullet = true;
                Gold.instance.RefreshGold();
            }
        }

        else if (dragObject == null)    // 드래그 오브젝트도 없고, 보드가 아닌 곳에서 마우스를 업 -> fade를 클릭한 것.
        {
            visualObject.SetActive(false);
        }
    }

    /// <summary>
    /// 비활성화된 총알을 찾고 반환합니다.
    /// </summary>
    Bullet SearchInactiveObject()
    {
        for (int i = 0; i < bulletPool.Length; ++i)
            if (bulletPool[i].gameObject.activeInHierarchy == false)
                return bulletPool[i];
        return null;
    }

    /// <summary>
    ///보드 화면에 보여주기용 오브젝트를 초기화합니다.
    /// </summary>
    void InitVisualObject()
    {
        SpriteRenderer tempRender = visualObject.AddComponent<SpriteRenderer>();
        SpriteRenderer myTempRender = transform.GetComponent<SpriteRenderer>();

        tempRender.sprite = myTempRender.sprite;
        tempRender.sortingOrder = myTempRender.sortingOrder;
        tempRender.sharedMaterial = myTempRender.sharedMaterial;

        visualObject.transform.position = transform.position;
        visualObject.transform.SetParent(transform.parent);
        visualObject.transform.localScale = Vector2.one;

        visualObject.name = "VisualObject";
        visualObject.SetActive(false);
    }


    void IgnoreBulletCollision(Collider2D coll)
    {
        foreach (var it in bulletCollArr)
            if(it.gameObject.activeInHierarchy)
                Physics2D.IgnoreCollision(coll, it);
    }
}
