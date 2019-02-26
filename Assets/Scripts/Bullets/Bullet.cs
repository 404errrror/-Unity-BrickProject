using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour {
    public static List<Bullet> m_activeList;
    public float    m_speed     = 10;
    public int      m_cost      = 5;
    public bool m_isMain = false;
    public bool m_isInit = false;
    [TextArea]
    public string info = "주의 : 충돌을 체크할 콜라이더가 버튼을 체크할 콜라이더(Circle Collider) 보다 위에 있어야합니다!" +
        "CircleCollider는 한개만 존재해야합니다. 따라서 충돌을 체크할 콜라이더가 Circle Collider이라면 충돌과 버튼 콜라이더를 공유해야합니다.";
    static float lineY;

    protected int m_attack  = 1;

    protected Rigidbody2D m_rigid;
    protected Collider2D m_coll;
    protected SpriteRenderer m_render;
    protected BulletEffect m_effect;
    protected Vector2 m_moveDir = Vector2.up;

    LineRenderer m_arrowRender_;
    CircleCollider2D    m_buttonColl;
    Transform           m_parent;
    Vector2             m_hitPoint;
    Vector2             m_moveDirPause;
    Vector2             m_pivot;
    bool                m_isDown;                       // 마우스를 꾸우우우욱 누르고 있으면 계속 true.
    bool                m_isUp;                         // 마우스를 뗐을 때 한번 true.
    bool                m_isClick;                      // 마우스를 클릭했을 때 한번 true.
    bool                m_editShoot;                    // 총알의 위치를 지정했다면 true;
    bool                m_isSetup;                      // 총알이 설치 되었다면 true
    bool                m_isRemove;                       // 삭제할 예정이라면 true;

    [HideInInspector]public bool m_isLineup;            // true라면 총알들이 줄을 섭니다(?)
    [HideInInspector]public bool m_isEditting;          // true라면 화살표를 계산합니다.
    public virtual void Start () {
        if (m_activeList == null)
            m_activeList = new List<Bullet>();

        if (m_isInit)
            return;
        m_isInit = true;

        if (lineY == 0)
            lineY = GameObject.Find("BulletLine").transform.position.y;

        m_rigid         = GetComponent<Rigidbody2D>();
        m_coll          = GetComponent<Collider2D>();
        m_buttonColl    = GetComponent<CircleCollider2D>();
        m_render        = GetComponent<SpriteRenderer>();
        m_effect        = GetComponent<BulletEffect>();
        m_arrowRender_  = transform.FindChild("Arrow").GetComponent<LineRenderer>();
        m_parent = transform.parent;
        m_pivot = new Vector2(0,lineY);
        m_isDown = false;
        m_isUp = false;
        m_isClick = false;
        m_editShoot = false;
        m_isSetup = false;
        m_isRemove = false;

        EditBulletDir(m_moveDir);
        m_isLineup = false;
        m_isEditting = false;

        if (m_isMain)
            m_activeList.Add(this);
        //gameObject.SetActive(false);
	}

	public virtual void Update () {
        if (GameManager.gameStart == false)         // 게임이 아직 시작 안했다면.
        {
            if (m_isLineup)
            {
                if (Vector2.Distance(transform.position, m_pivot) > 0.001f)
                {
                    transform.position += new Vector3(m_pivot.x - transform.position.x, (m_pivot.y - transform.position.y), 0) * Time.deltaTime * m_speed;
                }
                else
                {
                    m_isLineup = false;
                    transform.position = new Vector2(m_pivot.x, m_pivot.y);
                }
                MaintainPoint();
            }
            else if (m_isEditting)
            {
                MaintainPoint();
                m_pivot.x = transform.position.x;
            }
        }
	}

    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (GameManager.gameStart && (
            coll.collider.CompareTag("Brick") || 
            coll.collider.CompareTag("Brick_Invincibility")
            ))
            coll.transform.GetComponent<Brick>().Hitted(m_attack,coll.contacts[0].point);
    }

    /// <summary>
    /// 설치에 성공하면 true를 반환하고 골드가 부족하다면 false를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public bool Setup()
    {
        if (Gold.instance.gold - m_cost < 0)
        {
            Return();
            return false;
        }
        else
        {
            Gold.instance.gold -= m_cost;
            m_activeList.Add(this);
            m_isSetup = true;
            m_isLineup = true;
            return true;
        }
    }

    public void Remove()
    {
        m_activeList.Remove(this);

        m_render.color      = Color.white;
        m_arrowRender_.startColor = new Color(1,1,1,0.5f);
        m_arrowRender_.endColor = new Color(1,1,1,0);
        m_moveDir           = Vector2.up;
        m_isSetup           = false;
        m_isLineup          = false;
        m_isRemove          = false;
        m_editShoot         = false;
        Gold.instance.gold += m_cost;
        Return();
    }

    public void Return()
    {
        transform.position = m_parent.position;
        transform.SetParent(m_parent);
        gameObject.SetActive(false);

    }

    public void Shoot()
    {
        transform.position = m_pivot;
       // m_pivot = new Vector2(m_pivot.x, lineY);
        m_rigid.velocity = m_moveDir * m_speed;
    }

    public void Pause()
    {
        m_moveDirPause = m_rigid.velocity.normalized;
        m_rigid.velocity = Vector2.zero;
    }

    public void Play()
    {
        m_rigid.velocity = m_moveDirPause * m_speed;
    }

    virtual public void Restart()
    {
        m_rigid.velocity    = Vector2.zero;
        arrow               = true;
        m_isLineup           = true;
        m_buttonColl.enabled = true;
        m_effect.Reset();
    }

    public void Break()
    {
        m_effect.Break();
        m_buttonColl.enabled = false;
        Pause();
    }


    /// <summary>
    /// 총알의 이동방향을 수정합니다.
    /// </summary>
    /// <param name="direction">정규화된 이동방향</param>
    public void EditBulletDir(Vector2 direction)
    {
        if (m_editShoot == false && m_moveDir != direction)
            m_editShoot = true;

        // 총알 이동방향 설정.
        m_moveDir = direction;
        LayerMask layerMask = ~(1 << 13 | 1 << 2);          // Bullet레이어와 IgnoreRaycast레이어를 Raycast에서 제외.

        // 화살표 계산.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100 , layerMask);
        if (hit.collider == null)
            Debug.LogError("Bullet.MaintainPoint Error! Raycast로 충돌된 오브젝트가 없습니다.");

        float radiusAngle = Mathf.Atan2(direction.y, direction.x);

        m_hitPoint = hit.point;
        m_arrowRender_.SetPosition(0, m_arrowRender_.transform.position);
        m_arrowRender_.SetPosition(1, m_hitPoint);

        transform.eulerAngles = new Vector3(0, 0, (radiusAngle * 180) / Mathf.PI - 90); // -> 삼각형같은 오브젝트 때문에 화살표가 아닌 총알을 돌림.
    }

    /// <summary>
    /// 총알을 이동해도 쏘는 점은 유지합니다.
    /// </summary>
    public void MaintainPoint()
    {
        if (m_editShoot == false)
        { 
            EditBulletDir(m_moveDir);
            return;
        }
        Vector2 direction = (Vector2)m_hitPoint - (Vector2)transform.position;
        direction.Normalize();

        LayerMask layerMask = ~(1 << 13 | 1 << 2);          // Bullet레이어와 IgnoreRaycast레이어를 Raycast에서 제외.

   
        // 총알 이동방향 설정.
        m_moveDir = direction;

        // 화살표 계산.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,100 , layerMask);
        if (hit.collider == null)
            Debug.LogError("Bullet.MaintainPoint Error! Raycast로 충돌된 오브젝트가 없습니다.");

        float radiusAngle = Mathf.Atan2(direction.y, direction.x);

        m_arrowRender_.SetPosition(0, m_arrowRender_.transform.position);
        m_arrowRender_.SetPosition(1, m_hitPoint);
        transform.eulerAngles = new Vector3(0, 0, (radiusAngle * 180) / Mathf.PI - 90); // -> 삼각형같은 오브젝트 때문에 화살표가 아닌 총알을 돌림.
    }

    /// <summary>
    /// 총알의 위치를 바꿉니다.
    /// </summary>
    /// <param name="position"></param>
    public void MoveX(float positionX)
    {
        if (BackgroundInfo.instance.IsInX(positionX))
        {
            transform.position = new Vector2(positionX, m_pivot.y);
            m_pivot.x = transform.position.x;
        }
    }

    public void Move(Vector2 position)
    {
        if (BackgroundInfo.instance.IsIn(position))
        {
            transform.position = position;
            m_pivot.x = transform.position.x;
            if (m_isRemove)                                                                             // Remove Cancel
            {
                m_isRemove = false;
                m_render.color = Color.white;
                m_arrowRender_.startColor = new Color(1, 1, 1, 0.5f);
                m_arrowRender_.endColor = new Color(1, 1, 1, 0);
            }
        }
        else                                                                                            // Remove Ready
        {
            m_isRemove = true;
            m_arrowRender_.startColor = new Color(1, 0.5f, 0.5f,0.5f);
            m_arrowRender_.endColor = new Color(1, 0.5f, 0.5f,0);
            m_render.color = new Color(1, 0.5f, 0.5f);
        }
    }


    /// <summary>
    /// 마우스가 총알 안에있다면 true를 반환합니다.
    /// </summary>
    public bool IsMouseIn()
    {
        if (gameObject.activeInHierarchy == false || m_buttonColl.enabled == false)
            return false;
#if UNITY_EDITOR
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) <= m_buttonColl.radius)
#elif UNITY_ANDROID || UNITY_IPHONE
        if (Vector2.Distance(
        transform.position, 
        Camera.main.ScreenToWorldPoint(Input.GetTouch(Input.touchCount - 1).position)
        ) <= m_buttonColl.radius)
