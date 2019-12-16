using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_BuildingResources : MonoBehaviour
{
    //建材の資源
    [SerializeField]
    private GameObject m_prefab = null;

    //トランスフォーム
    [SerializeField]
    private Transform m_parent = null;

    //建材の生成
    public BuildingResources Create()
    {
        GameObject instance = Instantiate(m_prefab);
        instance.transform.SetParent(m_parent.transform, false);

        BuildingResources building = instance.GetComponent<BuildingResources>();


        building.Initialize();



        return building;
    }

}
