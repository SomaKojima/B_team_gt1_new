﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Floor
{
    //建材のリスト
    private List<Floor> m_floors = new List<Floor>();

    Floor topLnading = null;
    int landingCount = 0;

    public void Initialize()
    {
    }

    public void Update()
    {
        landingCount = 0;
        foreach (Floor floor in m_floors)
        {
            if (floor.IsLanding())
            {
                topLnading = floor;
                landingCount++;
            }
        }
        Debug.Log(m_floors.Count + " : " + landingCount);
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
    
    public Floor GetTopFloor()
    {
        if (m_floors.Count <= 0) return null;
        return m_floors[m_floors.Count - 1];
    }

    public Floor GetTopLandingFloor()
    {
        return topLnading;
    }

    public int GetLandingCount()
    {
        return landingCount;
    }
}
