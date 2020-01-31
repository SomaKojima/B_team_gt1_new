using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonUnitButton : UI_Button_Market
{
    // 取得できるアイテムのリスト
    private List<IItem> getItems = new List<IItem>();    
    // 要求するアイテムの数量
    private int requiredNum = 0;
    // 要求するアイテムの数量(テキスト)
    [SerializeField]
    private Text requiredText = null;

    // 要求アイテムが資源化どうか
    bool isBr = false;

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

    [SerializeField]
    Image humanImage;

    [SerializeField]
    Image brImage;

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
    public void Initialize(List<IItem> _getItems, int _requiredNum, bool _isBr)
    {

        // 値初期化
        requiredNum = _requiredNum;
        // ボタンのテキスト変更
        requiredText.text = requiredNum.ToString();
        // リスト内の数だけ入手できるアイテムを生成する
        getItems.Clear();
        getItems = _getItems;
        foreach (IItem item in _getItems)
        {
            // ノーマルを表示
            if (item.GetNormalCount() > 0)
            {
                managerCmnIcn.Add(
                factoryCmnIcn.Create(item.GetItemType(), item.GetNormalCount(), false)
                );
            }

            //Debug.Log(item.GetPowerUpCount());
            // 強化のアイコンを表示
            if (item.GetPowerUpCount() > 0)
            {
                managerCmnIcn.Add(
                    factoryCmnIcn.Create(item.GetItemType(), item.GetPowerUpCount(), true)
                    );
            }
        }

        isBr = _isBr;

        humanImage.gameObject.SetActive(false);
        brImage.gameObject.SetActive(false);
        if (isBr)
        {
            brImage.gameObject.SetActive(true);
        }
        else
        {
            humanImage.gameObject.SetActive(true);
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

    public void ChangeGetItems(List<IItem> _getItems)
    {
        managerCmnIcn.AllDestory();
        Initialize(_getItems, requiredNum, isBr);
    }

    public void ChangeRequiredNum(int _requiredNum)
    {
        requiredNum = _requiredNum;
        // ボタンのテキスト変更
        requiredText.text = "必要数\n" + requiredNum.ToString();
    }

    public void UnActiveMask()
    {
        mask.gameObject.SetActive(false);
    }

    public bool IsBr()
    {
        return isBr;
    }
}
