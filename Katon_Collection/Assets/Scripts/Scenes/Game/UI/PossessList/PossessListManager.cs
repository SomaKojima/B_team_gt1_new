using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessListManager : MonoBehaviour
{
    // 枠に収まっているかを決める範囲(Rect)
    [SerializeField]
    private Camera CountainArea;

    // 範囲内判定のためのrect
    Rect rect = new Rect(0, 0, 3, 3); // 画面内か判定するためのRect

    // 基準オブジェクト
    [SerializeField]
    private GameObject basePointObject;

    // 表示状態
    private bool moveState = false;

    // 動かすスピード
    [SerializeField]
    private float moveSpeed = 20.0f;

    private float speed = 0;

    // RectTransform
    private RectTransform rectTransform;
    private RectTransform baseRectTransform;

    // PossessListオブジェクト
    [SerializeField]
    private PossessList possessList = null;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = moveSpeed;
        rectTransform = possessList.gameObject.GetComponent<RectTransform>();
        baseRectTransform = basePointObject.GetComponent<RectTransform>();
        // 所持リストの幅取得
        possessList.Initialize(possessList.GetComponent<RectTransform>().sizeDelta.x);
    }

    // Update is called once per frame
    void Update()
    {
        // 座標変換
        var viewportPos = CountainArea.WorldToViewportPoint(basePointObject.transform.position);
        // なぜか範囲が(-10,-10)～(0,0)になっているため符号反転
        var pos = new Vector3(-viewportPos.x, -viewportPos.y, -viewportPos.z);

        // クリックされたら
        if (possessList.GetClick())
        {
            possessList.FinishClick();
            // フラグを反転
            moveState = !moveState;
            speed *= -1;
        }

        // テキスト変更
        if(moveState)
        {
            possessList.SetButtonText("－");
        }
        else
        {
            possessList.SetButtonText("＋");
        }

        // 
        float min = baseRectTransform.position.x - rectTransform.sizeDelta.x;
        float max = baseRectTransform.position.x;
        float x = ChangePosition(min, max, rectTransform.position.x, speed);
        // 所持リストの移動
        rectTransform.position = new Vector3(x, rectTransform.position.y, rectTransform.position.z);

        if (IsMove(min, max, rectTransform.position.x, speed))
        {
            // 移動中
        }
        else
        {
            // 停止中
        }
    }

    float ChangePosition(float min, float max, float position, float speed)
    {
        float buf = position + speed;
        if (buf < min)
        {
            return min;
        }
        else if (buf > max)
        {
            return max;
        }
        return buf;
    }

    bool IsMove(float min, float max, float position, float speed)
    {
        float buf = position + speed;
        if (buf < min)
        {
            return false;
        }
        else if (buf > max)
        {
            return false;
        }
        return true;
    }
}
