using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaleUnitButton : UI_Button_Market
{
    // 取得できるアイテムのリスト
    private List<IItem> getItems = new List<IItem>();
    // 支払うアイテムのリスト
    private List<IItem> payItems = new List<IItem>();

    // 交換用のアイテムリスト
    List<IItem> exchangeItemList = new List<IItem>();

    // 交換可能回数
    int exchangeCount = 0;

    [SerializeField]
    Image mask;
    
    // Factory_CommonUnitIconオブジェクト
    [SerializeField]
    Factory_CommonUnitIcon factoryGetCmnIcn;
    // Manager_CommonUnitIconオブジェクト
    [SerializeField]
    Manager_CommonUnitIcon managerGetCmnIcn;


    // Factory_CommonUnitIconオブジェクト
    [SerializeField]
    Factory_CommonUnitIcon factoryPayCmnIcn;
    // Manager_CommonUnitIconオブジェクト
    [SerializeField]
    Manager_CommonUnitIcon managerPayCmnIcn;

    // 足りているかどうか
    bool isEnough = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Commonボタンの初期化
    /// </summary>
    /// <param name="_getItems">入手できるアイテムのリスト</param>
    /// <param name="_requiredNum">要求するアイテムの総数</param>
    public void Initialize(List<IItem> _getItems, List<IItem> _payItems)
    {
        // リスト内の数だけ入手できるアイテムを生成する
        getItems.Clear();
        getItems = _getItems;
        foreach (IItem item in _getItems)
        {
            if (item.GetNormalCount() > 0)
            {
                managerGetCmnIcn.Add(
                    factoryGetCmnIcn.Create(item.GetItemType(), item.GetNormalCount(), false)
                    );
            }

            if (item.GetPowerUpCount() > 0)
            {
                managerGetCmnIcn.Add(
                    factoryGetCmnIcn.Create(item.GetItemType(), item.GetPowerUpCount(), true)
                    );
            }
        }

        payItems.Clear();
        payItems = _payItems;
        foreach (IItem item in _payItems)
        {
            if (item.GetNormalCount() < 0)
            {
                managerPayCmnIcn.Add(
            factoryPayCmnIcn.Create(item.GetItemType(), item.GetNormalCount(), false)
            );
            }

            if (item.GetPowerUpCount() < 0)
            {
                managerPayCmnIcn.Add(
                    factoryPayCmnIcn.Create(item.GetItemType(), item.GetPowerUpCount(), true)
                    );
            }
        }

        UpdateExchangeItemList();
    }

    // 取得できるアイテムのリストを取得
    public List<IItem> GetGetItems() { return getItems; }

    // 支払うアイテムのリストを取得
    public List<IItem> GetPayItems() { return payItems; }

    // 交換可能数を取得
    public int GetExchangeCount() { return exchangeCount; }

    // 交換可能かどうかを判定する
    public bool IsExchangable()
    {
        if (exchangeCount <= 0)
        {
            return false;
        }
        return true;
    }

    // 交換時の処理
    public void Exchange()
    {
        exchangeCount--;
    }

    public void ActiveMask()
    {
        mask.gameObject.SetActive(true);
    }

    public void ChangeGetItems(List<IItem> _getItems)
    {
        managerGetCmnIcn.AllDestory();
        getItems.Clear();
        getItems = _getItems;
        foreach (IItem item in _getItems)
        {
            if (item.GetNormalCount() > 0)
            {
                managerGetCmnIcn.Add(
                    factoryGetCmnIcn.Create(item.GetItemType(), item.GetNormalCount(), false)
                    );
            }

            if (item.GetPowerUpCount() > 0)
            {
                managerGetCmnIcn.Add(
                    factoryGetCmnIcn.Create(item.GetItemType(), item.GetPowerUpCount(), true)
                    );
            }
        }
        UpdateExchangeItemList();
    }


    public void ChangePayItems(List<IItem> _payItems)
    {
        managerPayCmnIcn.AllDestory();
        getItems.Clear();
        getItems = _payItems;
        foreach (IItem item in _payItems)
        {
            if (item.GetNormalCount() < 0)
            {
                    managerPayCmnIcn.Add(
                factoryPayCmnIcn.Create(item.GetItemType(), item.GetNormalCount(), false)
                );
            }

            if (item.GetPowerUpCount() < 0)
            {
                managerPayCmnIcn.Add(
                    factoryPayCmnIcn.Create(item.GetItemType(), item.GetPowerUpCount(), true)
                    );
            }
        }
        UpdateExchangeItemList();
    }

    public void UnActiveMask()
    {
        mask.gameObject.SetActive(false);
    }

    public void ChackePayItems(IItem[] items)
    {
        int index = 0;
        // 足りているかどうか
        isEnough = true;
        foreach (IItem item in payItems)
        {
            managerPayCmnIcn.Icons[index].UnActiveRed();
            // アイテムの数が足りているかどうか判定
            if (item.GetNormalCount() < 0 &&
                - item.GetNormalCount() > items[(int)item.GetItemType()].GetNormalCount())
            {
                managerPayCmnIcn.Icons[index].ActiveRed();
                isEnough = false;
                index++;
            }

            if (item.GetPowerUpCount() < 0 &&
                -item.GetPowerUpCount() > items[(int)item.GetItemType()].GetPowerUpCount())
            {
                isEnough = false;
                managerPayCmnIcn.Icons[index].ActiveRed();
                index++;
            }
        }
    }

    public bool IsEnough()
    {
        return isEnough;
    }

    void UpdateExchangeItemList()
    {
        exchangeItemList.Clear();
        exchangeItemList.AddRange(getItems);
        exchangeItemList.AddRange(payItems);
    }

    public List<IItem> GetExchangeItemList()
    {
        return exchangeItemList;
    }
}
