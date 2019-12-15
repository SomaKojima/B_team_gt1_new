using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterRoom_Window : MonoBehaviour
{
    [SerializeField]
    InputField playerNameInput;
    [SerializeField]
    Factory_RoomNameButton factory_RoomNameButton;
    [SerializeField]
    Manager_RoomNameButton manager_RoomNameButton;

    UI_Button_RoomName entryButton = null;

    // Start is called before the first frame update
    void Start()
    {
        manager_RoomNameButton.Add(factory_RoomNameButton.Create("kojima"));
        manager_RoomNameButton.Add(factory_RoomNameButton.Create("kojima"));
        manager_RoomNameButton.Add(factory_RoomNameButton.Create("kojima"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
    }

    public string GetInputPlayerName()
    {
        return playerNameInput.text;
    }

    public bool IsEnterRoom()
    {
        return false;
    }

    public string GetEnterRoomName()
    {
        if (manager_RoomNameButton.GetEnterButtonName() == null)
        {
            return "";
        }
        return manager_RoomNameButton.GetEnterButtonName().name;
    }

    public void UpdateRooms()
    {

    }

    
}
