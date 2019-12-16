﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingRoomGame : MonoBehaviour
{
    [SerializeField]
    SelectRoomMode_Window selectRoomModeWindow;
    [SerializeField]
    MakeRoom_Window makeRoom_Window;
    [SerializeField]
    WaitRoom_Window waitRoom_Window;
    [SerializeField]
    EnterRoom_Window entryRoom_Window;

    [SerializeField]
    Manager_ServerInterface serverInterface;

    // Start is called before the first frame update
    void Start()
    {
        serverInterface.ConnectServer();
        serverInterface.EnterLobby();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (selectRoomModeWindow.GetRoomMode() != ROOM_MODE.None)
        {
            selectRoomModeWindow.gameObject.SetActive(false);

            // 部屋に作るウィンドウをアクティブに
            if (selectRoomModeWindow.GetRoomMode() == ROOM_MODE.Enter)
            {
                entryRoom_Window.gameObject.SetActive(true);
                ActiveEntoryRoomMode();
            }
            // 部屋を入るウィンドウをアクティブに
            if (selectRoomModeWindow.GetRoomMode() == ROOM_MODE.Make)
            {
                makeRoom_Window.gameObject.SetActive(true);
                ActiveCreateRoomMode();
            }
            selectRoomModeWindow.Initialized();
        }

        // 部屋に入る
        if (entryRoom_Window.GetEnterRoomName() != "")
        {
            waitRoom_Window.gameObject.SetActive(true);
            entryRoom_Window.gameObject.SetActive(false);
            waitRoom_Window.Inititalize(true, entryRoom_Window.GetEnterRoomName());
            EntoryRoom();
        }

        // 部屋を作る
        if (makeRoom_Window.IsMakeRoom())
        {
            waitRoom_Window.gameObject.SetActive(true);
            makeRoom_Window.gameObject.SetActive(false);
            waitRoom_Window.Inititalize(false, makeRoom_Window.GetInputRoomName());
            CreateRoom();
        }
    }

    // 部屋を作るウィンドウをアクティブにする時の処理
    void ActiveCreateRoomMode()
    {
    }

    // 部屋に入るウィンドウをアクティブにする時の処理
    void ActiveEntoryRoomMode()
    {
    }

    // 部屋を作ったときの処理
    void CreateRoom()
    {
        serverInterface.CreateRoom(makeRoom_Window.GetInputRoomName());
    }

    // 部屋に入るときの処理
    void EntoryRoom()
    {
        serverInterface.EnterRoom(entryRoom_Window.GetEnterRoomName());
    }

}
