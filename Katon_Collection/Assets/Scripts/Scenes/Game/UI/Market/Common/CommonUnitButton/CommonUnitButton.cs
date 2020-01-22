using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonUnitButton : UI_Button_Market
{
    // 取得できるアイテムのリスト
    private List<IItem> getItems = null;    
    // 要求するアイテムの数量
    private int requiredNum = 0;
    // 要求するアイテムの数量(テキスト)
    [SerializeField]
    private Text requiredText = null;

    // アイコンを表示するための親オブジェクトの位置
    [SerializeField]
    Transform iconParent;

    // Factory_CommonUnitIconオブジェクト
    [SerializeField]
    Factory_CommonUnitIcon factoryCmnIcn;
    // Manager_CommonUnitIconオブジェクト
    [SerializeField]
    Manager_CommonUnitIcon managerCmnIcn;

    [SerializeField]
    Image mask;

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
    public void Initialize(List<IItem> _getItems, int _requiredNum)
    {

        // 値初期化
        requiredNum = _requiredNum;
        // ボタンのテキスト変更
        requiredText.text = "必要数\n" + requiredNum.ToString();
        // リスト内の数だけ入手できるアイテムを生成する
        getItems = _getItems;
        foreach (IItem item in _getItems)
        {
            factoryCmnIcn.Create(item.GetItemType(), item.GetCount());
        }
    }


    // 要求するアイテムの数量を取得
    public int GetRequiredNum() { return requiredNum; }
    // 取得できるアイテムのリストを取得
    public List<IItem> GetGetItems() { return getItems; }

    public void ActiveMask()
    {
        mask.gameObject.SetActive(true);
    }


    public void UnActiveMask()
    {
        mask.gameObject.SetActive(false);
    }
}
