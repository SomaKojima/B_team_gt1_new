using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleMenu : MonoBehaviour
{
    // メニューの親オブジェクト
    [SerializeField]
    GameObject menuParent;

    // SALEメニューオブジェクト
    [SerializeField]
    GameObject saleMenu;

    // Commonメニューオブジェクト
    [SerializeField]
    GameObject commonMenu;

    // MarketWindowオブジェクト
    [SerializeField]
    MarketWindow marketWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // SALEメニューの表示/非表示
        if(marketWindow.GetMarketType() == Market_Type.MARKET_TYPE.SALE)
        {
            menuParent.transform.Find(commonMenu.name).gameObject.active = false;
        }
        else
        {
            menuParent.transform.Find(commonMenu.name).gameObject.active = true;
        }

        // Commonメニューの表示/非表示
        if (marketWindow.GetMarketType() == Market_Type.MARKET_TYPE.COMMON)
        {
            menuParent.transform.Find(saleMenu.name).gameObject.active = false;
        }
        else
        {
            menuParent.transform.Find(saleMenu.name).gameObject.active = true;
        }
    }
}
