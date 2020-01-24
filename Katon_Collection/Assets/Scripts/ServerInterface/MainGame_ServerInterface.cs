using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame_ServerInterface : MonoBehaviour
{
    [SerializeField]
    Manage_SI_Player manager_SI_Player;

    [SerializeField]
    Manager_ServerInterface manager_ServerInterface;

    [SerializeField]
    PLInfoManager PLInfoManager;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.inRoom)
        {
            PhotonNetwork.offlineMode = true;
        }

        //PLInfoManager.CreatePLInfoWindow(PhotonNetwork.playerList.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool LostConnection()
    {
        return manager_ServerInterface.LostConnection();
    }

    public Manage_SI_Player Manager_SI_Player()
    {
        return manager_SI_Player;
    }


}
