using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button_RoomName : UI_Button_RoomMatching
{
    [SerializeField]
    Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialize(string name)
    {
        text.text = name;
    }

    public string GetRoomName()
    {
        return text.text;
    }
}

