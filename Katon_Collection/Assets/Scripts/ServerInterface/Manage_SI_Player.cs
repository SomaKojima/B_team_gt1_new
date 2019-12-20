using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage_SI_Player : Photon.MonoBehaviour
{
    private List<SI_Player> players = new List<SI_Player>();
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if(players.Count==0)
        //{

        //}
        //else
        //{
        //    for (int i = 0; i < players.Count; i++)
        //    {
        //        if(players[i].GetChangeFlag())
        //        {
        //            RPCOthersChange();
        //        }
        //    }
        //}
        
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
        players.Clear();

        if (playerList.Length == 0)
        {
            Debug.Log("プレイヤーがいません");
        }
        else
        {
            for (int i = 0; i < playerList.Length; i++)
            {
                if (players.Count < i + 1)
                {
                    players.Add(new SI_Player());
                }
                players[i].ID = playerList[i].ID;
                players[i].Name = playerList[i].NickName;
            }
        }
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(players);
        }
        else
        {
            //データの受信
            this.players = (List<SI_Player>)stream.ReceiveNext();
        }
    }

    //public void OthersChange()
    //{
    //    photonView.RPC("RPCOthersChange", PhotonTargets.Others);
    //}

    //[PunRPC]
    //private void RPCOthersChange()
    //{
        
    //}
}
