using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder : MonoBehaviour {

    Renderer m_render;
    public int m_sortingOrder;

    public Renderer Render { get { return m_render; } }
    public void Init()
    {
        m_render = transform.GetComponent<Renderer>();
        m_sortingOrder = m_render.sortingOrder;
    }

    public void SetSortingOrder()
    {
        m_render.sortingOrder = m_sortingOrder;
    }
}
