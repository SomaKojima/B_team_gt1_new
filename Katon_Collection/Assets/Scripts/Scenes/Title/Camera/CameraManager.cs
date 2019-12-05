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


    //カメラの場所をチェンジする
    public void ChangeCamera(Type type)
    {

        foreach (CameraTransform ct in m_cameraTransform)
        {
            
            switch (ct.GetPlaceType())
            {
                case Type.none:
                    type = Type.none;
                    break;
                case Type.market:
                    type = Type.market;
                    break;
                case Type.fountain:
                    type = Type.fountain;
                    break;
                case Type.forest:
                    type = Type.forest;
                    break;
                case Type.cave:
                    type = Type.cave;
                    break;
                case Type.factory:
                    type = Type.factory;
                    break;
                case Type.farm:
                    type = Type.farm;
                    break;
            }
        }    
    }
}
