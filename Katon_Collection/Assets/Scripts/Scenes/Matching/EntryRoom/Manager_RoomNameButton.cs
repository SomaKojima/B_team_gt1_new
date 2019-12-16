using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_RoomNameButton : MonoBehaviour
{
    [SerializeField]
    List<UI_Button_RoomName> buttons;
    UI_Button_RoomName enter = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enter = null;
        foreach(UI_Button_RoomName button in buttons)
        {
            if(button.IsClick() == true)
            {
                enter = button;
            }
        }
    }

    public void AllDelete()
    {
        buttons.Clear();
    }

    public void Add(UI_Button_RoomName button)
    {
        buttons.Add(button);
    }

    public UI_Button_RoomName GetEnterButtonName()
    {
        return enter;
    }
}
