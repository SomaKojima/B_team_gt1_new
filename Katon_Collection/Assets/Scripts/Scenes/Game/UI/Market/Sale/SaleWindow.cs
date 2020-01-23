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

    bool isExchange = false;

    List<IItem> exchangeItems = new List<IItem>();

    Manager_Item managerItem;

    Common_Encode common_Encode = new Common_Encode();

    public void Initialize(Manager_Item _managerItem)
    {
        owner_saleUnitButton.Initialize(_managerItem);
        managerItem = _managerItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        common_Encode.Initialize();
        common_Encode.EncodeToItem(csvFile.text);
        CreateUnitButton();
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
                // アイテムの数が足りている場合
                if (owner_saleUnitButton.GetSelectCommonUnitButton().IsEnough())
                {
                    isExchange = true;
                    exchangeItems = owner_saleUnitButton.GetSelectCommonUnitButton().GetExchangeItemList();
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

    public void CreateUnitButton()
    {
        // 仮提示
        foreach (CommonEncodeData data in common_Encode.GetDateList())
        {
            List<IItem> getItems = new List<IItem>();

            // 手に入るitemの設定
            foreach (IItem item in data.get_items)
            {
                getItems.Add(item);
            }

            List<IItem> payItems = new List<IItem>();

            // 手に入るitemの設定
            foreach (IItem item in data.pay_items)
            {
                payItems.Add(item);
            }

            // ボタンの作成
            owner_saleUnitButton.Create(getItems, payItems);
        }

    }
}
