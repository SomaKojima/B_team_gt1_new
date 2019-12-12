using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SI_Room : Photon.MonoBehaviour
{
    private int currentPlayer;
    private int maxPlayer;
    private string roomName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CurrentPlayer
    {
        get { return currentPlayer; }
        set { currentPlayer = value; }
    }

    public int MaxPlayer
    {
        get { return maxPlayer; }
        set { maxPlayer = value; }
    }

    public string RoomName
    {
        get { return roomName; }
        set { roomName = value; }
    }
}
