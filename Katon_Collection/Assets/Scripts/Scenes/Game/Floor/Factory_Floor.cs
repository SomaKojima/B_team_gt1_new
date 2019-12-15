using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Floor : MonoBehaviour
{

    [SerializeField]
    private GameObject m_prefab = null;

    [SerializeField]
    private Transform m_parent = null;

    //生成する
    public Floor Create()
    {
        GameObject instance = Instantiate(m_prefab);
        instance.transform.SetParent(m_parent.transform, false);

        Floor floor = instance.GetComponent<Floor>();
    
        return floor;
    }

    
}
