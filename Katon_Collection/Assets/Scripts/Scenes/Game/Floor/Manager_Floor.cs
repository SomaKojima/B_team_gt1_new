using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Floor : MonoBehaviour
{
    //建材のリスト
    [SerializeField]
    private List<Floor> m_floors;

    //リストへの追加
    public void Add(Floor _floor)
    {
        m_floors.Add(_floor);

    }

    //建材の取得
    public List<Floor> Floor
    {
        get { return m_floors; }
    }
}
