using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ビットフラグの種類
/// </summary>
public enum REQUEST_BIT_FLAG_TYPE
{
    IMMEDIATELY,    // 即座にリクエストを飛ばす
    FADE,           // フェードインが終わったタイミングでリクエストを飛ばす

    MAX
}

/// <summary>
/// リクエスト
/// </summary>
public enum REQUEST
{
    NONE = 0,
    CAMERA_MOVE_PLACE = 1 << 1,       // カメラを移動
    CAMERA_UNDO = 1 << 2,       // ひとつ前にカメラに戻る
    BUILDING =  1 << 3,          // 建築する
    CREADED_QR = 1 << 4,        // QRを作成したフラグ
    CAMERA_STOP = 1 << 5,       // カメラを止める
    CAMERA_START = 1 << 6,      // カメラの動きを再開
    EXCHANGE    = 1 << 7,       // 交換
    CAMERA_SCROLL = 1 << 8,      // カメラの動きをscrollにする
    CAMERA_OUT_RANGE = 1 << 9,  // カメラの動きをマウスカーソルが範囲外の時にする
    COLLECT = 1 << 10,          // 収集
    POSITION_TO_PLACE = 1 << 11,   // 座標から場所のタイプに変換処理
    POWER_UP_HUMAN = 1 << 12,       // 人間の強化
    QR_READE,                       // QRを読み込んだ
    GET_CURRENT_PLACE_HUMAN_INFO,   // 現在地の人間の情報を知りたい

    MAX
}

/// <summary>
/// リクエストの管理
/// </summary>
public class RequestBitFlag
{
    // リクエスト用のビットフラグ
    BitFlag[] bitRequestFlag = new BitFlag[(int)REQUEST_BIT_FLAG_TYPE.MAX];

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        for (int i = 0; i < (int)REQUEST_BIT_FLAG_TYPE.MAX; i++)
        {
            bitRequestFlag[i] = new BitFlag();
        }
    }

    /// <summary>
    /// リクエストがあるかどうかの確認
    /// </summary>
    /// <param name="_request"></param>
    /// <returns></returns>
    public bool IsFlag(REQUEST _request)
    {
        return bitRequestFlag[(int)REQUEST_BIT_FLAG_TYPE.IMMEDIATELY].IsFlag((int)_request);
    }

    /// <summary>
    /// 列挙型でフラグを立てる
    /// </summary>
    /// <param name="_request"></param>
    public void OnFlag(REQUEST_BIT_FLAG_TYPE _type, REQUEST _request)
    {
        bitRequestFlag[(int)_type].OnFlag((int)_request);
    }
    
    /// <summary>
    /// フラグを伏せる
    /// </summary>
    /// <param name="_request"></param>
    public void OffFlag(REQUEST_BIT_FLAG_TYPE _type, REQUEST _request)
    {
        bitRequestFlag[(int)_type].OffFlag((int)_request);
    }
    
    /// <summary>
    /// ビットフラグをリクエストに反映させる
    /// </summary>
    /// <param name="_type"></param>
    public void Reflection(REQUEST_BIT_FLAG_TYPE _type)
    {
        int bufBitFlag = bitRequestFlag[(int)_type].GetBitFlag();
        bitRequestFlag[(int)REQUEST_BIT_FLAG_TYPE.IMMEDIATELY].OnFlag(bufBitFlag);
    }

    /// <summary>
    /// フラグを全て伏せる
    /// </summary>
    public void Clear(REQUEST_BIT_FLAG_TYPE _type)
    {
        bitRequestFlag[(int)_type].Clear();
    }

    /// <summary>
    /// 全てのフラグを伏せる
    /// </summary>
    public void AllClear()
    {
        for (int i = 0; i < (int)REQUEST_BIT_FLAG_TYPE.MAX; i++)
        {
            bitRequestFlag[i].Clear();
        }
    }

    /// <summary>
    /// フラグの取得
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    public int GetBitFlag(REQUEST_BIT_FLAG_TYPE _type)
    {
        return bitRequestFlag[(int)_type].GetBitFlag();
    }
}