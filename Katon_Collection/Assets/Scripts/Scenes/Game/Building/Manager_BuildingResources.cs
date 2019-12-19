using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_BuildingResources : MonoBehaviour
{
    //建材のリスト
    [SerializeField]
    private  List<BuildingResources> m_buildingResources;

    //リストへの追加
    public void Add(BuildingResources _buildingResources)
    {
        m_buildingResources.Add(_buildingResources);

    }

    //建材の取得
    public List<BuildingResources> BuildingResouce
    {
        get { return m_buildingResources; }
    }
}
