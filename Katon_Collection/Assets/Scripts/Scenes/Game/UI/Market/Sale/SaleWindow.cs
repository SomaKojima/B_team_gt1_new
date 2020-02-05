using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleWindow : MonoBehaviour
{

    [SerializeField]
    UI_Button_Market applyButton;

    [SerializeField]
    Owner_SaleUnitButton owner_saleUnitButton;

    [SerializeField]
    TextAsset csvFile; // CSVファイル

    [SerializeField]
    SuccessWindow successWindow;

    // 項目の更新時間
    [SerializeField]
    float refreshDuringTime = 10;

    bool isExchange = false;

    List<IItem> exchangeItems = new List<IItem>();

    Manager_Item managerItem;

    Common_Encode common_Encode = new Common_Encode();

    float time = 0;

    float refreshTime = 0;

    public void Initialize(Manager_Item _managerItem)
    {
        owner_saleUnitButton.Initialize(_managerItem);
        managerItem = _managerItem;

        common_Encode.Initialize();
        common_Encode.EncodeToItem(csvFile.text);
    }
    // Start is called before the first frame update
    void Start()
    {
        //CreateUnitButtonProcess(1);
        //refreshTime = refreshDuringTime;
    }

    // Update is called once per frame
    void Update()
    {

        isExchange = false;

        // 交換ボタンが押された
        if (applyButton.IsClick())
        {
            applyButton.OnClickProcess();
            if (owner_saleUnitButton.GetSelectCommonUnitButton() != null)
            {
                successWindow.gameObject.SetActive(true);
                // アイテムの数が足りている場合
                if (owner_saleUnitButton.GetSelectCommonUnitButton().IsEnough())
                {
                    isExchange = true;
                    exchangeItems = owner_saleUnitButton.GetSelectCommonUnitButton().GetExchangeItemList();
                    successWindow.Success();
                }
                else
                {
                    successWindow.Field();
                }
            }
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
        return isExchange;
    }

    public List<IItem> GetExchangeItems()
    {
        return exchangeItems;
    }

    /// <summary>
    /// ボタンの作成処理
    /// </summary>
    public void CreateUnitButtonProcess(float time)
    {

        if (time < refreshTime)
        {
            return;
        }

        refreshTime = time + refreshDuringTime;
        owner_saleUnitButton.AllDestory();

        // 仮提示
        // ランダム用のInt配列
        int[] randomIndex = new int[common_Encode.GetDateList().Count];
        for (int i = 0; i < common_Encode.GetDateList().Count; i++)
        {
            randomIndex[i] = i;
        }

        // 中身をshuffle
        ShuffleArray.shuffle(randomIndex, randomIndex.Length);

        // ランダム用のInt配列を最初から4番目までを取得
        // エンコードデータからボタンを作成
        for (int i = 0; i < 4; i++)
        {
            int index = randomIndex[i];
            CreateUnitButton(common_Encode.GetDateList()[index]);
        }
    }

    // ボタンの作成
    private void CreateUnitButton(CommonEncodeData data)
    {

        List<IItem> getItems = new List<IItem>();

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
            getItems.Add(item);
        }

        List<IItem> payItems = new List<IItem>();

        // 手に入るitemの設定
        foreach (IItem item in data.pay_items)
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
            payItems.Add(item);
        }

        // ボタンの作成
        owner_saleUnitButton.Create(getItems, payItems);
    }

    // 時間を取得
    public void SetTime(float _time)
    {
        time = _time;
    }
}
