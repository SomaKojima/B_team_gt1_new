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
    UI_Button_RoomMatching backButton;

    [SerializeField]
    Fade_CloudEffect fade_CloudEffect = null;

    // Start is called before the first frame update
    void Start()
    {
        serverInterface.ConnectServer();
        serverInterface.EnterLobby();
        StartCoroutine(fade_CloudEffect.FadeOut());
    }

    // Update is called once per frame
    void Update()
    {

        if (selectRoomModeWindow.GetRoomMode() != ROOM_MODE.None &&
            selectRoomModeWindow.gameObject.activeSelf &&
            !entryRoom_Window.gameObject.activeSelf &&
            !makeRoom_Window.gameObject.activeSelf)
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
        if (entryRoom_Window.GetEnterRoomName() != null)
        {
            waitRoom_Window.gameObject.SetActive(true);
            entryRoom_Window.gameObject.SetActive(false);
            EntoryRoom();
            if (serverInterface.IsJoinedRoom())
            {
                entryRoom_Window.GetEnterRoomName().OnClickProcess();
                waitRoom_Window.Inititalize(true, entryRoom_Window.GetEnterRoomName().GetRoomName());
            }
        }

        // 部屋を作る
        if (makeRoom_Window.IsMakeRoom())
        {
            waitRoom_Window.gameObject.SetActive(true);
            makeRoom_Window.gameObject.SetActive(false);
            CreateRoom();
            if(serverInterface.IsJoinedRoom())
            {
                waitRoom_Window.Inititalize(false, makeRoom_Window.GetInputRoomName());
            }
        }
        
        if (!waitRoom_Window.gameObject.activeSelf)
        {
            serverInterface.LeaveRoom();
        }

        //ゲームを開始する
        if (serverInterface.GetGameStartFlag())
        {
            Debug.Log("gamestart");

            SceneManager.LoadScene("GameScene");
        }

        if (waitRoom_Window.IsGameStart())
        {
            if (serverInterface.IsMaster())
            {
                StartGameMaster();
            }
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
    }

    // 部屋に入るときの処理
    void EntoryRoom()
    {
        serverInterface.EnterRoom(entryRoom_Window.GetEnterRoomName().GetRoomName());
        serverInterface.SetPlayerName(entryRoom_Window.GetInputPlayerName());
    }

    // ゲーム開始時の処理
    void StartGameMaster()
    {
        serverInterface.OthersGameStartFlagSet(true);
    }

    void GameStart()
    {
        StartCoroutine(fade_CloudEffect.FadeIn());
    }
}
