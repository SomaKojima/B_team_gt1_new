using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_ServerInterface : Photon.MonoBehaviour
{
    private bool changeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectServer()
    {
        PhotonNetwork.ConnectUsingSettings(null);
    }

    public void EnterLobby()
    {
        //PhotonNetwork.JoinLobby();
    }

    public void CreateRoom(string roomName)
    {
        if (roomName != null)
        {
            PhotonNetwork.autoCleanUpPlayerObjects = false;
            RoomOptions roomOptions = new RoomOptions();

            roomOptions.MaxPlayers = 4; //部屋の最大人数
            roomOptions.IsOpen = true; //入室許可する
            roomOptions.IsVisible = true; //ロビーから見えるようにする

            PhotonNetwork.CreateRoom(roomName, roomOptions, null);
        }
    }

    public void EnterRoom(string roomName)
    {
        if (roomName != null)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public void SetPlayerName(string name)
    {
        PhotonNetwork.playerName = name;
    }


    //ロビーに接続した時に呼ばれるコールバックメソッド
    void OnJoinedLobby()
    {
        Debug.Log("ロビーに接続しました");
    }

    //ルーム入室した時に呼ばれるコールバックメソッド
    void OnJoinedRoom()
    {
        Debug.Log("ルームに入りました");
    }

    //ルーム作成した時に呼ばれるコールバックメソッド
    void OnCreatedRoom()
    {
        Debug.Log("ルームを作成しました");
    }

    //サーバーに到達できず接続できない時に呼ばれるコールバックメソッド
    void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("接続に失敗しました:" + cause.ToString());
    }

    //何かのせいで(接続が確立した後)接続失敗した時に呼ばれるコールバックメソッド
    void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("サーバーとの接続後に何らかの原因で切断されました:" + cause.ToString());
    }

    //同時接続可能数の制限に到達した時に呼ばれるコールバックメソッド
    void OnPhotonMaxCccuReached()
    {
        Debug.Log("サーバーに接続しているクライアント数が上限に達しています");
    }

    //誰かがルームに入室したときに呼ばれるコールバックメソッド
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log(newPlayer.NickName + "　が入室しました");
        ChangedFlag();
    }

    //誰かがルームを退室したときに呼ばれるコールバックメソッド
    void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "　が退室しました");
        ChangedFlag();
    }

    private void ChangedFlag()
    {
        if(changeFlag)
        {
            changeFlag = false;
        }
        else
        {
            changeFlag = true;
        }
    }

    public bool IsChangedMember()
    {
        bool flag = changeFlag;
        if (changeFlag)
        {
            ChangedFlag();
        }

        return flag;
    }

}
