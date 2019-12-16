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

    bool isMakeRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(makeRoomButton.IsClick() == true)
        {
            makeRoomButton.OnClickProcess();
            isMakeRoom = true;
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
        return isMakeRoom;
    }
}
