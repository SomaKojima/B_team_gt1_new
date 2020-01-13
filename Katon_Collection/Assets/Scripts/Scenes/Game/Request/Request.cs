using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リクエスト
/// </summary>
public class Request
{
    RequestBitFlag flag = new RequestBitFlag();
    ReplyRequestBitFlag replayFlag = new ReplyRequestBitFlag();

    List<IItem> exchangeItems = null;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        flag.Initialize();
        replayFlag.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// フラグを取得
    /// </summary>
    public RequestBitFlag Flag
    {
        get { return flag; }
    }

    public ReplyRequestBitFlag ReplayFlag
    {
        get { return replayFlag; }
    }
    
    /// <summary>
    /// 交換のアイテム
    /// </summary>
    public List<IItem> ExchangeItems
    {
        get { return exchangeItems; }
        set { exchangeItems = value; }
    }

    /// <summary>
    /// 建築終了時のリクエスト処理
    /// </summary>
    /// <param name="isBuildable">建築できたかどうか</param>
    public void FinalizeBuilding(bool isBuildable)
    {
        if (isBuildable)
        {
            replayFlag.OnFlag(REPLAY_REQUEST.BUILDING_SUCCESS);
        }
        else
        {
            replayFlag.OnFlag(REPLAY_REQUEST.BUILDING_FALIED);
        }
    }

    /// <summary>
    /// 交換終了時のリクエスト処理
    /// </summary>
    /// <param name="isExchangable">交換できたかどうか</param>
    public void FinalizeExchange(bool isExchangable)
    {
        if (isExchangable)
        {
            replayFlag.OnFlag(REPLAY_REQUEST.EXCHANGE_SUCCESS);
        }
        else
        {
            replayFlag.OnFlag(REPLAY_REQUEST.EXCHANGE_FALIED);
        }
    }
}
