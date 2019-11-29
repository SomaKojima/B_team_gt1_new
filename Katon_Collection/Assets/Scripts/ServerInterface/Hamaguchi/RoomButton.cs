using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : Photon.MonoBehaviour
{
    private RoomInfo m_Data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(RoomInfo roomData)
    {
        m_Data = roomData;
        gameObject.GetComponentInChildren<Text>().text = m_Data.name + ", " + m_Data.playerCount + "/4";
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        PhotonNetwork.JoinRoom(m_Data.name);
        Debug.Log("部屋入室 name:"+m_Data.name);
        Debug.Log("部屋入室 playercount:"+m_Data.playerCount);
    }
}
