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

    [SerializeField]
    Owner_CommonUnitButton owner_commonUnitButton;

    [SerializeField]
    Owner_ChangeItem owner_changeItem;

    [SerializeField]
    ChangeCountWindow changeCountWindow = null;

    [SerializeField]
    Owner_SelectItemButton owner_selectItemButton;

    [SerializeField]
    Text totalText;

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

        owner_commonUnitButton.Create(items, 30);
        List<IItem> items2 = new List<IItem>();
        item = new Item();
        item.Initialize(10, ITEM_TYPE.COAL_MINER);
        items2.Add(item);
        item = new Item();
        item.Initialize(10, ITEM_TYPE.ORE);
        items2.Add(item);
        owner_commonUnitButton.Create(items2, 20);

        List<IItem> items3 = new List<IItem>();
        item = new Item();
        item.Initialize(10, ITEM_TYPE.COAL_MINER);
        items2.Add(item);
        item = new Item();
        item.Initialize(10, ITEM_TYPE.ORE);
        items2.Add(item);
        owner_commonUnitButton.Create(items3, 20);

        // 仮マイアイテム
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.WOOD, 30));
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.ORE, 20));
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.PARTS, 10));

        //managerCngItm.LineupRemainItem();
        //managerCngItm.DisplayTotalCount();

        owner_selectItemButton.Create(ITEM_TYPE.WOOD);
        owner_selectItemButton.Create(ITEM_TYPE.ORE);
    }

    // Update is called once per frame
    void Update()
    {
        // タブを切り替え
        ChangeTab();

        Debug.Log(marketType);

        // 支払いの素材を選択
        if (owner_selectItemButton.IsClick())
        {
            changeCountWindow.Initialize(owner_selectItemButton.GetItem().GetItemType(), owner_selectItemButton.GetItem().GetCount());
        }

        if (changeCountWindow.IsAplly())
        {
            owner_selectItemButton.ChangeCountOfClickButton(changeCountWindow.GetCount());
        }

        // 合計値を更新する
        totalText.text = "合計 : "+ owner_selectItemButton.GetTotal().ToString() + "個";
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
