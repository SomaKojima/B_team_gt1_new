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

    [SerializeField]
    TextAsset csvFile; // CSVファイル

    Manager_Item managerItem = null;

    bool isExhcnage = false;
    int exchangeCount = 0;

    List<IItem> exchangeItemList = new List<IItem>();

    Common_Encode common_Encode = new Common_Encode();

    public void Initialize(Manager_Item _managerItem)
    {
        owner_commonUnitButton.Initialize();
        selectItemButtonWindow.Initialize(_managerItem);
        managerItem = _managerItem;
    }

    // Start is called before the first frame update
    void Start()
    {
        common_Encode.Initialize();
        bool isEncode = common_Encode.EncodeToItem(csvFile.text);
        Debug.Log(isEncode);
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
        Debug.Log("start");
        // 仮提示
        foreach (CommonEncodeData data in common_Encode.GetDateList())
        {
            List<IItem> items = new List<IItem>();
            Debug.Log("create");

            // 手に入るitemの設定
            foreach (IItem item in data.get_items)
            {
                items.Add(item);
            }

            // ボタンの作成
            owner_commonUnitButton.Create(items, data.neccesaryCount);
        }


        //List<IItem> items = new List<IItem>();
        //items.Add(new Item(10, ITEM_TYPE.WOOD));
        //owner_commonUnitButton.Create(items, 30);

        //List<IItem> items2 = new List<IItem>();
        //items2.Add(new Item(10, ITEM_TYPE.COAL_MINER));
        //items2.Add(new Item(10, ITEM_TYPE.ORE));
        //owner_commonUnitButton.Create(items2, 20);


        //List<IItem> items3 = new List<IItem>();
        //items3.Add(new Item(10, ITEM_TYPE.COAL_MINER));
        //items3.Add(new Item(10, ITEM_TYPE.ORE));
        //owner_commonUnitButton.Create(items3, 20);

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
