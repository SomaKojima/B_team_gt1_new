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

    [SerializeField]
    SuccessWindow successWindow;

    // 選択している項目の必要な資源のタイプを表す画像
    [SerializeField]
    Image humanImage;
    [SerializeField]
    Image brImage;

    Manager_Item managerItem = null;

    bool isExhcnage = false;
    int exchangeCount = 0;

    float time = 0;

    int requiredNum = 0;
    int total = 0;

    List<IItem> exchangeItemList = new List<IItem>();

    Common_Encode common_Encode = new Common_Encode();

    public void Initialize(Manager_Item _managerItem)
    {
        owner_commonUnitButton.Initialize();
        selectItemButtonWindow.Initialize(_managerItem);
        managerItem = _managerItem;
        humanImage.gameObject.SetActive(false);
        brImage.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        common_Encode.Initialize();
        common_Encode.EncodeToItem(csvFile.text);

        CreateUnitBotton(time);
    }

    // Update is called once per frame
    void Update()
    {

        // 合計交換回数を更新する
        if (owner_commonUnitButton.GetSelectCommonUnitButton() != null)
        {
            requiredNum = owner_commonUnitButton.GetSelectCommonUnitButton().GetRequiredNum();
            total = selectItemButtonWindow.GetTotal();
            exchangeCount = total / requiredNum;
            totalExchageNumText.text = exchangeCount.ToString();

            // 必要な資源のタイプを表す画像をこうしんする
            if (owner_commonUnitButton.GetSelectCommonUnitButton().IsBr())
            {
                brImage.gameObject.SetActive(true);
                humanImage.gameObject.SetActive(false);
            }
            else
            {
                brImage.gameObject.SetActive(false);
                humanImage.gameObject.SetActive(true);
            }
            bool isBr = owner_commonUnitButton.GetSelectCommonUnitButton().IsBr();
            bool isHuman = !isBr;
            selectItemButtonWindow.SetLimitSelectType(isHuman, isBr);
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

        // 余りは残す
        int different = total - (exchangeCount * requiredNum);
        int[] differentType = new int[(int)ITEM_TYPE.NUM];
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            differentType[i] = 0;
        }
        Debug.Log(different);
        while (different > 0)
        {
            if (selectItemButtonWindow.GetLastSelectItem() == null) break;
            ITEM_TYPE type = selectItemButtonWindow.GetLastSelectItem().GetItemType();
            IItem item = selectItemButtonWindow.GetManagerItem().GetItem(type);
            int count = item.GetCount() - different;
            if (count < 0)
            {
                different = count * -1;
                count = 0;
            }
            else
            {
                different -= item.GetCount();
            }
            differentType[(int)type] = selectItemButtonWindow.GetManagerItem().GetItem(type).GetCount() - count;
            selectItemButtonWindow.GetManagerItem().GetItem(type).SetCount(count);
        }

        foreach (IItem item in selectItemButtonWindow.GetManagerItem().GetItemList())
        {
            exchangeItemList.Add(new Item(-item.GetNormalCount(), -item.GetPowerUpCount(), item.GetItemType()));
        }
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            selectItemButtonWindow.GetManagerItem().GetItem(type).SetCount(differentType[i]);
        }

        selectItemButtonWindow.AllUpdate();
    }

    public void Active()
    {
        gameObject.SetActive(true);
        selectItemButtonWindow.SetLimitSelectType(true, true);
    }

    public void UnActive()
    {
        Debug.Log("unActive");
        gameObject.SetActive(false);
        selectItemButtonWindow.SetLimitSelectType(false, false);
        owner_commonUnitButton.ClearSelect();
    }

    public bool IsExchange()
    {
        return isExhcnage;
    }
    
    void CreateUnitBotton(float time)
    {
        // 仮提示
        List<CommonEncodeData> deleteData = new List<CommonEncodeData>();
        foreach (CommonEncodeData data in common_Encode.GetDateList())
        {
            // 時間に達していない場合は作らない
            //if (data.time > time) continue;
            

            List<IItem> items = new List<IItem>();
            
            // 手に入るitemの設定
            foreach (IItem item in data.get_items)
            {
                ITEM_TYPE type = item.GetItemType();
                switch (type)
                {
                    case ITEM_TYPE.RANDOM_HUMAN:
                        type = ItemType.RandomHuman();
                        break;
                    case ITEM_TYPE.RANDOM_BUILIDNG_RESOURCE:
                        type = ItemType.RandomBuildingResource();
                        break;
                    case ITEM_TYPE.RANDOM:
                        type = ItemType.RandomType();
                        break;
                    default:
                        break;
                }
                item.SetType(type);
                items.Add(item);
            }

            // ボタンの作成
            owner_commonUnitButton.Create(items, data.neccesaryCount, data.is_item, !data.is_item);
            deleteData.Add(data);
        }

        // 作ったボタンのデーターを破棄する
        //foreach (CommonEncodeData data in deleteData)
        //{
        //    common_Encode.GetDateList().Remove(data);
        //}
        

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

    /// <summary>
    /// 建築時の更新処理
    /// </summary>
    public void UpdateBuilding(int buildingTotal)
    {
    }

    public List<IItem> GetExchangeItemList()
    {
        return exchangeItemList;
    }

    public void SetTime(float _time)
    {
        time = _time;
    }

    public void FinalizeExchange(bool isExchangeable)
    {
        successWindow.gameObject.SetActive(true);
        if(isExchangeable)
        {
            successWindow.Success();
        }
        else
        {
            successWindow.Field();
        }
    }
}
