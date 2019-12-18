using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //タイプ
    Type m_placeType;

    //拠点の情報
    [SerializeField]
    List<CameraTransform> m_cameraTransform = new List<CameraTransform>(); 

    public Vector3 GetPositionOf(Type placetype)
    {

        foreach (CameraTransform ct in m_cameraTransform)
        {
            if(ct.GetPlaceType()==placetype)
            {
                return ct.transform.position;
            }
        }

        return Vector3.zero;
    }
}
