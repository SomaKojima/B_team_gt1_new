using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Floor : MonoBehaviour
{
    //建材のリスト
    private List<Floor> m_floors = new List<Floor>();

    private void Start()
    {
    }

    private void Update()
    {
    }

    //リストへの追加
    public void Add(Floor _floor)
    {
        m_floors.Add(_floor);
    }

    //建材の取得
    public List<Floor> Floors
    {
        get { return m_floors; }
    }
    
    public Floor GetTopFloorOf()
    {
        if (m_floors.Count <= 0) return null;
        return m_floors[m_floors.Count - 1];
    }
}
