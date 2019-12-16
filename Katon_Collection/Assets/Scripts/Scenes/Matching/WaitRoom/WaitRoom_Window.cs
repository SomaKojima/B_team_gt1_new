using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitRoom_Window : MonoBehaviour
{
    [SerializeField]
    RoomName roomName;
    [SerializeField]
    HostName hostName;
    [SerializeField]
    Factory_GuestName factoryGuestName;
    [SerializeField]
    Manager_GuestName managerGuestName;
    [SerializeField]
    UI_Button_RoomMatching startButton;
    [SerializeField]
    GameObject wait;

    [SerializeField]
    Manage_SI_Player manager_si_player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGuestName();
    }

    public void Inititalize(bool entry, string _roomName)
    {
        if (entry == true)
        {
            startButton.gameObject.SetActive(false);
            wait.SetActive(true);
        }

        // 部屋名の初期化
        roomName.Inititalize(_roomName);

        // ホスト名の初期化
        hostName.Inititalize(manager_si_player.GetPlayer(0).Name);

        // ゲスト名の初期化
        UpdateGuestName();
    }


    public bool IsGameStart()
    {
        return false;
    }

    public void UpdateGuestName()
    {
        managerGuestName.Clear();
        // ゲスト名の初期化
        for (int i = 1; i < manager_si_player.GetPlayers().Count; i++)
        {
            string name = manager_si_player.GetPlayer(i).Name;
            managerGuestName.Add(factoryGuestName.Create(name));
        }
    }
}
