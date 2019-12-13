using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoomMode_Window : MonoBehaviour
{
    [SerializeField]
    UI_Button_RoomMatching makeRoomButton;
    [SerializeField]
    UI_Button_RoomMatching entryRoomButton;

    private ROOM_MODE mode = ROOM_MODE.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Initialized()
    {
        mode = ROOM_MODE.None;
    }
    // Update is called once per frame
    void Update()
    {
        if(makeRoomButton.IsClick() == true)
        {
            makeRoomButton.OnClickProcess();
            mode = ROOM_MODE.Make;
        }
        if(entryRoomButton.IsClick() == true)
        {
            entryRoomButton.OnClickProcess();
            mode = ROOM_MODE.Enter;
        }
    }

    public ROOM_MODE GetRoomMode()
    {
        return mode;
    }
}
