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
    
    // Start is called before the first frame update
    void Start()
    {
        manager_item.Initialize();
        owner_human.Intialize();
        owner_floor.Initialize();

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

        // カメラの動きの更新処理
        UpdateMoveCamera();

        // マンションを建てる更新処理
        UpdateBuildingFloor();
    }

    void UpdateMoveCamera()
    {
        Debug.Log(uiManager.GetPlaceType());
        mainCamera.Move(uiManager.GetPlaceType());
        if (uiManager.IsUndoCamera())
        {
            Debug.Log("undo");
            mainCamera.Undo();
        }
    }


    /// <summary>
    /// サーバー関係の更新処理
    /// </summary>
    void UpdateServer()
    {
        if (uiManager.IsCreateQR())
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
    /// マンションを建てる更新処理
    /// </summary>
    void UpdateBuildingFloor()
    {
        bool isActive = false;
        Type placeType = Type.none;
        List<IItem> _items = new List<IItem>();

        foreach (SignBoard board in owner_signBoard.GetSignBoards())
        {
            // 表示させるか判定
            if (mainCamera.IsSigneBoardInScreen(board.transform.position))
            {
                // 建築に必要な素材を取得
                _items = owner_floor.GetBuildingResource(board.GetPlaceType());
                placeType = board.GetPlaceType();
                isActive = true;
                break;
            }
        }
        uiManager.SetActiveBuildingBoard(isActive, _items);

        // 建築ボタンが押された
        if (uiManager.IsBuilding())
        {
            if (_items.Count == 0)
            {
                return;
            }

            // 建築可能か判定
            if (manager_item.IsExchange(owner_floor.GetBuildingResource(placeType)))
            {
                // 資源の消費
                manager_item.AddItems(owner_floor.GetBuildingResource(placeType));
                // 建築
                owner_floor.Building(placeType);

                Debug.Log("建築");
                uiManager.Building(true);
            }
            else
            {
                uiManager.Building(false);
            }
        }
    }

    /// <summary>
    /// 交換の更新処理
    /// </summary>
    void UpdateExchange()
    {
        // UIの交換処理
        if (uiManager.IsExchange())
        {
            // アイテムのマネージャに追加・削除
            bool isExchangable = manager_item.AddItems(uiManager.GetExchangeItems());


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
