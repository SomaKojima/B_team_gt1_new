using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

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


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        ChangePosition(Type.factory);
        Scroll();
       
    }

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

    //スクロール
    public void Scroll()
    {
        // マウスボタンをクリック
        if (Input.GetMouseButtonDown(0))
        {
            // 現在の座標を記憶
            playerPos = moveObject.transform.position;
            // マウスの座標を記憶
            mousePos = Input.mousePosition;
        }

        // 押している間
        if (Input.GetMouseButton(0))
        {
            // 現在の座標を記憶
            Vector3 prePos = moveObject.transform.localPosition;
            // タップ中の移動量を調整
            Vector3 diff = ((mousePos - Input.mousePosition) * 0.05f);

            // 横方向のみ移動
            diff.y = 0.0f;
            diff.z = 0.0f;
            // 移動できる範囲内に限り
            if (moveObject.transform.localPosition.x - 10.0f < moveObject.transform.localPosition.x && moveObject.transform.localPosition.x < moveObject.transform.localPosition.x+10.0f)
            {
                moveObject.transform.localPosition = playerPos + diff;
            }
            // 座標矯正
            if (moveObject.transform.localPosition.x > moveObject.transform.localPosition.x+10.0f)
            {
                moveObject.transform.localPosition = CorrectionPosition(moveObject.transform.localPosition.x+5.0f, moveObject.transform.localPosition.y, moveObject.transform.localPosition.z);
            }
            if (moveObject.transform.localPosition.x < moveObject.transform.localPosition.x- 10.0f)
            {
                moveObject.transform.localPosition = CorrectionPosition(moveObject.transform.localPosition.x - 5.0f, moveObject.transform.localPosition.y, moveObject.transform.localPosition.z);
            }

            // 移動前後の位置の差から速度を求めてvelocityに保存しておく
            // ※掛ける値で移動量の大きさを変更する
            velocity = ((moveObject.transform.localPosition - prePos) * 0.5f) / Time.deltaTime;
        }
        else
        {
            // スワイプ中でないとき、velocityを減衰させる
            velocity *= Mathf.Pow(Attenuation, Time.deltaTime);

            // 移動できる範囲内に限り
            if (moveObject.transform.localPosition.x - 10.0f < moveObject.transform.localPosition.x && moveObject.transform.localPosition.x < moveObject.transform.localPosition.x+10.0f)
            {
                // プレイヤーを移動する
                moveObject.transform.localPosition += velocity * Time.deltaTime;

                // 座標矯正
                if (moveObject.transform.localPosition.x > moveObject.transform.localPosition.x + 10.0f)
                {
                    moveObject.transform.localPosition = CorrectionPosition(moveObject.transform.localPosition.x + 5.0f, moveObject.transform.localPosition.y, moveObject.transform.localPosition.z);
                }
                if (moveObject.transform.localPosition.x < moveObject.transform.localPosition.x - 10.0f)
                {
                    moveObject.transform.localPosition = CorrectionPosition(moveObject.transform.localPosition.x - 5.0f, moveObject.transform.localPosition.y, moveObject.transform.localPosition.z);
                }
            }
        }

        // マウスボタンから離した
        if (Input.GetMouseButtonUp(0))
        {
            // 保存値をリセット
            playerPos = Vector3.zero;
            mousePos = Vector3.zero;
        }
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
}
