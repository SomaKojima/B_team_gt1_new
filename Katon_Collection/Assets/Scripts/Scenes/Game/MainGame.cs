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

    [SerializeField]
    Owner_BuildingResource owner_buildingResource;

    [SerializeField]
    JudgeField judgeField;

    // サウンド
    [SerializeField]
    Sound_MainGame sound;

    Debug_MainGame debug = new Debug_MainGame();
    
    // 現在地
    Type currentPlaceType = Type.cave;
    
    // Start is called before the first frame update
    void Start()
    {
        // BGMを鳴らす
        sound.PlaySound(SoundType_MainGame.BGM);

        manager_item.Initialize();
        owner_human.Intialize();
        owner_floor.Initialize();
        owner_signBoard.Initialize(mainCamera.IsSigneBoardInScreen, owner_human.GetPlaceCount, owner_floor.GetPlaceTotalFloor);

        manager_SI_Player.UpdatePlayers();

        uiManager.Initialize(manager_item);

        owner_buildingResource.Initialize();

        // カメラの初期位置
        mainCamera.Move(Type.cave);

        debug.Initialize(manager_item);
    }
    
    // Update is called once per frame
    void Update()
    {
        debug.Update();

        if(Input.GetMouseButtonDown(0))
        {
            // クリック音を鳴らす
            sound.PlaySound(SoundType_MainGame.Click);
        }

        // アイテムのマネージャと人間の数を合わせる
        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            owner_human.MatchItemsHumans(manager_item.GetItem(type), currentPlaceType);
        }
        
        // マンションのリクエスト処理
        UpdateRequest_SignBoard();
        
        // リクエストの処理
        UpdateRequestList();
        
        UpdateRequest(debug.GetRequest());

        UpdateUIRequest();

        UpdateServer();
    }

    void UpdateRequest_UI()
    {
        // フェードイン終了時
        if (uiManager.IsisFinishFadeIn())
        {
            foreach (Request r in owner_human.GetRequests())
            {
                r.Flag.Reflection(REQUEST_BIT_FLAG_TYPE.FADE);
            }
            uiManager.GetRequest().Flag.Reflection(REQUEST_BIT_FLAG_TYPE.FADE);
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
            //uiManager.SetActivePowerUpWindow(true);
        }
        else
        {
            //UIの建築ボードを非表示する
            uiManager.SetActiveBuildingBoard(false, null);
            //uiManager.SetActivePowerUpWindow(false);
        }
    }
    
    /// <summary>
    /// リクエストの処理
    /// </summary>
    void UpdateRequestList()
    {
        UpdateRequest(uiManager.GetRequest());
        foreach (Request _request in owner_human.GetRequests())
        {
            UpdateRequest(_request);
        }

        UpdateRequest(owner_human.GetRequest());
    }

    /// <summary>
    /// リクエストの処理
    /// </summary>
    void UpdateRequest(Request _request)
    {
        // 建築ボタンが押された
        if (_request.Flag.IsFlag(REQUEST.BUILDING))
        {
            // 建築に必要な素材を取得
            List<IItem> _items = owner_floor.GetBuildingResource(currentPlaceType);
            // 資源が足りているか確認
            bool _isExchange = manager_item.IsExchange(_items);

            // 交換成功
            if (_isExchange)
            {
                // 建築音
                sound.PlaySound(SoundType_MainGame.Bulid);

                // 建築時に初期資源を手に入れる
                if (!owner_floor.IsFirstBuilding())
                {
                    FirstGetResource();
                }
                // 資源の消費
                manager_item.AddItems(_items);
                // 建築
                owner_floor.Building(currentPlaceType);

                uiManager.UpdateBuilding(owner_floor.GetTotalFloor());
            }
            else
            {
                // 交換失敗時の処理
                sound.PlaySound(SoundType_MainGame.Error);
            }


            // 建築終了後の処理
            _request.FinalizeBuilding(_isExchange);
        }

        // カメラの移動
        if (_request.Flag.IsFlag(REQUEST.CAMERA_MOVE_PLACE))
        {
            mainCamera.Move(uiManager.GetPlaceType());
        }

        // カメラをひとつ前に戻す
        if (_request.Flag.IsFlag(REQUEST.CAMERA_UNDO))
        {
            mainCamera.Undo();
        }

        // カメラを止める
        if (_request.Flag.IsFlag(REQUEST.CAMERA_STOP))
        {
            mainCamera.StopMove();
        }

        // カメラの動きを再開する
        if (_request.Flag.IsFlag(REQUEST.CAMERA_START))
        {
            mainCamera.StartMove();
        }

        // カメラの動きをscrollに変える
        if (_request.Flag.IsFlag(REQUEST.CAMERA_SCROLL))
        {
            mainCamera.ChangeMoveType(CameraMove.CAMERA_MOVE_TYPE.SCROLL);
        }

        // カメラの動きを範囲外に変える
        if (_request.Flag.IsFlag(REQUEST.CAMERA_OUT_RANGE))
        {
            mainCamera.ChangeMoveType(CameraMove.CAMERA_MOVE_TYPE.MOUSE_OUTRANGE);
        }

        // 交換
        if (_request.Flag.IsFlag(REQUEST.EXCHANGE))
        {
            // アイテムのマネージャに追加・削除

            foreach (IItem item in _request.ExchangeItems)
            {

                Debug.Log(item.GetCount() + " : " + item.GetNormalCount() + " : " + item.GetPowerUpCount());
            }
            bool isExchangable = manager_item.AddItems(_request.ExchangeItems);

            // 交換終了したことを相手に伝える
            if (isExchangable && uiManager.GetExchangeOtherID() >= 0)
            {
                for (int i = 0; i < manager_SI_Player.GetPlayers().Count; i++)
                {
                    if (uiManager.GetExchangeOtherID() == manager_SI_Player.GetPlayer(i).ID)
                    {
                        // トレード音
                        sound.PlaySound(SoundType_MainGame.Trade);

                        manager_SI_Player.GetPlayer(i).IsExcange = false;
                    }
                }
            }

            // リクエストの返答
            _request.FinalizeExchange(isExchangable);
        }

        // QRの生成
        if (_request.Flag.IsFlag(REQUEST.CREADED_QR))
        {
            // 生成音
            sound.PlaySound(SoundType_MainGame.Qr);
            for (int i = 0; i < manager_SI_Player.GetPlayers().Count; i++)
            {
                if (PhotonNetwork.player.ID == manager_SI_Player.GetPlayer(i).ID)
                {
                    manager_SI_Player.GetPlayer(i).IsExcange = true;
                }
            }
        }

        // 収集
        if (_request.Flag.IsFlag(REQUEST.COLLECT))
        {
            bool isCollectable = owner_buildingResource.GetBuildingResource(_request.CollectPlaceType).IsCollectable(_request.CollectItemType);

            if (isCollectable)
            {
                // 資源の追加
                List<IItem> _items = owner_buildingResource.GetBuildingResource(_request.CollectPlaceType).GetItems(_request.CollectItemType, _request.IsDoubleCollect);
                manager_item.AddItems(_items);
            }
            _request.FinalizeCollect(isCollectable);
        }

        // 座標を場所に変換
        if (_request.Flag.IsFlag(REQUEST.POSITION_TO_PLACE))
        {
            Type placeType = judgeField.ChangePositionToPlaceType(_request.ChangePosition);
            bool isChange = (owner_floor.GetPlaceTotalFloor(placeType) != 0);
            if (isChange)
            {
                _request.ChangePlaceType = judgeField.ChangePositionToPlaceType(_request.ChangePosition);
                _request.AreaCenterPosition = judgeField.GetAreaCenterPosition(_request.ChangePlaceType);
                
            }
            _request.FinalizePositionToPlace(isChange);
        }

        // 人間の強化
        if (_request.Flag.IsFlag(REQUEST.POWER_UP_HUMAN))
        {
            if (_request.PowerUpHumanType != ITEM_TYPE.NONE)
            {

                // 建築に必要な素材を取得
                List<IItem> _items = _request.PowerUpItems;
                // 資源が足りているか確認
                bool _isExchange = manager_item.IsExchange(_items);

                // 強化成功
                if (_isExchange)
                {
                    manager_item.GetItem(_request.PowerUpHumanType).AddPowerUpCount(1);
                }
                // 強化失敗
                else
                {
                    // 失敗音
                    sound.PlaySound(SoundType_MainGame.Error);
                }
                _request.FinalizePowerUp(_isExchange);
            }
        }
        

        _request.FinalizeRequest();
    }

    // 初期資源を手に入れる
    void FirstGetResource()
    {
        manager_item.GetItem(ITEM_TYPE.LOOGER).SetCount(manager_item.GetItem(ITEM_TYPE.LOOGER).GetCount() + 1);
        manager_item.GetItem(ITEM_TYPE.ENGINEER).SetCount(manager_item.GetItem(ITEM_TYPE.ENGINEER).GetCount() + 1);
        manager_item.GetItem(ITEM_TYPE.COAL_MINER).SetCount(manager_item.GetItem(ITEM_TYPE.COAL_MINER).GetCount() + 1);
    }

    void UpdateUIRequest()
    {
        if (uiManager.IsSetPlaceHumanType())
        {
            List<ITEM_TYPE> types = new List<ITEM_TYPE>();
            foreach (Human human in owner_human.GetPlaceHuman(currentPlaceType))
            {
                if (!human.IsPowerUp())
                {
                    types.Add(human.GetItemType());
                }
            }
            uiManager.SetPlaceHumanType(types);
            uiManager.SetPowerUpWindow(owner_floor.GetTotalFloor());
        }
    }

    void UpdateServer()
    {
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            ChangedItem(manager_item.GetItem(type).GetCount(), i);
        }

        for (int i = 0; i < (int)Type.Max; i++)
        {
            Type type = (Type)i;
            for (int j = 0; j < manager_SI_Player.GetPlayers().Count; j++)
            {
                if (PhotonNetwork.player.ID == manager_SI_Player.GetPlayer(j).ID)
                {
                    manager_SI_Player.GetPlayer(j).SetPlacePoint(owner_floor.GetPlaceTotalFloor(type), (int)type);
                }
            }
        }
    }
}
