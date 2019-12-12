using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    //拠点のタイプ
    public Type m_placeType;

   public Type GetPlaceType()
    {
        return m_placeType;
    }
}
