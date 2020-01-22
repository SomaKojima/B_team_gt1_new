using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaleUnitButton : UI_Button_Market
{
    // 取得できるアイテムのリスト
    private List<IItem> getItems = null;
    // 支払うアイテムのリスト
    private List<IItem> payItems = null;

    // 交換可能回数
    int exchangeCount = 0;

    [SerializeField]
    Factory_SaleUnitButton factory;

    [SerializeField]
    Manager_SaleUnitButton manager;

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
        getItems = _getItems;
        foreach (IItem item in _getItems)
        {
            factoryGetCmnIcn.Create(item.GetItemType(), item.GetCount());
        }

        payItems = _payItems;
        foreach (IItem item in _payItems)
        {
            factoryPayCmnIcn.Create(item.GetItemType(), item.GetCount());
        }
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


    public void UnActiveMask()
    {
        mask.gameObject.SetActive(false);
    }
}
