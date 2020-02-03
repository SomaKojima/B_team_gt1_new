using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage_SI_Player : Photon.MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    public const int MAX_MEMBER = 4;
    private List<SI_Player> players = new List<SI_Player>();

    private bool[] changeFlag = new bool[MAX_MEMBER];
    private bool[] QRchangeFlag = new bool[MAX_MEMBER];
    private PhotonView m_photonView = null;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i< MAX_MEMBER;i++)
        {
            changeFlag[i] = false;
            QRchangeFlag[i] = false;
        }
        m_photonView = GetComponent<PhotonView>();
        UpdatePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfo()
    {
        if (players.Count > 1)
        {
            if (!PhotonNetwork.isMasterClient)
            {
                MasterChange();
            }
            else
            {
                for(int i = 0;i< players.Count;i++)
                {
                    if (changeFlag[i])
                    {
                        OthersChange(i);
                    }
                }
            }
        }
    }

    public SI_Player GetPlayer(int index)
    {

        return players[index];
    }

    public List<SI_Player> GetPlayers()
    {
        return players;
    }

    public void UpdatePlayers()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;
        

        if (playerList.Length == 0)
        {
            Debug.Log("プレイヤーがいません");
        }
        else
        {
            for (int i = 0; i < playerList.Length; i++)
            {
                if (playerList.Length >= MAX_MEMBER) break;
                if (playerList.Length > players.Count)
                {
                    GameObject playerObj = Instantiate(prefab);
                    SI_Player player = playerObj.GetComponent<SI_Player>();
                    player.transform.SetParent(gameObject.transform);
                    player.Initialize();
                    players.Add(player);
                }
                players[i].ID = playerList[i].ID;
                players[i].Name = playerList[i].NickName;
            }

            for (int i = 0; i < players.Count; i++)
            {
                if (i + 1 == players.Count)
                {
                    break;
                }
                if (players[i].ID > players[i+1].ID)
                {
                    SI_Player buf = players[i];
                    players[i] = players[i + 1];
                    players[i + 1] = buf;
                }
            }
        }
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < (int)Type.Max; j++)
                {
                    Debug.Log("送信");
                    stream.SendNext(players[i].GetPlacePoint(j));
                }
                for (int j = 0; j < (int)ITEM_TYPE.NUM; j++)
                {
                    stream.SendNext(players[i].GetItemCount(j));
                }
                stream.SendNext(players[i].IsExcange);
            }
        }
        else
        {
            //データの受信

            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < (int)Type.Max; j++)
                {
                    Debug.Log("受信");
                    this.players[i].SetPlacePoint((int)stream.ReceiveNext(),j);
                }
                for (int j = 0; j < (int)ITEM_TYPE.NUM; j++)
                {
                    this.players[i].SetItemCount((int)stream.ReceiveNext(), j);
                }
                this.players[i].IsExcange = (bool)stream.ReceiveNext();
            }
        }
    }

    public void MasterChange()
    {
        int id = GetMyPlayer().ID;
        int[] placePoints = GetMyPlayer().PlacePoints;
        int[] itemCounts = GetMyPlayer().ItemCounts;
        bool isExchange = GetMyPlayer().IsExcange;
        m_photonView.RPC("RPCMasterChange", PhotonTargets.MasterClient, id, placePoints, itemCounts, isExchange);
    }

    [PunRPC]
    private void RPCMasterChange(int id,int[] placePoints,int[] itemCounts,bool isExchange)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (id == this.players[i].ID)
            {
                this.players[i].PlacePoints = placePoints;
                this.players[i].ItemCounts = itemCounts;
                //this.players[i].IsExcange = isExchange;

                changeFlag[i] = true;
            }
        }
    }

    public void OthersChange(int index)
    {
        //変更を受信したプレイヤーの更新、送信
        changeFlag[index] = false;
        int id = players[index].ID;
        int[] placePoints = players[index].PlacePoints;
        int[] itemCounts = players[index].ItemCounts;
        bool isExchange = players[index].IsExcange;
        m_photonView.RPC("RPCOthersChange", PhotonTargets.Others, id, placePoints, itemCounts, isExchange);
        //マスタークライアントプレイヤーの更新、送信
        id = GetMyPlayer().ID;
        placePoints = GetMyPlayer().PlacePoints;
        itemCounts = GetMyPlayer().ItemCounts;
        isExchange = GetMyPlayer().IsExcange;
        m_photonView.RPC("RPCOthersChange", PhotonTargets.Others, id, placePoints, itemCounts, isExchange);
    }

    [PunRPC]
    private void RPCOthersChange(int id, int[] placePoints, int[] itemCounts, bool isExchange)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (id == this.players[i].ID)
            {
                this.players[i].PlacePoints = placePoints;
                this.players[i].ItemCounts = itemCounts;
                //this.players[i].IsExcange = isExchange;
            }
        }
    }

    public SI_Player GetMyPlayer()
    {
        SI_Player my_player = null;

        for (int i = 0; i < players.Count; i++)
        {
            if (this.players[i].ID == PhotonNetwork.player.ID)
            {
                my_player = this.players[i];
                break;
            }
        }

        return my_player;
    }

    public void ExChangeInfo(int ID, bool isExchange)
    {
        if (!PhotonNetwork.isMasterClient)
        {
            m_photonView.RPC("RPCExChange", PhotonTargets.MasterClient, ID, isExchange);
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (ID == this.players[i].ID)
                {
                    this.players[i].IsExcange = isExchange;
                    ExChangeOtherInfo(ID, isExchange);
                }
            }
        }
    }

    [PunRPC]
    private void RPCExChange(int ID, bool isExchange)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (ID == this.players[i].ID)
            {
                this.players[i].IsExcange = isExchange;
                ExChangeOtherInfo(ID, isExchange);
            }
        }
    }

    public void ExChangeOtherInfo(int ID, bool isExchange)
    {
        m_photonView.RPC("RPCExChangeOther", PhotonTargets.MasterClient, ID, isExchange);
    }

    [PunRPC]
    private void RPCExChangeOther(int ID, bool isExchange)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (ID == this.players[i].ID)
            {
                this.players[i].IsExcange = isExchange;
            }
        }
    }
}
