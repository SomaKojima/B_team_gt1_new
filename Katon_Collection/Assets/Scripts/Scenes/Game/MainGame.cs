using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // QR読み込みウィンドウ
    [SerializeField]
    QR_ReaderWindow qrReaderWindow;

    // 拠点移動バー
    [SerializeField]
    Manager_PlaceBar manager_placeBar;

    // 所持アイテム管理
    [SerializeField]
    Manager_Item manager_item;

    // 実体の人間を管理
    [SerializeField]
    Owner_Human owner_human;

    // マンションを管理
    [SerializeField]
    Owner_Floor owner_floor;

    // 看板を管理（現在は非表示、建築ボードの表示する場所を特定するのに使用）
    [SerializeField]
    Owner_SignBoard owner_signBoard;

    // 建築ボード（建築に必要な素材表示、建築ボタン）
    [SerializeField]
    BuildingBoard buildingBoard;

    // フェード
    [SerializeField]
    Fade_CloudEffect fade_CloudEffect = null;

    // サーバー・プレイヤー関係
    [SerializeField]
    Manage_SI_Player manager_SI_Player;

    // 噴水のウィンドウ
    [SerializeField]
    FountainWindow fountainWindow;

    // 市場のウィンドウ
    [SerializeField]
    MarketWindow marketWindow;

    // カメラ
    [SerializeField]
    MainCamera mainCamera;

    // 所持アイテムウィンドウ
    [SerializeField]
    PossessListManager possessListManager;

    //フェード　
    bool m_fade = false;

    //リザルトに移行したら
    bool m_switching = false;

    // Start is called before the first frame update
    void Start()
    {
        manager_item.Initialize();
        owner_human.Intialize();
        fountainWindow.Initialize(manager_item);
        marketWindow.Initialize(manager_item);
        owner_floor.Initialize();
        buildingBoard.Initialize();

        //manager_item.GetItem(ITEM_TYPE.LOOGER).SetCount(2);
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            manager_item.GetItem(type).SetCount(10);
        }
        manager_SI_Player.UpdatePlayers();

        possessListManager.Initialize();
    }



    // Update is called once per frame
    void Update()
    {
        // カメラの動きの更新処理
        UpdateMoveCamera();



        if (manager_placeBar.IsActiveFountain())
        {
            marketWindow.UnActive();
            fountainWindow.Active();
        }

        if (manager_placeBar.IsActiveShop())
        {
            marketWindow.Active();
            fountainWindow.UnActive();
        }

        // フェードの更新処理
        UpdateFade();

        // QRリーダーの更新処理
        UpdateQRReader();

        // 建築のボードの更新処理
        UpdateBuildingBoard();

        // アイテムのマネージャと人間の数を合わせる
        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            owner_human.MatchItemsHumans(manager_item.GetItem(type), false);
        }

        // サーバー関係の更新処理
        UpdateServer();
        
        if (fountainWindow.IsExchange())
        {
            // アイテムのマネージャに追加・削除
            Exchange(qrReaderWindow.GetItems());
            fountainWindow.FinishExchange();
        }

        if (marketWindow.IsExchange())
        {
            // 交換の処理
            Exchange(marketWindow.GetExchangeItemList());
            marketWindow.FinishExchange();
        }
    }

    

    //リザルトに行くときのフェード
    void ResultStart()
    {
        m_switching = true;
        StartCoroutine(fade_CloudEffect.FadeIn());
    }
    
    /// <summary>
    /// 交換時の処理
    /// </summary>
    /// <param name="_items"></param>
    void Exchange(List<IItem> _items)
    {
        // アイテムのマネージャに追加・削除
        foreach (IItem item in _items)
        {
            manager_item.GetItem(item.GetItemType()).AddCount(item.GetCount());
        }
    }

    /// <summary>
    /// 交換可能か判定
    /// </summary>
    /// <returns></returns>
    bool IsExchange(List<IItem> _items)
    {
        // 交換可能か判定
        foreach (IItem item in _items)
        {
            IItem myItem = manager_item.GetItem(item.GetItemType());

            // 足りない
            if (myItem.GetCount() + item.GetCount() < 0)
            {
                return false;
            }
        }
        return true;
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

        // QR読み込み完了
        bool isExchangable = false;
        if (qrReaderWindow.IsExchange())
        {
            isExchangable = IsExchange(qrReaderWindow.GetItems());

            // qrウィンドウの交換終了時の処理
            qrReaderWindow.FinishExchange(isExchangable);
        }

        // 交換処理
        if (isExchangable)
        {
            Exchange(qrReaderWindow.GetItems());

            // 交換終了したことを相手に伝える
            for (int i = 0; i < manager_SI_Player.GetPlayers().Count; i++)
            {
                if (qrReaderWindow.GetOtherID() == manager_SI_Player.GetPlayer(i).ID)
                {
                    manager_SI_Player.GetPlayer(i).IsExcange = false;
                }
            }
        }
    }


    /// <summary>
    /// 建築のボードの更新処理
    /// </summary>
    void UpdateBuildingBoard()
    {
        // 表示させる
        bool isActive = false;
        Type placeType = Type.none;
        foreach (SignBoard board in owner_signBoard.GetSignBoards())
        {
            // 表示させるか判定
            if (mainCamera.IsSigneBoardInScreen(board.transform.position))
            {
                placeType = board.GetPlaceType();
                buildingBoard.Active(owner_floor.GetBuildingResource(placeType));
                isActive = true;
                break;
            }
        }

        // 非表示にさせる
        if (!isActive)
        {
            buildingBoard.UnActive();
        }

        // 建築ボタン
        if (buildingBoard.IsClickBuildingButton())
        {
            Debug.Log(placeType.ToString());
            if (owner_floor.GetBuildingResource(placeType) != null)
            {
                // 建築可能か判定
                if (IsExchange(owner_floor.GetBuildingResource(placeType)))
                {
                    // 資源の消費
                    Exchange(owner_floor.GetBuildingResource(placeType));
                    // 建築
                    owner_floor.Building(placeType);

                    Debug.Log("建築");
                }
                else
                {
                    buildingBoard.ActiveMissMessage();
                }
            }
        }
    }

    /// <summary>
    /// カメラの動きの更新処理
    /// </summary>
    void UpdateMoveCamera()
    { 
        // カメラを移動
        if (manager_placeBar.IsChangeCameraPosiiton())
        {
            m_fade = true;

            mainCamera.Move(manager_placeBar.GetchangeType());

            m_switching = false;
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
    /// サーバー関係の更新処理
    /// </summary>
    void UpdateServer()
    {
        if (fountainWindow.IsCreateQR())
        {
            for (int i = 0; i < manager_SI_Player.GetPlayers().Count; i++)
            {
                if (PhotonNetwork.player.ID == manager_SI_Player.GetPlayer(i).ID)
                {
                    manager_SI_Player.GetPlayer(i).IsExcange = true;
                }
            }
        }
    }

    /// <summary>
    /// アイテムの変更をserverに伝える
    /// </summary>
    /// <param name="Count"></param>
    /// <param name="ItemType"></param>
    private void ChangedItem(int Count, int ItemType)
    {
        for (int i = 0; i < manager_SI_Player.GetPlayers().Count; i++)
        {
            if (PhotonNetwork.player.ID == manager_SI_Player.GetPlayer(i).ID)
            {
                manager_SI_Player.GetPlayer(i).SetItemCount(Count, ItemType);
            }
        }
    }
}
