﻿using System.Collections;
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
    [SerializeField]
    Manager_SI_Room manager_si_room;
    [SerializeField]
    UI_Button_RoomMatching updateButton;

    UI_Button_RoomName entryButton = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (updateButton.IsClick())
        {
            updateButton.OnClickProcess();
            UpdateRooms();
        }
    }

    public void Initialize()
    {
        UpdateRooms();
    }

    public string GetInputPlayerName()
    {
        return playerNameInput.text;
    }

    public bool IsEnterRoom()
    {
        return false;
    }

    public UI_Button_RoomName GetEnterRoomName()
    {
        return manager_RoomNameButton.GetEnterButtonName();
    }

    public void UpdateRooms()
    {
        manager_si_room.UpdateRooms();
        manager_RoomNameButton.AllDelete();
        foreach (SI_Room room in manager_si_room.GetRooms())
        {
            Debug.Log(room.RoomName);
            manager_RoomNameButton.Add(factory_RoomNameButton.Create(room.RoomName));
        }
    }
}
