using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraMove : MonoBehaviour
{
    //カメラ
    [SerializeField]
    private Camera m_camera = null;

    //カメラのマネージャー
    [SerializeField]
    private CameraManager m_cameraManager = null;


    //初期化
    public void Initialize()
    {
        m_camera = Camera.main;
    }


    //ポジションを切り替える
    public void ChangePosition(Type _placeType)
    {
        m_camera.transform.position = m_cameraManager.GetPositionOf(_placeType);
    }

}
