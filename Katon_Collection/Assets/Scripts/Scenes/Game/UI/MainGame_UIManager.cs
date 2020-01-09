using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame_UIManager : MonoBehaviour
{
    /// <summary>
    /// BitFlagに使うリクエスト一覧
    /// </summary>
    public enum REQUEST_UI
    {
        NONE = 0,
        MOVE_CAMERA,    // カメラを移動
        UNDO_CAMERA,    // ひとつ前にカメラに戻る
        BUILDING,       // 建築する
        CREADED_QR      // QRを作成したフラグ
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

    BitFlag bitFlag = new BitFlag();

    // 交換処理をするときに使うアイテムリスト
    List<IItem> exchangeItems = new List<IItem>();

    // 交換相手のID
    int otherID = -1;


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
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // 初期化
        bitFlag.Clear();
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
    }


    /// <summary>
    /// 建築のボードの更新処理
    /// </summary>
    void UpdateRequest_BuildingBoard()
    {
        // 建築ボタン
        if (buildingBoard.IsClickBuildingButton())
        {
            OnFlag(REQUEST_UI.BUILDING);
        }
    }

    /// <summary>
    /// フェードのリクエスト処理
    /// </summary>
    void UpdateRequest_Fade()
    {
        //　フェードインが終わったらフェードアウトに移る
        if (fade_CloudEffect.GetIsProcess)
        {
            Debug.Log("out");
            //フェードアウトの処理
            fade_CloudEffect.StartFadeOut();
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
            manager_placeBar.Active();
            OnFlag(REQUEST_UI.UNDO_CAMERA);
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
            OnFlag(REQUEST_UI.CREADED_QR);
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
            manager_placeBar.Active();
            OnFlag(REQUEST_UI.UNDO_CAMERA);
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
            manager_placeBar.UnActive();
            marketWindow.UnActive();
            fountainWindow.Active();
        }

        // 市場ウィンドウを表示
        if (manager_placeBar.IsActiveShop())
        {
            manager_placeBar.UnActive();
            marketWindow.Active();
            fountainWindow.UnActive();
        }

        // QRリーダーを起動
        if (manager_placeBar.GetIsQRLeader())
        {
            qrReaderWindow.Initialize();
            fountainWindow.UnActive();
            marketWindow.UnActive();
        }

        // 移動する
        if (manager_placeBar.IsChangeCameraPosiiton())
        {
            fade_CloudEffect.StartFadeIn();
            OnFlag(REQUEST_UI.MOVE_CAMERA);
        }
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
            buildingBoard.Active(_items);
        }
        else
        {
            buildingBoard.UnActive();
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
        if (manager_placeBar.IsChangeCameraPosiiton())
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
        return bitFlag.IsFlag((int)_request);
    }

    /// <summary>
    /// フラグを立てる
    /// </summary>
    /// <param name="_request"></param>
    void OnFlag(REQUEST_UI _request)
    {
        bitFlag.OnFlag((int)_request);
    }

    /// <summary>
    /// フラグを伏せる
    /// </summary>
    /// <param name="_request"></param>
    void OffFlag(REQUEST_UI _request)
    {
        bitFlag.OnFlag((int)_request);
    }
}
