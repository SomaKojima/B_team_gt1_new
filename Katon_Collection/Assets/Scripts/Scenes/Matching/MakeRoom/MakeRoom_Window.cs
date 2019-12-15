using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeRoom_Window : MonoBehaviour
{
    [SerializeField]
    InputField playerNameInput;
    [SerializeField]
    InputField roomNameInput;
    [SerializeField]
    UI_Button_RoomMatching makeRoomButton;

    private ROOM_MODE mode = ROOM_MODE.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        mode = ROOM_MODE.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(makeRoomButton.IsClick() == true)
        {
            makeRoomButton.OnClickProcess();
            mode = ROOM_MODE.Wait;
        }
    }

    public string GetInputPlayerName()
    {
        return playerNameInput.text;
    }

    public string GetInputRoomName()
    {
        return roomNameInput.text;
    }

    public bool IsMakeRoom()
    {
        return false;
    }

    public ROOM_MODE GetRoomMode()
    {
        return mode;
    }
}
