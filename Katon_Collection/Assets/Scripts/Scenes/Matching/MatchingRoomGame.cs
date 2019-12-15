using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingRoomGame : MonoBehaviour
{
    [SerializeField]
    SelectRoomMode_Window selectRoomModeWindow;
    [SerializeField]
    MakeRoom_Window makeRoom_Window;
    [SerializeField]
    WaitRoom_Window waitRoom_Window;
    [SerializeField]
    EnterRoom_Window entryRoom_Window;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(selectRoomModeWindow.GetRoomMode() != ROOM_MODE.None)
        {
            selectRoomModeWindow.gameObject.SetActive(false);
           
            // 部屋に入る
            if (selectRoomModeWindow.GetRoomMode() == ROOM_MODE.Enter)
            {
                entryRoom_Window.gameObject.SetActive(true);

            }
            // 部屋を探す
            if (selectRoomModeWindow.GetRoomMode() == ROOM_MODE.Make)
            {
                makeRoom_Window.gameObject.SetActive(true);
            }
            selectRoomModeWindow.Initialized();
        }

        // 部屋に入る
        if(entryRoom_Window.GetEnterRoomName() != "")
        {
            waitRoom_Window.gameObject.SetActive(true);
            entryRoom_Window.gameObject.SetActive(false);
            if (selectRoomModeWindow.GetRoomMode() == ROOM_MODE.Make)
            {
                waitRoom_Window.Inititalize(false);
            }
            else
            {
                waitRoom_Window.Inititalize(true);
            }
        }
        
        if(makeRoom_Window.GetRoomMode() == ROOM_MODE.Wait)
        {
            waitRoom_Window.gameObject.SetActive(true);
            makeRoom_Window.gameObject.SetActive(false);
        }
    }
}
