using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketWindow : MonoBehaviour
{
    // 市場の選択しているタイプ(Sale or Common)
    public MARKET_TYPE marketType = MARKET_TYPE.SALE;

    // UI_Button_Marketオブジェクト
    [SerializeField]
    UI_Button_Market selectSaleBtn = null;

    [SerializeField]
    UI_Button_Market selectCommonBtn;

    [SerializeField]
    CommonWindow commonWindow;

    [SerializeField]
    SaleWindow saleWindow;

    [SerializeField]
    UI_Button backButton;
    bool isBack = false;

    bool isExchange = false;

    List<IItem> exchangeItemList = new List<IItem>();

    public void Initialize(Manager_Item _managerItem)
    {
        commonWindow.Initialize(_managerItem);
    }

    // Start is called before the first frame update
    void Start()
    {
    }



    // Update is called once per frame
    void Update()
    {
        // タブを切り替え
        ChangeTab();

        Debug.Log(marketType);
        exchangeItemList.Clear();

        if (commonWindow.IsExchange())
        {
            exchangeItemList = commonWindow.GetExchangeItemList();
            isExchange = true;
        }

        isBack = false;
        if (backButton.IsClick())
        {
            backButton.OnClickProcess();
            UnActive();
            isBack = true;
        }
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
        if (selectSaleBtn.IsClick())
        {
            selectSaleBtn.OnClickProcess();
            marketType = MARKET_TYPE.SALE;
            commonWindow.UnActive();
            saleWindow.Active();
        }
        else if (selectCommonBtn.IsClick())
        {
            selectCommonBtn.OnClickProcess();
            marketType = MARKET_TYPE.COMMON;
            commonWindow.Active();
            saleWindow.UnActive();
        }
        
    }

    public bool IsExchange()
    {
        return isExchange;
    }

    public void FinishExchange()
    {
        isExchange = false;
    }

    public List<IItem> GetExchangeItemList()
    {
        return exchangeItemList;
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
    }

    public bool IsBack()
    {
        return isBack;
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
