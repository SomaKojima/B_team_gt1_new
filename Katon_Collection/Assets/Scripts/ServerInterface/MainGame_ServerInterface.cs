using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame_ServerInterface : MonoBehaviour
{
    [SerializeField]
    Manage_SI_Player manager_SI_Player;

    [SerializeField]
    SI_Game sI_Game;

    [SerializeField]
    Manager_ServerInterface manager_ServerInterface;

    [SerializeField]
    PLInfoManager PLInfoManager;

    [SerializeField]
    GameObject RankUI;

    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.inRoom)
        {
            PhotonNetwork.offlineMode = true;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateManager_SI_Player();
        RankUpdate();
        if(sI_Game.GetTime()<0)
        {
            int[] PLpoint = new int[manager_SI_Player.GetPlayers().Count];
            for(int i=0;i< manager_SI_Player.GetPlayers().Count;i++)
            {
                int point = 0;
                for (int j = 0; j < (int)Type.Max; j++)
                {
                    point += manager_SI_Player.GetPlayer(i).GetPlacePoint(j);
                }
                PLpoint[i] = point;
            }
            sI_Game.SetScore(PLpoint);
            sI_Game.IsGameSet = true;
        }
    }

    public bool LostConnection()
    {
        return manager_ServerInterface.LostConnection();
    }

    public Manage_SI_Player Manager_SI_Player()
    {
        return manager_SI_Player;
    }

    public void OnClick()
    {
        if (!PLInfoManager.GetWindowIsActive())
        {
            PLInfoManager.SetPlInfo(manager_SI_Player.GetPlayers());
            PLInfoManager.CreatePLInfoWindow();
        }
        else
        {
            PLInfoManager.DeleteWindow();
        }
    }

    public void UpdateManager_SI_Player()
    {
        manager_SI_Player.UpdateInfo();
    }

    public void RankUpdate()
    {
        int myID = manager_SI_Player.GetMyPlayer().ID;
        int myPoint = 0;
        int[] otherPoint = new int[manager_SI_Player.GetPlayers().Count - 1];
        bool flag = false;
        for (int i = 0; i < manager_SI_Player.GetPlayers().Count; i++) 
        {
            int point = 0;
            if (myID == manager_SI_Player.GetPlayer(i).ID)
            {
                for (int j = 0; j < (int)Type.Max; j++)
                {
                    point += manager_SI_Player.GetPlayer(i).placePoint[j];
                }
                myPoint = point;
                flag = true;
            }
            else
            {
                for (int j = 0; j < (int)Type.Max; j++)
                {
                    point += manager_SI_Player.GetPlayer(i).placePoint[j];
                }
                if (flag)
                {
                    otherPoint[i - 1] = point;
                }
                else
                {
                    otherPoint[i] = point;
                }
            }
        }

        int rank = 1;
        for(int i = 0; i < otherPoint.Length; i++)
        {
            if(otherPoint[i] > myPoint)
            {
                rank++;
            }
        }

        switch(rank)
        {
            case 1:
                RankUI.GetComponent<Text>().text = "1st";
                break;
            case 2:
                RankUI.GetComponent<Text>().text = "2nd";
                break;
            case 3:
                RankUI.GetComponent<Text>().text = "3rd";
                break;
            case 4:
                RankUI.GetComponent<Text>().text = "4th";
                break;
        }
    }
}
