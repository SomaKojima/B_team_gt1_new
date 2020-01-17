using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonWindow : MonoBehaviour
{
    [SerializeField]
    Owner_CommonUnitButton owner_commonUnitButton;

    [SerializeField]
    SelectItemButtonWidnow selectItemButtonWindow;
   
    [SerializeField]
    Text totalExchageNumText;

    [SerializeField]
    UI_Button_Market applyButton;
    
    Manager_Item managerItem = null;

    bool isExhcnage = false;
    int exchangeCount = 0;

    List<IItem> exchangeItemList = new List<IItem>();

    public void Initialize(Manager_Item _managerItem)
    {
        selectItemButtonWindow.Initialize(_managerItem);
        managerItem = _managerItem;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateUnitBotton();
    }

    // Update is called once per frame
    void Update()
    {

        // 合計交換回数を更新する
        if (owner_commonUnitButton.GetSelectCommonUnitButton() != null)
        {
            int requiredNum = owner_commonUnitButton.GetSelectCommonUnitButton().GetRequiredNum();
            int total = selectItemButtonWindow.GetTotal();
            exchangeCount = total / requiredNum;
            totalExchageNumText.text = exchangeCount.ToString();
        }

        // 交換ボタンが押された
        isExhcnage = false;
        if (applyButton.IsClick())
        {
            applyButton.OnClickProcess();
            isExhcnage = true;
            UpdateExchangeItemList();
        }
    }

    void UpdateExchangeItemList()
    {
        exchangeItemList.Clear();
            for (int i = 0; i < exchangeCount; i++)
        {
            exchangeItemList.AddRange(owner_commonUnitButton.GetSelectCommonUnitButton().GetGetItems());
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
    }

    public bool IsExchange()
    {
        return isExhcnage;
    }

    void CreateUnitBotton()
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
        items3.Add(item);
        item = new Item();
        item.Initialize(10, ITEM_TYPE.ORE);
        items3.Add(item);
        owner_commonUnitButton.Create(items3, 20);

        // 仮マイアイテム
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.WOOD, 30));
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.ORE, 20));
        //managerCngItm.Add(factoryCngItm.Create(ITEM_TYPE.PARTS, 10));

        //managerCngItm.LineupRemainItem();
        //managerCngItm.DisplayTotalCount();

    }

    public List<IItem> GetExchangeItemList()
    {
        return exchangeItemList;
    }
    
}
