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

    // Factory_ChangeItemオブジェクト
    [SerializeField]
    Factory_ChangeItem factoryCngItm = null;
    // Manager_ChangeItemオブジェクト
    [SerializeField]
    Manager_ChangeItem managerCngItm = null;

    [SerializeField]
    ChangeCountWindow changeCountWindow = null;

    [SerializeField]
    Factory_SelectItemsButton factorySelectItemButton = null;

    [SerializeField]
    Manager_SelectItemsButton managerSelectItemButton = null;

    // Start is called before the first frame update
    void Start()
    {
        // 仮提示
        List<IItem> items = new List<IItem>();
        Item item = new Item();
        item.Initialize(20, ITEM_TYPE.PARTS);
        items.Add(item);
        item = new Item();
        item.Initialize(10, ITEM_TYPE.WOOD);
        items.Add(item);
        managerCmnUntBtn.Add(factoryCmnUntBtn.Create(items, 30));
        List<IItem> items2 = new List<IItem>();
        item = new Item();
        item.Initialize(10, ITEM_TYPE.COAL_MINER);
        items2.Add(item);
        item = new Item();
        item.Initialize(10, ITEM_TYPE.ORE);
        items2.Add(item);
        managerCmnUntBtn.Add(factoryCmnUntBtn.Create(items2, 20));

        // 仮マイアイテム
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.WOOD, 30));
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.ORE, 20));
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.PARTS, 10));

        //managerCngItm.LineupRemainItem();
        //managerCngItm.DisplayTotalCount();

        managerSelectItemButton.Add(factorySelectItemButton.Create(ITEM_TYPE.WOOD));
        managerSelectItemButton.Add(factorySelectItemButton.Create(ITEM_TYPE.ORE));
    }

    // Update is called once per frame
    void Update()
    {
        // タブを切り替え
        ChangeTab();

        Debug.Log(marketType);
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
        // クリックで切り替え
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

        // デフォルトの値と現在のタブが異なっていたら切り替え
        if(marketType == MARKET_TYPE.SALE && marketWindow.sprite != saleSprite)
        {
            marketWindow.sprite = saleSprite;
        }
        else if (marketType == MARKET_TYPE.COMMON && marketWindow.sprite != commonSprite)
        {
            marketWindow.sprite = commonSprite;
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
