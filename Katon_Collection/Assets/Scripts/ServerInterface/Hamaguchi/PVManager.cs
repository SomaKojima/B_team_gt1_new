using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVManager : Photon.MonoBehaviour
{
    private PlayerInfo[] playerInfos = new PlayerInfo[4];
    int member = 0;

    private PhotonView m_photonView = null;

    // Start is called before the first frame update
    void Start()
    {
        PhotonPlayer[] players = PhotonNetwork.playerList;
        member = PhotonNetwork.playerList.Length;

        Debug.Log(member);

        int count = 0;
        while (true)
        {
            if (count + 1 >= players.Length)
            {
                break;
            }
            if (players[count].ID > players[count + 1].ID)
            {
                PhotonPlayer playerA = players[count];
                players[count] = players[count + 1];
                players[count + 1] = playerA;
            }
            count++;
        }

        for (int i = 0; i < 4; i++) 
        {
            playerInfos[i] = new PlayerInfo();
            playerInfos[i].Area1Point = 0;
            playerInfos[i].Area2Point = 0;
            playerInfos[i].Area3Point = 0;
            playerInfos[i].Area4Point = 0;
            playerInfos[i].Area5Point = 0;
            playerInfos[i].Area6Point = 0;
            playerInfos[i].TradeFlag = false;
            playerInfos[i].PlayerNum = i;
            if (i < member)
            {
                playerInfos[i].PlayerID = players[i].ID;
            }
            else
            {
                playerInfos[i].PlayerID = -1;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            for (int i = 0; i < 4; i++)
            {
                stream.SendNext(playerInfos[i].Area1Point);
                stream.SendNext(playerInfos[i].Area2Point);
                stream.SendNext(playerInfos[i].Area3Point);
                stream.SendNext(playerInfos[i].Area4Point);
                stream.SendNext(playerInfos[i].Area5Point);
                stream.SendNext(playerInfos[i].Area6Point);
                stream.SendNext(playerInfos[i].TradeFlag);
                stream.SendNext(playerInfos[i].PlayerID);
            }
        }
        else
        {
            //データの受信
            for (int i = 0; i < 4; i++)
            {
                this.playerInfos[i].Area1Point = (int)stream.ReceiveNext();
                this.playerInfos[i].Area2Point = (int)stream.ReceiveNext();
                this.playerInfos[i].Area3Point = (int)stream.ReceiveNext();
                this.playerInfos[i].Area4Point = (int)stream.ReceiveNext();
                this.playerInfos[i].Area5Point = (int)stream.ReceiveNext();
                this.playerInfos[i].Area6Point = (int)stream.ReceiveNext();
                this.playerInfos[i].TradeFlag = (bool)stream.ReceiveNext();
                this.playerInfos[i].PlayerID = (int)stream.ReceiveNext();
            }
        }
    }

    public int PLInfoAreaPointGet(int ID,int areanum)
    {
        for(int i=0;i<playerInfos.Length;i++)
        {
            if(playerInfos[i].PlayerID==ID)
            {
                switch(areanum)
                {
                    case 1:
                        return playerInfos[i].Area1Point;
                        break;
                    case 2:
                        return playerInfos[i].Area2Point;
                        break;
                    case 3:
                        return playerInfos[i].Area3Point;
                        break;
                    case 4:
                        return playerInfos[i].Area4Point;
                        break;
                    case 5:
                        return playerInfos[i].Area5Point;
                        break;
                    case 6:
                        return playerInfos[i].Area6Point;
                        break;
                }
            }
        }
        return 0;
    }

    public bool PLInfoTreadFlagGet(int ID)
    {
        for (int i = 0; i < playerInfos.Length; i++)
        {
            if (playerInfos[i].PlayerID == ID)
            {
                return playerInfos[i].TradeFlag;
            }
        }

        return false;
    }

    public int MyIDGet()
    {
        return PhotonNetwork.player.ID;
    }

    public int PlayerIDGet(int PlayerNum)
    {
        return playerInfos[PlayerNum].PlayerID;
    }

    public int PlayerNumGet(int PlayerNum)
    {
        return playerInfos[PlayerNum].PlayerNum;
    }

    public void PLInfoTreadFlagSet(int ID, bool SetFlag)
    {
        photonView.RPC("RPCPLInfoTreadFlagSet", PhotonTargets.MasterClient, ID, SetFlag);
    }

    public void PLInfoAreaPointSet(int ID, int areanum,int SetPoint)
    {
        photonView.RPC("RPCPLInfoAreaPointSet", PhotonTargets.MasterClient, ID, areanum, SetPoint);
    }

    public int Member
    {
        get { return member; }
    }

    [PunRPC]
    private void RPCPLInfoAreaPointSet(int ID, int areanum, int SetPoint)
    {
        for (int i = 0; i < playerInfos.Length; i++)
        {
            if (playerInfos[i].PlayerID == ID)
            {
                switch (areanum)
                {
                    case 1:
                        playerInfos[i].Area1Point = SetPoint;
                        break;
                    case 2:
                        playerInfos[i].Area2Point = SetPoint;
                        break;
                    case 3:
                        playerInfos[i].Area3Point = SetPoint;
                        break;
                    case 4:
                        playerInfos[i].Area4Point = SetPoint;
                        break;
                    case 5:
                        playerInfos[i].Area5Point = SetPoint;
                        break;
                    case 6:
                        playerInfos[i].Area6Point = SetPoint;
                        break;
                }
            }
        }
    }

    [PunRPC]
    private void RPCPLInfoTreadFlagSet(int ID, bool SetFlag)
    {
        for (int i = 0; i < playerInfos.Length; i++)
        {
            if (playerInfos[i].PlayerID == ID)
            {
                playerInfos[i].TradeFlag = SetFlag;
            }
        }
    }
}
