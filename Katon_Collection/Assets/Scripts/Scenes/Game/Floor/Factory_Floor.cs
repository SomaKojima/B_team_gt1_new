using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Floor : MonoBehaviour
{

    [SerializeField]
    private GameObject floorPrefab;

    [SerializeField]
    private GameObject basePrefab;

    [SerializeField]
    private Transform m_parent = null;

    //生成する
    public Floor CreateFloor(Vector3 position)
    {
        GameObject instance = Instantiate(floorPrefab);
        instance.transform.SetParent(m_parent.transform, false);
        instance.transform.position = position;

        Floor floor = instance.GetComponent<Floor>();
    
        return floor;
    }

    public Floor CreateBase(Vector3 position)
    {
        GameObject instance = Instantiate(basePrefab);
        instance.transform.SetParent(m_parent.transform, false);
        instance.transform.position = position;

        Floor floor = instance.GetComponent<Floor>();

        return floor;
    }
}
