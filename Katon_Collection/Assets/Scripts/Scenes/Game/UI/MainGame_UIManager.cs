using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainGame_UIManager : MonoBehaviour
{
    /// <summary>
    /// ビットフラグの種類
    /// </summary>
    enum REQUEST_BIT_FLAG_TYPE
    {
        REQUEST,        // 即座にリクエストを飛ばす
        FADE,           // フェードインが終わったタイミングでリクエストを飛ばす

        MAX
    }

    /// <summary>
    /// BitFlagに使うリクエスト一覧
    /// </summary>
    public enum REQUEST_UI
    {
        NONE = 0,
        MOVE_CAMERA = 1 << 1,       // カメラを移動
        UNDO_CAMERA = 1 << 2,       // ひとつ前にカメラに戻る
        BUILDING    = 1 << 3,       // 建築する
        CREADED_QR  = 1 << 4,       // QRを作成したフラグ

        MAX
    }

    enum ACTIVE_BIT_FLAG_TYPE
    {
        IMMEDIATELY,    // 即座に実行
        FADE,           // フェードインが終わったタイミングで実行

        MAX
    }

    /// <summary>
    /// 有効にするUIの種類
    /// </summary>
    public enum ACTIVE_UI
    {
        PLACE_BAR   =       1 << 1,
        FOUNTAIN    =       1 << 2,
        MARKET      =       1 << 3,
        QR_READER   =       1 << 4,
        BUILDIGN_BOARD =    1 << 5,

        MAX
    }
    
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
    BitFlag[] bitRequestFlag = new BitFlag[(int)REQUEST_BIT_FLAG_TYPE.MAX];

    // UIを有効化する用のビットフラグ
    BitFlag[] bitActiveFlag = new BitFlag[(int)ACTIVE_BIT_FLAG_TYPE.MAX];

    // UIを無効化する用のビットフラグ
    BitFlag[] bitUnActiveFlag = new BitFlag[(int)ACTIVE_BIT_FLAG_TYPE.MAX];

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

        for (int i = 0; i < (int)REQUEST_BIT_FLAG_TYPE.MAX; i++)
        {
            bitRequestFlag[i] = new BitFlag();
        }

        for (int i = 0; i < (int)ACTIVE_BIT_FLAG_TYPE.MAX; i++)
        {
            bitActiveFlag[i] = new BitFlag();
            bitUnActiveFlag[i] = new BitFlag();
        }
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
        if (IsActive(ACTIVE_UI.PLACE_BAR))
        {
            manager_placeBar.Active();
        }

        // 市場を有効化
        if (IsActive(ACTIVE_UI.MARKET))
        {
            marketWindow.Active();
        }

        // 噴水を有効化
        if (IsActive(ACTIVE_UI.FOUNTAIN))
        {
            fountainWindow.Active();
        }

        // QRリーダーを有効化
        if (IsActive(ACTIVE_UI.QR_READER))
        {
            qrReaderWindow.Initialize();
        }

        // 建築ボードを有効化
        if (IsActive(ACTIVE_UI.BUILDIGN_BOARD))
        {
            buildingBoard.Active();
        }

        // フラグをすべて初期化する
        bitActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].Clear();
    }

    /// <summary>
    /// UIを無効化する処理
    /// </summary>
    void UpdateUnActive()
    {
        // 移動バーを無効化
        if (IsUnActive(ACTIVE_UI.PLACE_BAR))
        {
            manager_placeBar.UnActive();
        }

        // 市場を無効化
        if (IsUnActive(ACTIVE_UI.MARKET))
        {
            marketWindow.UnActive();
        }

        // 噴水を無効化
        if (IsUnActive(ACTIVE_UI.FOUNTAIN))
        {
            fountainWindow.UnActive();
        }

        // QRリーダーを無効化
        if (IsUnActive(ACTIVE_UI.QR_READER))
        {
        }

        // 建築ボードを無効化
        if (IsUnActive(ACTIVE_UI.BUILDIGN_BOARD))
        {
            buildingBoard.UnActive();
        }

        // フラグをすべて初期化する
        bitUnActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].Clear();
    }

    /// <summary>
    /// 建築のボードの更新処理
    /// </summary>
    void UpdateRequest_BuildingBoard()
    {
        // 建築ボタン
        if (buildingBoard.IsClickBuildingButton())
        {
            OnFlag(REQUEST_BIT_FLAG_TYPE.REQUEST, REQUEST_UI.BUILDING);
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
            ReflectionRequest(REQUEST_BIT_FLAG_TYPE.FADE);
            bitRequestFlag[(int)REQUEST_BIT_FLAG_TYPE.FADE].Clear();

            // 有効化の処理
            ReflectionActiveFlag(ACTIVE_BIT_FLAG_TYPE.FADE);
            bitActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.FADE].Clear();

            // 無効化の処理
            ReflectionUnActiveFlag(ACTIVE_BIT_FLAG_TYPE.FADE);
            bitUnActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.FADE].Clear();
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
            ActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);
            UnActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.FOUNTAIN);

            OnFlag(REQUEST_BIT_FLAG_TYPE.FADE, REQUEST_UI.UNDO_CAMERA);
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
            OnFlag(REQUEST_BIT_FLAG_TYPE.REQUEST, REQUEST_UI.CREADED_QR);
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
            ActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);
            UnActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.MARKET);

            OnFlag(REQUEST_BIT_FLAG_TYPE.FADE, REQUEST_UI.UNDO_CAMERA);
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
        // 噴水ウィンドウを表示
        if (manager_placeBar.IsActiveFountain())
        {
            ActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.FOUNTAIN);
            UnActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);
        }

        // 市場ウィンドウを表示
        if (manager_placeBar.IsActiveShop())
        {
            ActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.MARKET);
            UnActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.PLACE_BAR);
        }

        // QRリーダーを起動
        if (manager_placeBar.GetIsQRLeader())
        {
            ActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.FADE, ACTIVE_UI.QR_READER);
        }

        // 移動する
        if (manager_placeBar.IsChangeCameraPosiiton())
        {
            fade_CloudEffect.StartFadeIn();

            // フェードインが終わったら
            OnFlag(REQUEST_BIT_FLAG_TYPE.FADE, REQUEST_UI.MOVE_CAMERA);
        }
    }

    /// <summary>
    /// カメラ移動終了時の処理
    /// </summary>
    public void FinalizeMoveCamera()
    {
        OffFlag(REQUEST_BIT_FLAG_TYPE.REQUEST, REQUEST_UI.MOVE_CAMERA);
        OffFlag(REQUEST_BIT_FLAG_TYPE.REQUEST, REQUEST_UI.UNDO_CAMERA);
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
        OffFlag(REQUEST_BIT_FLAG_TYPE.REQUEST ,REQUEST_UI.BUILDING);
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
            ActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.BUILDIGN_BOARD);
        }
        else
        {
            UnActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY, ACTIVE_UI.BUILDIGN_BOARD);
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
        if (IsFlag(REQUEST_UI.MOVE_CAMERA))
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
    /// リクエストがあるかどうかの確認
    /// </summary>
    /// <param name="_request"></param>
    /// <returns></returns>
    public bool IsFlag(REQUEST_UI _request)
    {
        return bitRequestFlag[(int)REQUEST_BIT_FLAG_TYPE.REQUEST].IsFlag((int)_request);
    }

    /// <summary>
    /// フラグを立てる
    /// </summary>
    /// <param name="_request"></param>
    void OnFlag(REQUEST_BIT_FLAG_TYPE _type, REQUEST_UI _request)
    {
        bitRequestFlag[(int)_type].OnFlag((int)_request);
    }

    /// <summary>
    /// フラグを伏せる
    /// </summary>
    /// <param name="_request"></param>
    void OffFlag(REQUEST_BIT_FLAG_TYPE _type, REQUEST_UI _request)
    {
        bitRequestFlag[(int)_type].OffFlag((int)_request);
    }

    /// <summary>
    /// ビットフラグをリクエストに反映させる
    /// </summary>
    /// <param name="_type"></param>
    void ReflectionRequest(REQUEST_BIT_FLAG_TYPE _type)
    {
        int bufBitFlag = bitRequestFlag[(int)_type].GetBitFlag();
        bitRequestFlag[(int)REQUEST_BIT_FLAG_TYPE.REQUEST].OnFlag(bufBitFlag);
    }


    /// <summary>
    /// フェードインが終わったタイミングを知らせる
    /// </summary>
    /// <returns></returns>
    bool IsisFinishFadeIn()
    {
        return isFinishFadeIn;
    }

    /// <summary>
    /// 有効化するUIのビットフラグを立てる
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_ui"></param>
    void ActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE _type, ACTIVE_UI _ui)
    {
        bitActiveFlag[(int)_type].OnFlag((int)_ui);
    }

    /// <summary>
    /// 有効化するUIのビットフラグを伏せる
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_ui"></param>
    void ActiveUIOffFlag(ACTIVE_BIT_FLAG_TYPE _type, ACTIVE_UI _ui)
    {
        bitActiveFlag[(int)_type].OffFlag((int)_ui);
    }

    /// <summary>
    /// 無効化するUIのビットフラグを立てる
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_ui"></param>
    void UnActiveUIOnFlag(ACTIVE_BIT_FLAG_TYPE _type, ACTIVE_UI _ui)
    {
        bitUnActiveFlag[(int)_type].OnFlag((int)_ui);
    }

    /// <summary>
    /// 無効化するUIのビットフラグを伏せる
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_ui"></param>
    void UnActiveUIOffFlag(ACTIVE_BIT_FLAG_TYPE _type, ACTIVE_UI _ui)
    {
        bitUnActiveFlag[(int)_type].OffFlag((int)_ui);
    }

    /// <summary>
    /// 有効化するかどうかを取得する
    /// </summary>
    /// <param name="_ui"></param>
    /// <returns></returns>
    bool IsActive(ACTIVE_UI _ui)
    {
        return bitActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].IsFlag((int)_ui);
    }


    /// <summary>
    /// 無効化するかどうかを取得する
    /// </summary>
    /// <param name="_ui"></param>
    /// <returns></returns>
    bool IsUnActive(ACTIVE_UI _ui)
    {
        return bitUnActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].IsFlag((int)_ui);
    }

    /// <summary>
    /// ビットフラグをリクエストに反映させる
    /// </summary>
    /// <param name="_type"></param>
    void ReflectionActiveFlag(ACTIVE_BIT_FLAG_TYPE _type)
    {
        int bufBitFlag = bitActiveFlag[(int)_type].GetBitFlag();
        bitActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].OnFlag((int)bufBitFlag);
    }


    /// <summary>
    /// ビットフラグをリクエストに反映させる
    /// </summary>
    /// <param name="_type"></param>
    void ReflectionUnActiveFlag(ACTIVE_BIT_FLAG_TYPE _type)
    {
        int bufBitFlag = bitUnActiveFlag[(int)_type].GetBitFlag();
        bitUnActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].OnFlag(bufBitFlag);
    }
}
