using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_SI_Room : Photon.MonoBehaviour
{
    private SI_Room[] rooms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SI_Room GetRoom(int index)
    {
        return rooms[index];
    }

    public SI_Room[] GetRooms()
    {
        return rooms;
    }

    public void UpdateRooms()
    {
        //ルーム一覧を取る
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        if (roomList.Length == 0)
        {
            Debug.Log("ルームが一つもありません");
        }
        else
        {
            //ルームが1件以上ある時ループでRoomInfo情報を部屋のリストに更新
            for (int i = 0; i < roomList.Length; i++)
            {
                rooms[i].MaxPlayer = roomList[i].MaxPlayers;
                rooms[i].CurrentPlayer = roomList[i].PlayerCount;
                rooms[i].RoomName = roomList[i].Name;
            }
        }
    }
}
