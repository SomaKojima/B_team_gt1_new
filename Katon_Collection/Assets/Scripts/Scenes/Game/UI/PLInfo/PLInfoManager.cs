﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLInfoManager : MonoBehaviour
{
    private GameObject[] PLInfoWindows;
    private List<SI_Player> data = new List<SI_Player>();
    private int currentNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatePLInfoWindow()
    {
        //Prefabの読み込み
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Scenes/Game/UI/PLInfo/PLInfoWindow");
        PhotonPlayer[] playerList = PhotonNetwork.playerList;
        PLInfoWindows = new GameObject[data.Count];


        for (int i = 0; i < data.Count; i++)
        {
            PLInfoWindows[i] = Instantiate(prefab, this.transform.position, Quaternion.identity);
            PLInfoWindows[i].transform.parent = this.transform;
            PLInfoWindows[i].GetComponent<PLInfoWindow>().SetNameText(playerList[i].NickName);
            PLInfoWindows[i].GetComponent<PLInfoWindow>().DataSet(data[i]);
        }
        
    }

    public void SetPlayerInfo(List<SI_Player> data)
    {
        this.data = data;
    }

    public void PLInfoActives()
    {
        for (int i = 0; i < PLInfoWindows.Length; i++)
        {
            PLInfoWindows[i].SetActive(false);
            if(i==currentNumber)
            {
                PLInfoWindows[i].SetActive(true);
            }
        }
    }
}
