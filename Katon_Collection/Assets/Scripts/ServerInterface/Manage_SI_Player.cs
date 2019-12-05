using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage_SI_Player : MonoBehaviour
{
    private SI_Player[] players;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SI_Player GetPlayer(int index)
    {
        return players[index];
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
                players[i].ID = playerList[i].ID;
                players[i].Name = playerList[i].NickName;
            }
        }
    }
}
