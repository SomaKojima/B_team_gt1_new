using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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

    // ログのウィンドウ
    [SerializeField]
    UI_LogWindow logWindow;

    // タイマー
    [SerializeField]
    UI_Time timer;
    
    [SerializeField]
    Sound_MainGame sound;

    // 最初の文字
    [SerializeField]
    FirstText firstText;

    // 人間関係
    [SerializeField]
    HumanWindow humanWindow;

    [SerializeField]
    SelectQrReaderOrFountain selectQrReaderOrFountain;

    [SerializeField]
    GameObject infoBtn;

    // リクエスト用のビットフラグ
    Request request = new Request();

    // UIのアクティブを管理するビットフラグ
    RequestActiveUI requestActiveUI = new RequestActiveUI();

    // 交換相手のID
    int otherID = -1;

    // フェードインが終了したかどうかのフラグ
    bool isFinishFadeIn = false;

    /// <summary>
    /// 現在の場所の人間の情報が欲しい
    /// </summary>
    bool isGetPlaceHuman = false;
    List<ITEM_TYPE> humaPlaceType = new List<ITEM_TYPE>();

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="_managerItem"></param>
    public void Initialize(Manager_Item _managerItem)
    {
        timer.Initialize();

        fountainWindow.Initialize(_managerItem);
        marketWindow.Initialize(_managerItem, timer.GetCountTimer());
        buildingBoard.Initialize();
        possessListManager.Initialize();
        fade_CloudEffect.StartFadeOut();


        request.Initialize();
        requestActiveUI.Initailize();

        request.Initialize();

        humanWindow.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        // 返答のリクエスト処理
        UpdateReplayRequest();
        
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

        // ログウィンドウのリクエスト処理
        UpdateRequest_LogWindow();

        UpdateRequest_HumanWindow();

        UpdateRequest_selectQrReaderOrFountain();

        UpdateRequest_Timer();

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
            infoBtn.SetActive(true);
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

        // ｑｒの選択
        if (requestActiveUI.IsActive(ACTIVE_UI.SELECT_QR))
        {
            selectQrReaderOrFountain.Active();
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

        // 最初の文字を有効か
        if (requestActiveUI.IsActive(ACTIVE_UI.FIRST_TEXT))
        {
            firstText.Active();
        }

        // 情報ウィンドウの有効化
        if (requestActiveUI.IsActive(ACTIVE_UI.INFO_WINDOW))
        {
            infoBtn.SetActive(true);
        }

        // 人間ウィンドウの有効化
        if (requestActiveUI.IsActive(ACTIVE_UI.HUMAN_WINDOW))
        {
            humanWindow.Active();
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.GET_CURRENT_PLACE_HUMAN_INFO);
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
            infoBtn.SetActive(false);
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

        // ｑｒの選択
        if (requestActiveUI.IsUnActive(ACTIVE_UI.SELECT_QR))
        {
            selectQrReaderOrFountain.UnActive();
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

        // 最初の文字を無効化
        if (requestActiveUI.IsUnActive(ACTIVE_UI.FIRST_TEXT))
        {
            firstText.UnActive();
        }

        // 情報ウィンドウの無効化
        if (requestActiveUI.IsUnActive(ACTIVE_UI.INFO_WINDOW))
        {
            infoBtn.SetActive(false);
        }

        // 人間ウィンドウの無効化
        if (requestActiveUI.IsUnActive(ACTIVE_UI.HUMAN_WINDOW))
        {
            humanWindow.UnActive();
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
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.FIRST_TEXT);
        }

        // 資源のボードを表示中かどうか
        //if (buildingBoard.IsActiveBoard())
        //{
        //    requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.FIRST_TEXT);
        //}
        //else
        //{
        //    requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.FIRST_TEXT);
        //}
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
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.EXCHANGE);
            request.ExchangeItems = qrReaderWindow.GetItems();

            Debug.Log(qrReaderWindow.GetOtherID());
            otherID = qrReaderWindow.GetOtherID();
        }

        // 読み込んだ
        if (qrReaderWindow.IsReader())
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.QR_READE);
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

            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_START);
            
            //fade_CloudEffect.StartFadeIn();
        }
        
        // 交換
        if (fountainWindow.IsExchange())
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.EXCHANGE | REQUEST.CAMERA_START);
            request.ExchangeItems = fountainWindow.GetItems();
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

            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.FADE, REQUEST.CAMERA_UNDO | REQUEST.CAMERA_START);

            fade_CloudEffect.StartFadeIn();
        }

        // 交換
        if (marketWindow.IsExchange())
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.EXCHANGE);
            request.ExchangeItems = marketWindow.GetExchangeItemList();
        }

        marketWindow.UpdateUnits(timer.GetCountTimer());
    }

    /// <summary>
    /// 移動バーのリクエスト
    /// </summary>
    private void UpdateRequest_PlaceBar()
    {
        // 人間のボタンが押された
        if (manager_placeBar.IsClickHuman())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.HUMAN_WINDOW);
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_STOP);
        }

        // 市場ウィンドウを表示
        if (manager_placeBar.IsClickMarket())
        {
            fade_CloudEffect.StartFadeIn();
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.MARKET);
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);

            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.FADE, REQUEST.CAMERA_STOP | REQUEST.CAMERA_MOVE_PLACE);
            request.ChangeCameraPlaceType = Type.market;
        }

        // QRリーダーを起動
        if (manager_placeBar.IsClickCamera())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.SELECT_QR);
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_STOP);
        }
    }

    /// <summary>
    /// ログウィンドウのリクエスト処理
    /// </summary>
    private void UpdateRequest_LogWindow()
    {
        // スクロール中
        if (logWindow.IsClick())
        {
            // カメラ停止
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_STOP);
        }

        // スクロールをやめた
        if (logWindow.IsNotClick())
        {
            // カメラ停止
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_START);
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
            AddLog("こうかん　\nしっぱい");
        }

        // 交換成功時の処理
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.EXCHANGE_SUCCESS))
        {
            // 交換終了時の処理
            FinalizeExchange(true);
            AddLog("こうかん　\nせいこう");
        }

        // 建築失敗
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.BUILDING_FALIED))
        {
            FinalizeBuilding(false);
            AddLog("けんちく　\nしっぱい");
        }

        // 建築成功
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.BUILDING_SUCCESS))
        {
            FinalizeBuilding(true);
            AddLog("けんちく　\nせいこう");
        }

        // 強化成功
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.POWER_UP_SUCCESS))
        {
            humanWindow.OnCorrectPowerUp();
        }

        // 人間の情報を取得成功
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.GET_CURRENT_PLACE_HUMAN_INFO_SUCCESS))
        {
            humanWindow.OnGetCurrentPlaceHumanInfo(request.CurrentPlaceHumanType);
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
            marketWindow.FinishExchange(_isExchangable);
        }
        // QR読み込みの処理
        if (qrReaderWindow.IsExchange())
        {
            otherID = -1;
            qrReaderWindow.FinishExchange(_isExchangable);
        }
    }

    /// <summary>
    /// 強化画面のリクエスト処理
    /// </summary>
    void UpdateRequest_HumanWindow()
    {
        // 強化の処理
        if (humanWindow.IsPowerUp())
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.POWER_UP_HUMAN);
            request.PowerUpHumanType = humanWindow.GetPowerUpItemType();
            request.PowerUpItems = humanWindow.GetPowerUpResource();
        }

        // 雇用の処理
        if(humanWindow.IsEmployment())
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.EMPLOYMENT);
            request.EmploymentItems = humanWindow.GetEmploymentResource();
        }

        // 戻るボタンの処理
        if (humanWindow.IsBack())
        {
            humanWindow.UnActive();
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_START);
        }
    }

    /// <summary>
    /// 制限時間のリクエスト処理
    /// </summary>
    public void UpdateRequest_Timer()
    {
        // 時間を教える
        marketWindow.SetTime(timer.GetCountTimer());
    }
    
    /// <summary>
    /// QRリーダーと噴水の選択画面のrequest処理
    /// </summary>
    public void UpdateRequest_selectQrReaderOrFountain()
    {
        if (selectQrReaderOrFountain.IsSelectFountain())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.FOUNTAIN);
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.SELECT_QR);

            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_STOP);
        }

        if (selectQrReaderOrFountain.IsSelectQrReader())
        {
            requestActiveUI.Active_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.QR_READER);
            requestActiveUI.UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.SELECT_QR);
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_STOP);
        }

        if (selectQrReaderOrFountain.IsBack())
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_START);
        }
    }

    /// <summary>
    /// 建築終了の処理
    /// </summary>
    /// <param name="isBuilding"></param>
    void FinalizeBuilding(bool isBuilding)
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

    /// <summary>
    /// 建築時の更新処理
    /// </summary>
    public void UpdateBuilding(int buildingTotal)
    {
        marketWindow.UpdateBuilding(buildingTotal);
        firstText.SetFirstBuilding(true);
        humanWindow.OnBuilding(buildingTotal);
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

    public void SetPlaceHumanType(List<ITEM_TYPE> _humaType)
    {
        
    }

    public void AddLog(string _text)
    {
        logWindow.AddLog(_text, timer.GetTotalTime());
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

    public bool IsGetPlaceHumanType()
    {
        return isGetPlaceHuman;
    }

    public bool IsStartFade()
    {
        return fade_CloudEffect.IsStartProcess;
    }
}
