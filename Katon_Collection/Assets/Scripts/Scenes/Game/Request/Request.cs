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

    Type changePlaceType;
    Vector3 changePosition;
    
    Type collectPlaceType;
    ITEM_TYPE collectItemType;

    // 場所の中心座標
    Vector3 areaCenterPosition;

    ITEM_TYPE powerUpHumanType = ITEM_TYPE.NONE;

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

    public Vector3 ChangePosition
    {
        get { return changePosition; }
        set { changePosition = value; }
    }

    public Type ChangePlaceType
    {
        get { return changePlaceType; }
        set { changePlaceType = value; }
    }

    public Type CollectPlaceType
    {
        get { return collectPlaceType; }
        set { collectPlaceType = value; }
    }

    public ITEM_TYPE CollectItemType
    {
        get { return collectItemType; }
        set { collectItemType = value; }
    }

    public Vector3 AreaCenterPosition
    {
        get { return areaCenterPosition; }
        set { areaCenterPosition = value; }
    }

    public ITEM_TYPE PowerUpHumanType
    {
        get { return powerUpHumanType; }
        set { powerUpHumanType = value; }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void FinalizeRequest()
    {
        Flag.Clear(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY);
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

    /// <summary>
    /// 収集終了時のリクエスト処理
    /// </summary>
    /// <param name="isCollectable"></param>
    public void FinalizeCollect(bool isCollectable)
    {
        if (isCollectable)
        {
            replayFlag.OnFlag(REPLAY_REQUEST.COLLECT_SUCCESS);
        }
        else
        {
            replayFlag.OnFlag(REPLAY_REQUEST.COLLECT_FALIED);
        }
    }

    /// <summary>
    /// 座標を場所に変更したリクエスト処理
    /// </summary>
    /// <param name="isChangable"></param>
    public void FinalizePositionToPlace(bool isChangable)
    {
        if (isChangable)
        {
            replayFlag.OnFlag(REPLAY_REQUEST.POSITION_TO_PLACE_SUCCESS);
        }
        else
        {

            replayFlag.OnFlag(REPLAY_REQUEST.POSITION_TO_PLACE_FAILED);
        }
    }
    
}
