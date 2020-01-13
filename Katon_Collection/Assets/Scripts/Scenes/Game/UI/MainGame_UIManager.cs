using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainGame_UIManager : MonoBehaviour
{
    // QR読み込みウィンドウ
    [SerializeField]
    QR_ReaderWindow qrReaderWindow;

    // 拠点移動バー
    [SerializeField]
    Manager_PlaceBar manager_placeBar;

    // 建築ボード（建築に必要な素材表示、建築ボタン）
    [SerializeField]
    BuildingBoard buildingBoard;

    // フェード
    [SerializeField]
    Fade_CloudEffect fade_CloudEffect = null;


    // 噴水のウィンドウ
    [SerializeField]
    FountainWindow fountainWindow;

    // 市場のウィンドウ
    [SerializeField]
    MarketWindow marketWindow;


    // 所持アイテムウィンドウ
    [SerializeField]
    PossessListManager possessListManager;


    // リクエスト用のビットフラグ
    Request request = new Request();

    // UIのアクティブを管理するビットフラグ
    RequestActiveUI requestActiveUI = new RequestActiveUI();
    
    // 交換処理をするときに使うアイテムリスト
    List<IItem> exchangeItems = new List<IItem>();

    // 交換相手のID
    int otherID = -1;

    // フェードインが終了したかどうかのフラグ
    bool isFinishFadeIn = false;


    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="_managerItem"></param>
    public void Initialize(Manager_Item _managerItem)
    {
        fountainWindow.Initialize(_managerItem);
        marketWindow.Initialize(_managerItem);
        buildingBoard.Initialize();
        possessListManager.Initialize();
        fade_CloudEffect.StartFadeOut();


        request.Initialize();
        requestActiveUI.Initailize();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // 初期化
        exchangeItems.Clear();

        
        // フェードのリクエスト処理
        UpdateRequest_Fade();

        // 建築のボードのリクエスト処理
        UpdateRequest_BuildingBoard();

        // QRリーダーのリクエスト処理
        UpdateRequest_QRReader();

        // 市場のリクエスト処理
        UpdateRequest_Market();

        // 噴水のリクエスト処理
        UpdateRequest_Fountain();

        // 移動バーのリクエスト処理
        UpdateRequest_PlaceBar();


        // UIを有効化する処理
        UpdateActive();

        // UIを無効化する処理
        UpdateUnActive();
    }

    /// <summary>
    /// UIを有効化する処理
    /// </summary>
    void UpdateActive()
    {
        // 移動バーを有効化
        if (requestActiveUI.IsActive(ACTIVE_UI.PLACE_BAR))
        {
            manager_placeBar.Active();
        }

        // 市場を有効化
        if (requestActiveUI.IsActive(ACTIVE_UI.MARKET))
        {
            marketWindow.Active();
        }

        // 噴水を有効化
        if (requestActiveUI.IsActive(ACTIVE_UI.FOUNTAIN))
        {
            fountainWindow.Active();
        }

        // QRリーダーを有効化
        if (requestActiveUI.IsActive(ACTIVE_UI.QR_READER))
        {
            qrReaderWindow.Initialize();
        }

        // 建築ボードを有効化
        if (requestActiveUI.IsActive(ACTIVE_UI.BUILDIGN_BOARD))
        {
            buildingBoard.Active();
        }

        // フラグをすべて初期化する
        requestActiveUI.ClearActiveFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY);
    }

    /// <summary>
    /// UIを無効化する処理
    /// </summary>
    void UpdateUnActive()
    {
        // 移動バーを無効化
        if (requestActiveUI.IsUnActive(ACTIVE_UI.PLACE_BAR))
        {
            manager_placeBar.UnActive();
        }

        // 市場を無効化
        if (requestActiveUI.IsUnActive(ACTIVE_UI.MARKET))
        {
            marketWindow.UnActive();
        }

        // 噴水を無効化
        if (requestActiveUI.IsUnActive(ACTIVE_UI.FOUNTAIN))
        {
            fountainWindow.UnActive();
        }

        // QRリーダーを無効化
        if (requestActiveUI.IsUnActive(ACTIVE_UI.QR_READER))
        {
        }

        // 建築ボードを無効化
        if (requestActiveUI.IsUnActive(ACTIVE_UI.BUILDIGN_BOARD))
        {
            buildingBoard.UnActive();
        }

        // フラグをすべて初期化する
        requestActiveUI.ClearUnActiveFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY);
    }

    /// <summary>
    /// 建築のボードの更新処理
    /// </summary>
    void UpdateRequest_BuildingBoard()
    {
        // 建築ボタン
        if (buildingBoard.IsClickBuildingButton())
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.BUILDING);
        }
    }

    /// <summary>
    /// フェードのリクエスト処理
    /// </summary>
    void UpdateRequest_Fade()
    {
        isFinishFadeIn = false;
        //　フェードインが終わった時の処理
        if (fade_CloudEffect.GetIsProcess)
        {
            isFinishFadeIn = true;
            //フェードアウトの処理
            fade_CloudEffect.StartFadeOut();

            // リクエスト処理
            request.Flag.Reflection(REQUEST_BIT_FLAG_TYPE.FADE);
            request.Flag.Clear(REQUEST_BIT_FLAG_TYPE.FADE);

            // 有効化の処理
            requestActiveUI.Reflection_ActiveFlag(ACTIVE_BIT_FLAG_TYPE.FADE);
            requestActiveUI.ClearActiveFlag(ACTIVE_BIT_FLAG_TYPE.FADE);

            // 無効化の処理
            requestActiveUI.Reflection_UnActiveFlag(ACTIVE_BIT_FLAG_TYPE.FADE);
            requestActiveUI.ClearUnActiveFlag(ACTIVE_BIT_FLAG_TYPE.FADE);
        }
    }

    /// <summary>
    /// QRリーダーのリクエスト処理
    /// </summary>
    void UpdateRequest_QRReader()
    {
        // qr読み込みの交換処理
        if (qrReaderWindow.IsExchange())
        {
            exchangeItems = qrReaderWindow.GetItems();
            otherID = qrReaderWindow.GetOtherID();
        }
    }

    /// <summary>
    /// 噴水のリクエスト処理
    /// </summary>
    public void UpdateRequest_Fountain()
    {
        // 戻るボタン
        if (fountainWindow.IsBack())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.FOUNTAIN);

            // カメラの挙動リクエスト

            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.FADE, REQUEST.UNDO_CAMERA | REQUEST.START_CAMERA);

            fade_CloudEffect.StartFadeIn();
        }

        // 交換
        if (fountainWindow.IsExchange())
        {
            exchangeItems = qrReaderWindow.GetItems();
        }

        // Qrコードを生成した
        if(fountainWindow.IsCreateQR())
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CREADED_QR);
        }
    }

    /// <summary>
    /// 市場のリクエスト処理
    /// </summary>
    public void UpdateRequest_Market()
    {
        // 戻るボタン
        if (marketWindow.IsBack())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.MARKET);

            // カメラの挙動リクエスト

            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.FADE, REQUEST.UNDO_CAMERA | REQUEST.START_CAMERA);

            fade_CloudEffect.StartFadeIn();
        }

        // 交換
        if (marketWindow.IsExchange())
        {
            exchangeItems = marketWindow.GetExchangeItemList();
        }
    }

    /// <summary>
    /// 移動バーのリクエスト
    /// </summary>
    private void UpdateRequest_PlaceBar()
    {
        // 拠点ボタンが有効化どうか
        if (manager_placeBar.IsActiveBase())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.BUILDIGN_BOARD);
        }

        // 噴水ウィンドウを表示
        if (manager_placeBar.IsActiveFountain())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.FOUNTAIN);
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);

            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.STOP_CAMERA);
        }

        // 市場ウィンドウを表示
        if (manager_placeBar.IsActiveShop())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.MARKET);
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);

            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.STOP_CAMERA);
        }

        // QRリーダーを起動
        if (manager_placeBar.GetIsQRLeader())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.QR_READER);
        }

        // 移動する
        if (manager_placeBar.IsChangeCameraPosiiton())
        {
            fade_CloudEffect.StartFadeIn();

            // フェードインが終わったら
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.FADE, REQUEST.MOVE_CAMERA);
        }
    }

    /// <summary>
    /// リクエストの返答
    /// </summary>
    private void UpdateReplayRequest()
    {
        // 交換失敗
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.EXCHANGE_FALIED))
        {
            // 交換終了時の処理
            FinalizeExchange(false);
        }

        // 交換成功時の処理
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.EXCHANGE_SUCCESS))
        {
            // 交換終了時の処理
            FinalizeExchange(true);
        }

        // 建築失敗
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.BUILDING_FALIED))
        {
            FinalizeBuilding(false);
        }

        // 建築成功
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.BUILDING_SUCCESS))
        {
            FinalizeBuilding(true);
        }

        request.ReplayFlag.Clear();
    }

    /// <summary>
    /// 交換終了時の処理
    /// </summary>
    /// <param name="isExchangable">交換できたかどうかのフラグ</param>
    public void FinalizeExchange(bool _isExchangable)
    {
        // qrウィンドウの処理
        if (qrReaderWindow.IsExchange())
        {
            qrReaderWindow.FinishExchange(_isExchangable);
        }
        // 噴水の処理
        if (fountainWindow.IsExchange())
        {
            fountainWindow.FinishExchange();
        }
        // 市場の処理
        if (marketWindow.IsExchange())
        {
            marketWindow.FinishExchange();
        }
        // QR読み込みの処理
        if (qrReaderWindow.IsExchange())
        {
            otherID = -1;
            qrReaderWindow.FinishExchange(_isExchangable);
        }
    }

    /// <summary>
    /// 建築終了の処理
    /// </summary>
    /// <param name="isBuilding"></param>
    public void FinalizeBuilding(bool isBuilding)
    {
        // 建築成功
        if (isBuilding)
        {
        }
        // 建築失敗
        else
        {
            buildingBoard.ActiveMissMessage();
        }
    }

    //リザルトに行くときのフェード
    void ResultStart()
    {
        fade_CloudEffect.StartFadeIn();
    }

    /// <summary>
    /// 建築ボードの表示・非表示
    /// </summary>
    /// <param name="isActive"></param>
    /// <param name="_items"></param>
    public void SetActiveBuildingBoard(bool isActive, List<IItem> _items)
    {
        if (isActive && _items != null)
        {
            buildingBoard.SetItems(_items);
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.BUILDIGN_BOARD);
        }
        else
        {
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.BUILDIGN_BOARD);
        }
    }

    // 交換するかどうかのフラグを取得
    public bool IsExchange(ref List<IItem> _items)
    {
        _items = exchangeItems;
        return exchangeItems.Count != 0;
    }

    // カメラの移動先をTypeで取得
    public Type GetPlaceType()
    {
        if (request.Flag.IsFlag(REQUEST.MOVE_CAMERA))
        {
            return manager_placeBar.GetchangeType();
        }
        return Type.none;
    }

    /// <summary>
    /// 交換時の相手のIDを取得する
    /// </summary>
    /// <returns></returns>
    public int GetExchangeOtherID()
    {
        return otherID;
    }

    /// <summary>
    /// フェードインが終わったタイミングを知らせる
    /// </summary>
    /// <returns></returns>
    public bool IsisFinishFadeIn()
    {
        return isFinishFadeIn;
    }
    
    /// <summary>
    /// リクエストのビットフラグを取得する
    /// </summary>
    /// <returns></returns>
    public Request GetRequest()
    {
        return request;
    }
}