#else
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) <= m_buttonColl.radius)
#endif
            return true;
        else
            return false;
    }

    /// <summary>
    /// 총알을 클릭했다면 true를 반환합니다.
    /// </summary>
    public bool IsBulletClick()
    {
        return m_isClick;
    }

    /// <summary>
    /// 마우스가 총알을 Down했는지 Up했는지 클릭했는지 검사합니다.
    /// </summary>
    public void BulletInputCheck()
    {
        if (Input.GetMouseButtonDown(0))
            m_isDown = IsMouseIn();

        else if (Input.GetMouseButtonUp(0))
        {
            m_isUp = IsMouseIn();

            if (m_isDown && m_isUp)
                m_isClick = true;
            m_isDown = false;
        }
        else
        {
            m_isUp = false;
            m_isClick = false;
        }
    }

    ///////////////////////////////////////////
    //                Get Set
    ///////////////////////////////////////////

    /// <summary>
    /// 총알에서 가리키는 화살표의 활성화 여부.
    /// </summary>
    public bool arrow
    {
        get { return    m_arrowRender_.gameObject.activeInHierarchy; }
        set {           m_arrowRender_.gameObject.SetActive(value);  }
    }

    public bool isRemove
    {
        get { return m_isRemove; }
    }

    public int cost
    {
        get { return m_cost; }
    }

    public Vector2 moveDir
    {
        get { return m_moveDir; }
        set { m_moveDir = value; }
    }

    static public void Quit()
    {
        m_activeList.Clear();
    }
}
