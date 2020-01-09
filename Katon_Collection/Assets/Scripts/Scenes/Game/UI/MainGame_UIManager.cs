using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    //フェード　
    bool m_fade = false;

    //リザルトに移行したら
    bool m_switching = false;

    // 交換するかどうかのフラグ
    bool isExchange = false;

    // 交換処理をするときに使うアイテムリスト
    List<IItem> exchangeItems = new List<IItem>();

    // 建築するかどうかのフラグ
    bool isBuilding = false;

    // 交換相手のID
    int otherID = -1;

    // ひとつ前のカメラに戻すかどうかのフラグ
    bool isUndoCamera = false;

    public void Initialize(Manager_Item _managerItem)
    {
        fountainWindow.Initialize(_managerItem);
        marketWindow.Initialize(_managerItem);
        buildingBoard.Initialize();
        possessListManager.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // 初期化
        isUndoCamera = false;
        isExchange = false;
        exchangeItems.Clear();



        // フェードの更新処理
        UpdateFade();

        // QRリーダーの更新処理
        UpdateQRReader();

        // 建築のボードの更新処理
        UpdateBuildingBoard();

        // 交換するかどうかを判定する処理
        UpdateRequest_QRReader();

        // 市場のリクエスト処理
        UpdateRequest_Market();

        // 噴水のリクエスト処理
        UpdateRequest_Fountain();

        // 移動バーの更新処理
        UpdateRequest_PlaceBar();
    }

    //リザルトに行くときのフェード
    void ResultStart()
    {
        m_switching = true;
        StartCoroutine(fade_CloudEffect.FadeIn());
    }


    /// <summary>
    /// QRリーダーの更新処理
    /// </summary>
    void UpdateQRReader()
    {
        // QRリーダーを起動
        if (manager_placeBar.GetIsQRLeader())
        {
            qrReaderWindow.Initialize();
            fountainWindow.UnActive();
            marketWindow.UnActive();
        }
    }

    /// <summary>
    /// 建築ボードの表示・非表示
    /// </summary>
    /// <param name="isActive"></param>
    /// <param name="_items"></param>
    public void SetActiveBuildingBoard(bool isActive, List<IItem> _items)
    {
        if (isActive)
        {
            buildingBoard.Active(_items);
        }
        else
        {
            buildingBoard.UnActive();
        }
    }

    /// <summary>
    /// 建築のボードの更新処理
    /// </summary>
    void UpdateBuildingBoard()
    {
        // 建築ボタン
        isBuilding = false;
        if (buildingBoard.IsClickBuildingButton())
        {
            isBuilding = true;
        }
    }

    /// <summary>
    /// フェードの更新処理
    /// </summary>
    void UpdateFade()
    {
        if (!m_switching)
        {
            if (m_fade)
            {
                StartCoroutine(fade_CloudEffect.FadeIn());


                if (!fade_CloudEffect.GetIsProcess)
                {
                    m_fade = false;
                }
            }
            else
            {
                //フェードアウトの処理
                StartCoroutine(fade_CloudEffect.FadeOut());
            }
        }
    }

    /// <summary>
    /// 交換処理のアイテムを取得
    /// </summary>
    /// <returns></returns>
    public List<IItem> GetExchangeItems()
    {
        return exchangeItems;
    }

    // 交換するかどうかのフラグを取得
    public bool IsExchange()
    {
        return isExchange;
    }
    
    /// <summary>
    /// QRリーダーのリクエスト処理
    /// </summary>
    void UpdateRequest_QRReader()
    {
        // qr読み込みの交換処理
        if (qrReaderWindow.IsExchange())
        {
            isExchange = true;
            exchangeItems = qrReaderWindow.GetItems();
            otherID = qrReaderWindow.GetOtherID();
        }
    }

    // カメラの移動先をTypeで取得
    public Type GetPlaceType()
    {
        if (manager_placeBar.IsChangeCameraPosiiton())
        {
            m_fade = true;
            m_switching = false;
            return manager_placeBar.GetchangeType();
        }

        return Type.none;
    }

    // QRを生成したかどうかのフラグを取得
    public bool IsCreateQR()
    {
        return fountainWindow.IsCreateQR();
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
    /// 建築するかどうかのフラグを取得
    /// </summary>
    /// <returns></returns>
    public bool IsBuilding()
    {
        return isBuilding;
    }

    // 建築時の処理
    public void Building(bool isBuilding)
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
    /// 交換時の相手のIDを取得する
    /// </summary>
    /// <returns></returns>
    public int GetExchangeOtherID()
    {
        return otherID;
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
            isUndoCamera = true;
            m_fade = true;
        }

        // 交換
        if (fountainWindow.IsExchange())
        {
            isExchange = true;
            exchangeItems = qrReaderWindow.GetItems();
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
            isUndoCamera = true;
            m_fade = true;
        }

        // 交換
        if (marketWindow.IsExchange())
        {
            isExchange = true;
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
    }

    /// <summary>
    /// カメラの位置をひとつ前にもどすかどうかのフラグを取得
    /// </summary>
    /// <returns></returns>
    public bool IsUndoCamera()
    {
        return isUndoCamera;
    }
}
