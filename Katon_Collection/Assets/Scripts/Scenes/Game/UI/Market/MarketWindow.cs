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

    Vector3 cameraPosition;

    List<IItem> exchangeItemList = new List<IItem>();

    float time = 0;

    public void Initialize(Manager_Item _managerItem, float _time)
    {
        commonWindow.Initialize(_managerItem);
        saleWindow.Initialize(_managerItem);

        SetTime(_time);
        commonWindow.SetTime(time);
        saleWindow.SetTime(time);
    }

    void Initialize()
    {
        isBack = false;
        isExchange = false;
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
        
        // コモンの交換
        if (commonWindow.IsExchange())
        {
            exchangeItemList = commonWindow.GetExchangeItemList();
            foreach (IItem item in exchangeItemList)
            {
                Debug.Log(item.GetItemType() + " : " + item.GetCount());
            }
            isExchange = true;
        }

        // セールの交換
        if (saleWindow.IsExchange())
        {
            exchangeItemList = saleWindow.GetExchangeItems();
            isExchange = true;
        }

        isBack = false;
        if (backButton.IsClick())
        {
            backButton.OnClickProcess();
            isBack = true;
        }

        // 時間を教える
        commonWindow.SetTime(time);
        saleWindow.SetTime(time);
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

    /// <summary>
    /// 建築時の更新処理
    /// </summary>
    /// <param name="buildingTotal"></param>
    public void UpdateBuilding(int buildingTotal)
    {
        commonWindow.UpdateBuilding(buildingTotal);
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
        Initialize();
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
    }

    public bool IsBack()
    {
        bool buf = isBack;
        isBack = false;
        return buf;
    }

    public void SetTime(float _time)
    {
        time = _time;
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
