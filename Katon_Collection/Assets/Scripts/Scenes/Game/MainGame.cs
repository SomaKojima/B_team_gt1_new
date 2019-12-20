﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    QR_ReaderWindow qrReaderWindow;

    [SerializeField]
    Manager_PlaceBar manager_placeBar;

    [SerializeField]
    Manager_Item manager_item;

    [SerializeField]
    Owner_Human owner_human;

    [SerializeField]
    Owner_Floor owner_floor;

    [SerializeField]
    Owner_SignBoard owner_signBoard;


    // Start is called before the first frame update
    void Start()
    {
        manager_item.Initialize();
        owner_human.Intialize();
    }

    // Update is called once per frame
    void Update()
    {
        // QRリーダーを起動
        if (manager_placeBar.GetIsQRLeader())
        {
            qrReaderWindow.Initialize();
        }

        // QR読み込み完了
        if (qrReaderWindow.IsExchange())
        {
            qrReaderWindow.FinishExchange();
            // アイテムのマネージャに追加・削除
            foreach (IItem item in qrReaderWindow.GetItems())
            {
                manager_item.GetItem(item.GetItemType()).AddCount(item.GetCount());
            }
        }

        // アイテムのマネージャと人間の数を合わせる
        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            owner_human.MatchItemsHumans(manager_item.GetItem(type), false);
        }

        // 看板が押されたら建築
        if (owner_signBoard.IsBuilding())
        {
            Type type = owner_signBoard.GetPlaceType();
            owner_floor.Building(type);
        }
    }

    
}
