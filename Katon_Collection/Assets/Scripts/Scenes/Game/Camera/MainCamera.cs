using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    // カメラの動き
    [SerializeField]
    CameraMove cameraMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public bool IsSigneBoardInScreen(Vector3 _position)
    {
        Rect rect = new Rect(0, 0, 1, 1);
        Vector3 point = camera.WorldToViewportPoint(_position);
        if (rect.Contains(point))
        {
            return true;
        }
        return false;
    }

    // カメラを動かす
    public void Move(Type _placeType)
    {
        if (_placeType == Type.none) return;
        cameraMove.ChangePosition(_placeType);
    }

    // カメラの位置をひとつ戻す
    public void Undo()
    {
        cameraMove.Undo();
    }

    public void StopMove()
    {
        cameraMove.StopMove();
    }

    public void StartMove()
    {
        cameraMove.StartMove();
    }

    public void ChangeMoveType(CameraMove.CAMERA_MOVE_TYPE _type)
    {
        cameraMove.ChangeMoveType(_type);
    }
}
