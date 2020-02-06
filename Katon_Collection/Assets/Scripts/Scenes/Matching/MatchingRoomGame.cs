using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    Fade_CloudEffect fade_CloudEffect = null;

    // サウンド
    [SerializeField]
    Sound_MatchingRoom sound;

    [SerializeField]
    Manage_SI_Player managetSIPlayer;

    [SerializeField]
    UI_Button backButton;

    [SerializeField]
    GameObject missWindow;

    [SerializeField]
    UI_Button missWindow_yesButton;

    // Start is called before the first frame update
    void Start()
    {
        // BGMを鳴らす
        sound.PlaySound(SoundType_MatchingRoom.BGM,1.0f);

        serverInterface.ConnectServer();
        serverInterface.EnterLobby();
        fade_CloudEffect.StartFadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // クリック音を鳴らす
            sound.PlaySound(SoundType_MatchingRoom.Click,1.7f);
        }

        // 部屋を作るか入るか選択
        Update_SelectRoomMode();

        // 部屋に入る
        Update_EnterRoom();

        // 部屋を作る
        Update_MakeRoom();

        // 部屋で待機
        Update_WaitRoom();

        // 戻るボタン
        Update_Back();

        Update_MissWindow();

        //ゲームを開始する
        if (serverInterface.GetGameStartFlag())
        {
            // スタート音を鳴らす
            sound.PlaySound(SoundType_MatchingRoom.Start,1.8f);
            SceneManager.LoadScene("GameScene");
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
        serverInterface.SetPlayerName(makeRoom_Window.GetInputPlayerName());
        if (serverInterface.IsJoinedRoom())
        {
            waitRoom_Window.Inititalize(false, makeRoom_Window.GetInputRoomName());
        }
    }

    // 部屋に入るときの処理
    void EntoryRoom()
    {
        serverInterface.EnterRoom(entryRoom_Window.GetEnterRoomName().GetRoomName());
        serverInterface.SetPlayerName(entryRoom_Window.GetInputPlayerName());
        if (serverInterface.IsJoinedRoom())
        {
            entryRoom_Window.GetEnterRoomName().OnClickProcess();
            waitRoom_Window.Inititalize(true, entryRoom_Window.GetEnterRoomName().GetRoomName());
        }
    }

    // ゲーム開始時の処理
    void StartGameMaster()
    {
        serverInterface.OthersGameStartFlagSet(true);
    }

    void GameStart()
    {
        fade_CloudEffect.StartFadeIn();
    }

    void Update_SelectRoomMode()
    {
        if (selectRoomModeWindow.GetRoomMode() != ROOM_MODE.None &&
            selectRoomModeWindow.gameObject.activeSelf)
        {
            // 部屋に作るウィンドウをアクティブに
            if (selectRoomModeWindow.GetRoomMode() == ROOM_MODE.Enter)
            {
                entryRoom_Window.gameObject.SetActive(true);
                entryRoom_Window.Initialize();
                ActiveEntoryRoomMode();
            }
            // 部屋を入るウィンドウをアクティブに
            if (selectRoomModeWindow.GetRoomMode() == ROOM_MODE.Make)
            {
                makeRoom_Window.gameObject.SetActive(true);
                ActiveCreateRoomMode();
            }

            selectRoomModeWindow.gameObject.SetActive(false);
        }
    }

    void Update_EnterRoom()
    {
        if (entryRoom_Window.GetEnterRoomName() != null)
        {
            if (entryRoom_Window.GetEnterRoomName().GetRoomName() == "")
            {
                missWindow.SetActive(true);
                return;
            }
            waitRoom_Window.gameObject.SetActive(true);
            entryRoom_Window.gameObject.SetActive(false);
            EntoryRoom();
        }
    }

    void Update_MakeRoom()
    {
        if (makeRoom_Window.IsMakeRoom())
        {
            if (makeRoom_Window.GetInputRoomName() == "" ||
                makeRoom_Window.GetInputPlayerName() == "")
            {
                missWindow.SetActive(true);
                return;
            }
            waitRoom_Window.gameObject.SetActive(true);
            makeRoom_Window.gameObject.SetActive(false);
            CreateRoom();
        }
    }

    void Update_WaitRoom()
    {  
        // 部屋から抜ける
        if (waitRoom_Window.gameObject.activeSelf)
        {
            backButton.gameObject.SetActive(false);
        }

        if (waitRoom_Window.IsGameStart())
        {
            if (serverInterface.IsMaster())
            {
                StartGameMaster();
            }
        }
    }

    /// <summary>
    /// 戻る処理の更新
    /// </summary>
    void Update_Back()
    {
        if (backButton.IsClick())
        {
            backButton.OnClickProcess();

            if (selectRoomModeWindow.gameObject.activeSelf)
            {
                SceneManager.LoadScene("TitleScene");
            }

            if (entryRoom_Window.gameObject.activeSelf)
            {
                entryRoom_Window.gameObject.SetActive(false);
                selectRoomModeWindow.gameObject.SetActive(true);
                selectRoomModeWindow.Initialized();
            }

            if (makeRoom_Window.gameObject.activeSelf)
            {
                makeRoom_Window.gameObject.SetActive(false);
                selectRoomModeWindow.gameObject.SetActive(true);
                selectRoomModeWindow.Initialized();
            }

            if (waitRoom_Window.gameObject.activeSelf)
            {
                waitRoom_Window.gameObject.SetActive(false);
                if (selectRoomModeWindow.GetRoomMode() == ROOM_MODE.Make)
                {
                    makeRoom_Window.gameObject.SetActive(true);
                }
                else
                {
                    entryRoom_Window.gameObject.SetActive(true);
                    entryRoom_Window.ClearSelect();
                }

                serverInterface.LeaveRoom();
            }

        }
    }


    void Update_MissWindow()
    {
        if (missWindow_yesButton.IsClick())
        {
            missWindow_yesButton.OnClickProcess();
            missWindow.SetActive(false);
        }
    }
}
