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

    // Start is called before the first frame update
    void Start()
    {
        managerGuestName.Add(factoryGuestName.Create("kojima"));
        managerGuestName.Add(factoryGuestName.Create("kojima"));
        managerGuestName.Add(factoryGuestName.Create("kojima"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Inititalize(bool entry)
    {
        if(entry == true)
        {
            startButton.gameObject.SetActive(false);
        }
    }


    public bool IsGameStart()
    {
        return false;
    }
}
