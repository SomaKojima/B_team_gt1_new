using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessListManager : MonoBehaviour
{
    // 枠に収まっているかを決める範囲(Rect)
    [SerializeField]
    private Camera CountainArea;

    // 範囲内判定のためのrect
    Rect rect = new Rect(0, 0, 10, 10); // 画面内か判定するためのRect

    // 基準オブジェクト
    [SerializeField]
    private GameObject basePointObject;

    // 表示中かどうかフラグ
    private bool visibleFlag = true;

    // 動作中かどうかフラグ
    private bool moveState = false;

    // フラグ
    private bool flag = false;

    // 動かすスピード
    [SerializeField]
    private float moveSpeed = 20.0f;

    // PossessListオブジェクト
    [SerializeField]
    private PossessList possessList = null;
    
    // Start is called before the first frame update
    void Start()
    {
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
        
        // 範囲に含まれていたら
        if (rect.Contains(pos))
        {
            // 表示できている
            visibleFlag = true;
        }
        // 含まれていなかったら
        else
        {
            // 表示できていない
            visibleFlag = false;
        }

        // クリックされたら
        if (possessList.GetClick())
        {
            // フラグを反転
            //if (moveState) return;
            flag = !flag;
        }

        // 表示されておらず表示したかったら
        if (!visibleFlag && flag)
        {
            // 画面左へスライド
            possessList.transform.position -= new Vector3(moveSpeed, 0, 0);
            if (moveState) return;
            // 所持リスト表示/非表示用の基点設定
            basePointObject.transform.position = new Vector3(basePointObject.transform.position.x + possessList.GetWidth(),
                                                             basePointObject.transform.position.y,
                                                             basePointObject.transform.position.z);
            // 所持リストがスライド可能でなければ動かさない
            moveState = true;
            possessList.FinishClick();
        }
        // 表示されており非表示にしたかったら
        else if (visibleFlag && !flag)
        {
            // 画面右へスライド
            possessList.transform.position += new Vector3(moveSpeed, 0, 0);
            if (moveState) return;
            // 所持リスト表示/非表示用の基点設定
            basePointObject.transform.position = new Vector3(basePointObject.transform.position.x - possessList.GetWidth(),
                                                             basePointObject.transform.position.y,
                                                             basePointObject.transform.position.z);
            // 所持リストがスライド可能でなければ動かさない
            moveState = true;
            possessList.FinishClick();
        }
        else
        {
            moveState = false;
        }
    }

    // 設定関数
    public bool MoveState { set { moveState = value; } }
}
