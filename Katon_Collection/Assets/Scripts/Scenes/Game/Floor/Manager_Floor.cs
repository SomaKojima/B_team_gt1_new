using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Floor : MonoBehaviour
{
    //建材のリスト
    private List<Floor>[] m_floors = new List<Floor>[(int)Type.Max];
    List<Floor> all = new List<Floor>();

    private void Start()
    {
        for (int i = 0; i < (int)Type.Max; i++)
        {
            m_floors[i] = new List<Floor>();
        }
    }

    private void Update()
    {
        all.Clear();
        for (int i = 0; i < (int)Type.Max; i++)
        {
            all.AddRange(m_floors[i]);
        }
    }

    //リストへの追加
    public void Add(Type type, Floor _floor)
    {
        m_floors[(int)type].Add(_floor);
    }

    //建材の取得
    public List<Floor> Floor
    {
        get { return all; }
    }

    public List<Floor> GetFloorsOf(Type type)
    {
        return m_floors[(int)type];
    }

    public Floor GetTopFloorOf(Type type)
    {
        List<Floor> buf = GetFloorsOf(type);
        if (buf.Count <= 0) return null;
        Debug.Log(GetFloorsOf(type)[buf.Count - 1]);
        return buf[buf.Count - 1];
    }
}
