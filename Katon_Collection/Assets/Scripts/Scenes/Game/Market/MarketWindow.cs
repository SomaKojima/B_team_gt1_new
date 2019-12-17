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
    public MARKET_TYPE marketType = MARKET_TYPE.SALE;

    // UI_Button_Marketオブジェクト
    [SerializeField]
    UI_Button_Market uiButtonMkt = null;

    // Factory_CommonUnitButtonオブジェクト
    [SerializeField]
    Factory_CommonUnitButton factoryCmnUntBtn = null;

    // Manager_CommonUnitButtonオブジェクト
    [SerializeField]
    Manager_CommonUnitButton managerCmnUntBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        // 仮提示
        List<IItem> items = new List<IItem>();
        Item item = new Item();
        items.Add(item.Initialize(ITEM_TYPE.PARTS, 20));
        items.Add(item.Initialize(ITEM_TYPE.WOOD, 10));
        managerCmnUntBtn.Add(factoryCmnUntBtn.Create(items, 30));
    }

    // Update is called once per frame
    void Update()
    {
        // タブを切り替え
        ChangeTab();


    }

    // 市場の選択メニューを取得
    public MARKET_TYPE GetMarketType()
    {
        return marketType;
    }

    /// <summary>
    /// タブを切り替え
    /// </summary>
    private void ChangeTab()
    {
        if (uiButtonMkt.IsClickSale())
        {
            marketWindow.sprite = saleSprite;
            marketType = MARKET_TYPE.SALE;
        }
        else if (uiButtonMkt.IsClickCommon())
        {
            marketWindow.sprite = commonSprite;
            marketType = MARKET_TYPE.COMMON;
        }
    }

    /// <summary>
    /// アイテムリスト生成
    /// </summary>
    /// <returns>アイテムリスト</returns>
    //List<IItem> CreateItem()
    //{
    //    List<IItem> items = new List<IItem>();

    //    return items;
    //}

}
