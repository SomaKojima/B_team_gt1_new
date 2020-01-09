using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
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
    
    // サーバー・プレイヤー関係
    [SerializeField]
    Manage_SI_Player manager_SI_Player;
    
    // カメラ
    [SerializeField]
    MainCamera mainCamera;

    // UIを管理
    [SerializeField]
    MainGame_UIManager uiManager;
    
    // 現在地
    Type currentPlaceType = Type.none;
    
    // Start is called before the first frame update
    void Start()
    {
        manager_item.Initialize();
        owner_human.Intialize();
        owner_floor.Initialize();
        owner_signBoard.Initialize(mainCamera.IsSigneBoardInScreen);

        //manager_item.GetItem(ITEM_TYPE.LOOGER).SetCount(2);
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            manager_item.GetItem(type).SetCount(10);
        }
        manager_SI_Player.UpdatePlayers();

        uiManager.Initialize(manager_item);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        // アイテムのマネージャと人間の数を合わせる
        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            owner_human.MatchItemsHumans(manager_item.GetItem(type), false);
        }

        // サーバー関係の更新処理
        UpdateServer();

        // 交換の更新処理
        UpdateExchange();
        
        // マンションのリクエスト処理
        UpdateRequest_SignBoard();

        // UIマネージャーのリクエスト処理
        UpdateRequest_UIManager();
    }


    /// <summary>
    /// サーバー関係の更新処理
    /// </summary>
    void UpdateServer()
    {
        if (uiManager.IsFlag(MainGame_UIManager.REQUEST_UI.CREADED_QR))
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

    /// <summary>
    /// 看板のリクエスト処理
    /// </summary>
    void UpdateRequest_SignBoard()
    {
        if (owner_signBoard.IsActiveBoard())
        {
            currentPlaceType = owner_signBoard.GetVisiblePlaceType();

            // 建築に必要な素材を取得
            List<IItem> _items = owner_floor.GetBuildingResource(currentPlaceType);
            //UIの建築ボードを表示する
            uiManager.SetActiveBuildingBoard(true, _items);
        }
        else
        {
            //UIの建築ボードを非表示する
            uiManager.SetActiveBuildingBoard(false, null);
        }
    }
    

    /// <summary>
    /// UIマネージャーのリクエスト処理
    /// </summary>
    void UpdateRequest_UIManager()
    {
        // 建築ボタンが押された
        if (uiManager.IsFlag(MainGame_UIManager.REQUEST_UI.BUILDING))
        {
            // 建築に必要な素材を取得
            List<IItem> _items = owner_floor.GetBuildingResource(currentPlaceType);
            // 資源が足りているか確認
            bool _isExchange = manager_item.IsExchange(_items);
            if (_isExchange)
            {
                // 資源の消費
                manager_item.AddItems(owner_floor.GetBuildingResource(currentPlaceType));
                // 建築
                owner_floor.Building(currentPlaceType);
            }

            // 建築終了後のUIの処理
            uiManager.FinalizeBuilding(_isExchange);
        }

        // カメラの移動
        if (uiManager.IsFlag(MainGame_UIManager.REQUEST_UI.MOVE_CAMERA))
        {
            mainCamera.Move(uiManager.GetPlaceType());
        }
        
        // カメラをひとつ前に戻す
        if (uiManager.IsFlag(MainGame_UIManager.REQUEST_UI.UNDO_CAMERA))
        {
            mainCamera.Undo();
        }
    }

    /// <summary>
    /// 交換の更新処理
    /// </summary>
    void UpdateExchange()
    {
        // UIの交換処理
        List<IItem> bufItems = new List<IItem>();
        if (uiManager.IsExchange(ref bufItems))
        {
            // アイテムのマネージャに追加・削除
            bool isExchangable = manager_item.AddItems(bufItems);


            // 交換終了したことを相手に伝える
            if (isExchangable && uiManager.GetExchangeOtherID() >= 0)
            {
                for (int i = 0; i < manager_SI_Player.GetPlayers().Count; i++)
                {
                    if (uiManager.GetExchangeOtherID() == manager_SI_Player.GetPlayer(i).ID)
                    {
                        manager_SI_Player.GetPlayer(i).IsExcange = false;
                    }
                }
            }

            // 交換終了時の処理
            uiManager.FinalizeExchange(isExchangable);
        }
    }
}
