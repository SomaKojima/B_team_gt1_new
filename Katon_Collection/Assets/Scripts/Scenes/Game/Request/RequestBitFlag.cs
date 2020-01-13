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
    MOVE_CAMERA = 1 << 1,       // カメラを移動
    UNDO_CAMERA = 1 << 2,       // ひとつ前にカメラに戻る
    BUILDING =  1 << 3,          // 建築する
    CREADED_QR = 1 << 4,        // QRを作成したフラグ
    STOP_CAMERA = 1 << 5,       // カメラを止める
    START_CAMERA = 1 << 6,      // カメラを動きを再開する
    EXCHANGE    = 1 << 7,       // 交換

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