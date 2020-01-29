using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 返答リクエスト
/// </summary>
public enum REPLAY_REQUEST
{
    NONE = 0,

    EXCHANGE_FALIED = 1 << 1,       // 交換失敗
    EXCHANGE_SUCCESS= 1 << 2,       // 交換成功

    BUILDING_FALIED = 1 << 3,       // 建築失敗
    BUILDING_SUCCESS= 1 << 4,       // 建築成功

    COLLECT_FALIED = 1 << 5,        // 収集失敗
    COLLECT_SUCCESS = 1 << 6,       // 収集成功

    POSITION_TO_PLACE_SUCCESS = 1 << 7, // 座標を場所のタイプに変換成功     
    POSITION_TO_PLACE_FAILED = 1 << 8,  // 座標を場所のタイプに変換失敗

    POWER_UP_SUCCESS = 1 << 9,
    POWER_UP_FAILED = 1 << 10,

    MAX
}

/// <summary>
/// リクエスト処理結果の返答するためのクラス
/// </summary>
public class ReplyRequestBitFlag
{
    BitFlag replayFlag = new BitFlag();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        
    }

    /// <summary>
    /// リクエストがあるかどうかの確認
    /// </summary>
    /// <param name="_request"></param>
    /// <returns></returns>
    public bool IsFlag(REPLAY_REQUEST _request)
    {
        return replayFlag.IsFlag((int)_request);
    }

    /// <summary>
    /// 列挙型でフラグを立てる
    /// </summary>
    /// <param name="_request"></param>
    public void OnFlag(REPLAY_REQUEST _request)
    {
        replayFlag.OnFlag((int)_request);
    }
    
    /// <summary>
    /// フラグを伏せる
    /// </summary>
    /// <param name="_request"></param>
    public void OffFlag(REPLAY_REQUEST _request)
    {
        replayFlag.OffFlag((int)_request);
    }
    
    /// <summary>
    /// フラグを全て伏せる
    /// </summary>
    public void Clear()
    {
        replayFlag.Clear();
    }
    
    /// <summary>
    /// フラグの取得
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    public int GetBitFlag()
    {
        return replayFlag.GetBitFlag();
    }
}
