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

    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGuestName();
        isStart = false;
        if (startButton.IsClick())
        {
            startButton.OnClickProcess();
            isStart = true;
        }
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

        manager_si_player.UpdatePlayers();
        // ホスト名の初期化
        hostName.Inititalize(manager_si_player.GetPlayer(manager_si_player.GetPlayers().Count - 1).Name);

        // ゲスト名の初期化
        UpdateGuestName();
    }
    


    public bool IsGameStart()
    {
        return isStart;
    }

    public void UpdateGuestName()
    {
        managerGuestName.AllDelete();

        manager_si_player.UpdatePlayers();

        // ゲスト名の初期化
        for (int i = manager_si_player.GetPlayers().Count - 2; 0 <= i; i--)
        {
            string name = manager_si_player.GetPlayer(i).Name;
            managerGuestName.Add(factoryGuestName.Create(name));
        }
    }
}
