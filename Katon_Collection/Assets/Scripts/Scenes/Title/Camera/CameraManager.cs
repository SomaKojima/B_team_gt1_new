using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //拠点の情報
    [SerializeField, EnumListLabel(typeof(Type))]
    Transform[] m_cameraTransform = new Transform[(int)Type.Max]; 

    public Transform GetTransformOf(Type placetype)
    {
        return m_cameraTransform[(int)placetype];
    }
}
