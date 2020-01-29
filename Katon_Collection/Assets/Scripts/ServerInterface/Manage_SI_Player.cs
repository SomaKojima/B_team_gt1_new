using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage_SI_Player : Photon.MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    public const int MAX_MEMBER = 4;
    private List<SI_Player> players = new List<SI_Player>();

   
    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        //if (players.Count == 0)
        //{

        //}
        //else
        //{
        //    for (int i = 0; i < players.Count; i++)
        //    {
        //        if (players[i].GetChangeFlag())
        //        {
        //            //RPCOthersChange();
                    
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

    //public void OthersChange()
    //{
    //    photonView.RPC("RPCOthersChange", PhotonTargets.Others);
    //}

    //[PunRPC]
    //private void RPCOthersChange()
    //{

    //}

   
}
