using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketWindow : MonoBehaviour
{
    // 市場のウィンドウ
    [SerializeField]
    Image marketWindow = null;

    // Saleのウィンドウスプライト
    [SerializeField]
    Sprite saleSprite = null;
    // Commonのウィンドウスプライト
    [SerializeField]
    Sprite commonSprite = null;

    // 市場の選択しているタイプ(Sale or Common)
    public Market_Type.MARKET_TYPE marketType = Market_Type.MARKET_TYPE.SALE;

    // UI_Button_Marketオブジェクト
    [SerializeField]
    UI_Button_Market uiButtonMkt = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(uiButtonMkt.IsClickSale())
        {
            marketWindow.sprite = saleSprite;
            marketType = Market_Type.MARKET_TYPE.SALE;
        }
        else if(uiButtonMkt.IsClickCommon())
        {
            marketWindow.sprite = commonSprite;
            marketType = Market_Type.MARKET_TYPE.COMMON;
        }

        Debug.Log(marketType);
    }

    // 市場の選択メニューを取得
    public Market_Type.MARKET_TYPE GetMarketType()
    {
        return marketType;
    }
}
