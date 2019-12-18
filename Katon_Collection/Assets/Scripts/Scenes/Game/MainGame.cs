using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    QR_ReaderWindow qrReaderWindow;

    [SerializeField]
    Manager_PlaceBar manager_placeBar;

    [SerializeField]
    Manager_Item manager_item;

    [SerializeField]
    Owner_Human owner_human;


    // Start is called before the first frame update
    void Start()
    {
        manager_item.Initialize();
        owner_human.Intialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager_placeBar.GetIsQRLeader())
        {
            qrReaderWindow.Initialize();
        }

        if (qrReaderWindow.IsExchange())
        {
            qrReaderWindow.FinishExchange();
            foreach (IItem item in qrReaderWindow.GetItems())
            {
                manager_item.GetItem(item.GetItemType()).AddCount(item.GetCount());
            }
        }

        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            owner_human.MatchItemsHumans(manager_item.GetItem(type), false);
        }


    }

    
}
