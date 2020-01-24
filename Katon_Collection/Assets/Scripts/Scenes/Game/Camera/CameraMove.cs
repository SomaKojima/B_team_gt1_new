using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public enum CAMERA_MOVE_TYPE
    {
        SCROLL,             // scrollで移動
        MOUSE_OUTRANGE,     // マウスが範囲外にいるときに移動
    }

    [SerializeField]
    private Camera m_camera;

    [SerializeField]
    private CameraManager m_cameraManager = null;

    // 動くオブジェクトの座標(一時保存値)
    private Vector3 playerPos;
    
    // マウスの座標
    private Vector3 mousePos;

    // 動かすオブジェクト
    [SerializeField]
    private GameObject moveObject; 

    // 動かすオブジェクトの座標
    private Vector3 moveObjectPos;

    // 動くオブジェクトの速度
    private Vector3 velocity;

    // 速度の減衰率を示すフィールドを追加
    // 0ならピタッと止まり、1なら減速せず飛び続ける
    [Range(0.0f, 1.0f)]
    public float Attenuation = 0.5f;

    bool isStop = false;

    // 一つ前に戻すかどうか
    bool isUndo = false;
    // ひとつ前の場所
    List<Vector3> undoPositions = new List<Vector3>();

    CAMERA_MOVE_TYPE moveType = CAMERA_MOVE_TYPE.SCROLL;

    // 最初に触れた位置
    private Vector3 mousePosStart = Vector3.zero;
    // 移動中、以前に触れた位置
    private Vector3 mousePosTmp = Vector3.zero;
    // 現在の位置
    private Vector3 mousePosCurrent = Vector3.zero;

    // フリック判定用経過時間計測
    private float duration = 0.0f;
    private const float DO_FLICK_TIME = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            switch (moveType)
            {
                case CAMERA_MOVE_TYPE.SCROLL:
                    Scroll();
                    break;
                case CAMERA_MOVE_TYPE.MOUSE_OUTRANGE:
                    MouseOutRange();
                    break;
            }
        }
        else
        {
            velocity = Vector3.zero;
        }
    }

    //初期化
    public void Initialize()
    {
        m_camera = Camera.main;
    }


    //ポジションを切り替える
    public void ChangePosition(Type _placeType)
    {
        // 現在の座標を保存する
        undoPositions.Add(m_camera.transform.position);
        // 最大数１０
        if (undoPositions.Count > 10)
        {
            undoPositions.RemoveAt(0);
        }
        // 移動
        m_camera.transform.position = m_cameraManager.GetTransformOf(_placeType).position;
    }

    public void ChangeMoveType(CAMERA_MOVE_TYPE _type)
    {
        moveType = _type;
    }
    
    /// <summary>
    /// scrollの移動
    /// </summary>
    private void Scroll()
    {
        // タッチ情報の取得
        TouchInfo info = TouchUtil.GetTouch();

        Vector3 _position = moveObject.transform.localPosition;

        // タップ状態の分岐
        switch (info)
        {
            // タップ開始
            case TouchInfo.Began:

                // タッチ開始座標取得
                mousePosStart = Input.mousePosition;
                mousePosTmp = Input.mousePosition;

                // 計測準備
                duration = 0;
                break;

            // タップ中、指が動いている
            case TouchInfo.Moved:

                // 経過時間計測
                duration += Time.deltaTime;

                // 移動量算出
                mousePosCurrent = Input.mousePosition;
                velocity = mousePosTmp - mousePosCurrent;
                // 速度調整
                velocity *= 0.1f;

                // 位置更新
                mousePosTmp = mousePosCurrent;

                break;

            // タップ終了
            case TouchInfo.Ended:

                // タップ終了位置取得
                Vector2 endPos = Input.mousePosition;

                // 触れていた秒数でフリックとスワイプ分岐
                if (duration <= DO_FLICK_TIME)
                {
                    float flickLengthX = mousePosStart.x - endPos.x;
                    float flickLengthY = mousePosStart.y - endPos.y;

                    // フリックの長さを速度に変換する
                    velocity.x = flickLengthX / 100.0f;
                    velocity.y = flickLengthY / 100.0f;
                }
                else
                {
                    //フリック判定じゃない場合は速度を0にする
                    velocity = Vector3.zero;
                }
                break;

            // 触れてないとき
            default:
                // velocityを減衰させる
                velocity *= 0.9f;
                break;
        }

        //if (Input.GetMouseButton(0))
        //{
        //    // 速度の計算
        //    float x = -Input.GetAxis("Mouse X");
        //    float y = -Input.GetAxis("Mouse Y");
        //    velocity = new Vector3(x, y, 0);
        //}
        //else
        //{
        //    // スワイプ中でないとき、velocityを減衰させる
        //    velocity *= 0.99f;
        //}

        float top = 70;
        float bottom = 20;
        float left = -20;
        float right = 650;
        // x軸
        _position = new Vector3(Move(_position.x, velocity.x, left, right), _position.y, _position.z);
        // y軸
        _position = new Vector3(_position.x, Move(_position.y, velocity.y, bottom, top), _position.z);

        moveObject.transform.localPosition = _position;
    }

    /// <summary>
    /// 範囲外の時に移動
    /// </summary>
    void MouseOutRange()
    {

        Vector3 _position = moveObject.transform.localPosition;
        float screenHalfHeight = Screen.height * 0.5f;
        float screenHalfWidth = Screen.width * 0.5f;
        float top = screenHalfHeight + (screenHalfHeight * 0.5f);
        float bottom = screenHalfHeight - (screenHalfHeight * 0.5f);
        float left = screenHalfWidth - (screenHalfWidth * 0.5f);
        float right = screenHalfWidth + (screenHalfWidth * 0.5f);
        float addSpeed = 1.0f;

        // 範囲内
        if (IsRange(Input.mousePosition.x, left, right) &&
            IsRange(Input.mousePosition.y, bottom, top))
        {
            // velocityを減衰させる
            velocity = Vector3.zero;
            return;
        }

        // x軸
        float tX = (Input.mousePosition.x - screenHalfWidth) / screenHalfWidth; 
        velocity.x = tX * addSpeed;
        // y軸
        float tY = (Input.mousePosition.y - screenHalfHeight) / screenHalfHeight;
        velocity.y = tY * addSpeed;

        // 速度の調整
        //if (velocity.x > maxSpeed) velocity.x = maxSpeed;
        //else if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;

        //if (velocity.y > maxSpeed) velocity.y = maxSpeed;
        //else if (velocity.y < -maxSpeed) velocity.y = -maxSpeed;
        

        float _top = 70;
        float _bottom = 20;
        float _left = -20;
        float _right = 430;

        // x軸
        _position = new Vector3(Move(_position.x, velocity.x, _left, _right), _position.y, _position.z);
        // y軸
        _position = new Vector3(_position.x, Move(_position.y, velocity.y, _bottom, _top), _position.z);

        //Debug.Log(_position);

        moveObject.transform.localPosition = _position;
    }

    /// <summary>
    /// 移動可能かどうか
    /// </summary>
    /// <returns></returns>
    float Move(float _value, float _velocity, float _min, float _max)
    {
        if (_value + _velocity < _min) return _min;
        if (_value + _velocity > _max) return _max;
        return _value + _velocity;
    }

    bool IsRange(float _value, float _min, float _max)
    {
        if (_value < _min) return false;
        if (_value > _max) return false;
        return true;
    }

    /// <summary>
    /// 座標矯正関数
    /// </summary>
    /// <param name="corPosX">X座標</param>
    /// <param name="corPosY">Y座標</param>
    /// <param name="corPosZ">Z座標</param>
    /// <returns>矯正後の座標</returns>
    private Vector3 CorrectionPosition(float corPosX, float corPosY, float corPosZ)
    {
        Vector3 pos;
        pos.x = corPosX;
        pos.y = corPosY;
        pos.z = corPosZ;

        return pos;
    }


    /// <summary>
    /// 取得・設定関数
    /// </summary>
    public GameObject MoveObject { get { return moveObject; } set { moveObject = value; } }

    public Camera GetCamera()
    {
        return m_camera;
    }
    
    public void StopMove()
    {
        isStop = true;
    }

    public void StartMove()
    {
        isStop = false;
    }


    // ひとつ前の座標に戻す
    public void Undo()
    {
        if (undoPositions.Count != 0)
        {
            int index = undoPositions.Count - 1;
            m_camera.transform.position = undoPositions[index];
            undoPositions.RemoveAt(index);
        }
    }
}
