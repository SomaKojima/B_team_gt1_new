using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PhotonManager : Photon.MonoBehaviour
{
    [SerializeField]
    private GameObject m_CreateBtnObj;
    [SerializeField]
    private GameObject m_JoinBtnObj;
    [SerializeField]
    private GameObject m_LoginBtnObj;
    [SerializeField]
    private GameObject m_CreateRoomBtnObj;
    [SerializeField]
    private GameObject m_NameInputField;
    [SerializeField]
    private GameObject m_RoomNameInputField;
    [SerializeField]
    private GameObject m_RoomList;
    [SerializeField]
    private GameObject m_Room1BtnObj;
    [SerializeField]
    private GameObject m_Room2BtnObj;
    [SerializeField]
    private GameObject m_Room3BtnObj;
    [SerializeField]
    private GameObject m_Room4BtnObj;
    [SerializeField]
    private GameObject m_UpdateBtnObj;

    /// <summary>ルーム名</summary>
    private string m_RoomName = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(null);
    }

    // Update is called once per frame
    void Update()
    {

    }


    //ロビーに接続した時に呼ばれるコールバックメソッド
    void OnJoinedLobby()
    {
        Debug.Log("ロビーに接続しました");
        //ボタンを押せるようにする
        m_CreateBtnObj.GetComponent<Button>().interactable = true;
        m_JoinBtnObj.GetComponent<Button>().interactable = true;

    }

    public void ConnectPhoton()
    {
        //PhotonNetwork.ConnectUsingSettings(null);
    }

    //ルーム作成
    public void CreateRoom()
    {
        //string userName = "ユーザ1";
        //string userId = "user1";
        PhotonNetwork.autoCleanUpPlayerObjects = false;
        ////カスタムプロパティ
        //ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
        //customProp.Add("userName", userName); //ユーザ名
        //customProp.Add("userId", userId); //ユーザID
        //PhotonNetwork.SetPlayerCustomProperties(customProp);

        RoomOptions roomOptions = new RoomOptions();
        //roomOptions.customRoomProperties = customProp;
        //ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
        //roomOptions.customRoomPropertiesForLobby = new string[] { "userName", "userId" };
        roomOptions.maxPlayers = 4; //部屋の最大人数
        roomOptions.isOpen = true; //入室許可する
        roomOptions.isVisible = true; //ロビーから見えるようにする
        //userIdが名前のルームがなければ作って入室、あれば普通に入室する。
        PhotonNetwork.CreateRoom(m_RoomName, roomOptions, null);
    }

    //ルーム入室した時に呼ばれるコールバックメソッド
    void OnJoinedRoom()
    {
        Debug.Log("ルームに入りました");
        m_RoomNameInputField.SetActive(false);
        m_CreateRoomBtnObj.SetActive(false);
        m_RoomList.SetActive(false);
        m_UpdateBtnObj.SetActive(false);
        SceneManager.LoadScene("RoomScene");
    }

    //ルーム作成した時に呼ばれるコールバックメソッド
    void OnCreatedRoom()
    {
        Debug.Log("ルームを作成しました");
        Debug.Log(m_RoomName);
    }

    //ルーム一覧が取れると
    void OnReceivedRoomListUpdate()
    {
        //ルーム一覧を取る
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (rooms.Length == 0)
        {
            //Debug.Log("ルームが一つもありません");
        }
        else
        {
            //ルームが1件以上ある時ループでRoomInfo情報をログ出力
            for (int i = 0; i < rooms.Length; i++)
            {
                //Debug.Log("RoomName:" + rooms[i].name);
                //Debug.Log("userName:" + rooms[i].customProperties["userName"]);
                //Debug.Log("userId:" + rooms[i].customProperties["userId"]);
                //GameObject.Find("StatusText").GetComponent<Text>().text = rooms[i].name;
            }
        }

    }

    //名前決定ボタンを押した際に呼ぶメソッド
    public void OnLogin()
    {
        m_NameInputField.SetActive(false);
        m_LoginBtnObj.SetActive(false);
        m_CreateBtnObj.SetActive(true);
        m_JoinBtnObj.SetActive(true);
    }

    //部屋作成選択ボタンを押した際に呼ぶメソッド
    public void CreateScene()
    {
        m_CreateBtnObj.SetActive(false);
        m_JoinBtnObj.SetActive(false);
        m_RoomNameInputField.SetActive(true);
        m_CreateRoomBtnObj.SetActive(true);
    }

    //部屋参加選択ボタンを押した際に呼ぶメソッド
    public void JoinScene()
    {
        m_CreateBtnObj.SetActive(false);
        m_JoinBtnObj.SetActive(false);
        m_RoomList.SetActive(true);
        m_UpdateBtnObj.SetActive(true);

        UpdateRoomData();
    }

    //ルームリストを更新する
    public void UpdateRoomData()
    {
        m_Room1BtnObj.SetActive(false);
        m_Room2BtnObj.SetActive(false);
        m_Room3BtnObj.SetActive(false);
        m_Room4BtnObj.SetActive(false);

        //ルーム一覧を取る
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (rooms.Length == 0)
        {
            Debug.Log("ルームが一つもありません");
        }
        else
        {
            //ルームが1件以上ある時ループでRoomInfo情報をログ出力
            for (int i = 0; i < rooms.Length; i++)
            {
                Debug.Log("number:" + i);
                Debug.Log("RoomName:" + rooms[i].name);
                Debug.Log("PlayerCount:" + rooms[i].playerCount);
                switch(i)
                {
                    case 0:
                        m_Room1BtnObj.SetActive(true);
                        m_Room1BtnObj.GetComponent<RoomButton>().Set(rooms[i]);
                        break;
                    case 1:
                        m_Room2BtnObj.SetActive(true);
                        m_Room2BtnObj.GetComponent<RoomButton>().Set(rooms[i]);
                        break;
                    case 2:
                        m_Room3BtnObj.SetActive(true);
                        m_Room3BtnObj.GetComponent<RoomButton>().Set(rooms[i]);
                        break;
                    case 3:
                        m_Room4BtnObj.SetActive(true);
                        m_Room4BtnObj.GetComponent<RoomButton>().Set(rooms[i]);
                        break;
                }
                
            }
        }
    }

    //名前の入力欄が変更された時に呼ばれるコールバックメソッド
    public void InputLogger()
    {
        string inputValue = m_NameInputField.GetComponent<InputField>().text;

        PhotonNetwork.playerName = inputValue;

        Debug.Log("名前:" + PhotonNetwork.playerName);

        m_LoginBtnObj.GetComponent<Button>().interactable = true;

        if (inputValue=="")
        {
            m_LoginBtnObj.GetComponent<Button>().interactable = false;
        }
    }

    //部屋名の入力欄が変更された時に呼ばれるコールバックメソッド
    public void InputRoomLogger()
    {
        string inputValue = m_RoomNameInputField.GetComponent<InputField>().text;

        m_RoomName = inputValue;

        Debug.Log("部屋:" + m_RoomName);

        m_CreateRoomBtnObj.GetComponent<Button>().interactable = true;

        if (inputValue == "")
        {
            m_CreateRoomBtnObj.GetComponent<Button>().interactable = false;
        }
    }

    //サーバーに到達できず接続できない時に呼ばれるコールバックメソッド
    void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("接続に失敗しました:" + cause.ToString());
        //SceneManager.LoadScene("TitleScene");
    }

    //何かのせいで(接続が確立した後)接続失敗した時に呼ばれるコールバックメソッド
    void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("サーバーとの接続後に何らかの原因で切断されました:" + cause.ToString());
        //SceneManager.LoadScene("TitleScene");
    }

    //同時接続可能数の制限に到達した時に呼ばれるコールバックメソッド
    void OnPhotonMaxCccuReached()
    {
        Debug.Log("サーバーに接続しているクライアント数が上限に達しています");
        //SceneManager.LoadScene("TitleScene");
    }
}
