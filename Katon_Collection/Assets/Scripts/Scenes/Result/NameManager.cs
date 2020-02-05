using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NameManager : MonoBehaviour
{
    [SerializeField]
    Manage_SI_Player si_player = null;

    [SerializeField]
    Text name1;

    [SerializeField]
    Text name2;

    [SerializeField]
    Text name3;

    [SerializeField]
    Text name4;

    [SerializeField]
    GameObject fukidasi1;

    [SerializeField]
    GameObject fukidasi2;

    [SerializeField]
    GameObject fukidasi3;

    [SerializeField]
    GameObject fukidasi4;


    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        for (int i = 0; i < si_player.GetPlayers().Count; i++)
        {
            count++;
            switch (i)
            {
                case 0:
                    name1.text = si_player.GetPlayer(i).Name;
                    break;
                case 1:
                    name2.text = si_player.GetPlayer(i).Name;
                    break;
                case 2:
                    name3.text = si_player.GetPlayer(i).Name;
                    break;
                case 3:
                    name4.text = si_player.GetPlayer(i).Name;
                    break;
            }
        }

        switch (count)
        {
            case 1:
                fukidasi2.SetActive(false);
                fukidasi3.SetActive(false);
                fukidasi4.SetActive(false);
                break;
            case 2:
                fukidasi3.SetActive(false);
                fukidasi4.SetActive(false);
                break;
            case 3:
                fukidasi4.SetActive(false);
                break;
            case 4:
                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
