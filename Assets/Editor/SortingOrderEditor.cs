using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SortingOrder))]
public class SortingOrderEditor : Editor
{
    SortingOrder m_target;
    void OnEnable()
    {
        m_target = (SortingOrder)target;
        m_target.Init();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_target.SetSortingOrder();
    }

}
