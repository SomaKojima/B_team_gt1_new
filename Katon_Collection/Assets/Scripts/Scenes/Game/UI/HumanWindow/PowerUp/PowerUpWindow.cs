using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWindow : MonoBehaviour
{
    const int MAX_POWER_UP_RESOURCE_NUM = 7;

    [SerializeField]
    Owner_PowerUpUnit owner_powerUpUnit;

    [SerializeField]
    JudgeClickUI judgeClickUI;

    /// <summary>
    /// 必要な資源を表示させる項目オブジェクト
    /// </summary>
    [SerializeField]
    Owner_BuildingItemUnit resourceUnit;
    
    bool isClickPowerUp = false;
    
    RectTransform rectTransform;

    List<IItem>[] necessaryItems = new List<IItem>[MAX_POWER_UP_RESOURCE_NUM];
    int totalFloor = 0;

    // ウィンドウを閉じる
    bool isClose = false;

    public void Initialize()
    {
        InitializeResouce();
        resourceUnit.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // ウィンドウ以外をクリックした場合
        isClose = false;
        if (judgeClickUI.IsClickOutSide())
        {
            isClose = true;
        }
    }

    public void Active()
    {
        if (this.gameObject.activeSelf) return;
        this.gameObject.SetActive(true);
        // 必要な資源を設定する
        resourceUnit.SetUnits(necessaryItems[totalFloor]);
    }

    public void UnActive()
    {
        if (!this.gameObject.activeSelf) return;
        this.gameObject.SetActive(false);
        judgeClickUI.Initialize();
        isClose = false;
    }

    /// <summary>
    /// 強化することを教える
    /// </summary>
    /// <returns></returns>
    public bool IsPowerUp()
    {
        return owner_powerUpUnit.IsPowerUp();
    }

    /// <summary>
    /// 強化する人間のタイプを取得
    /// </summary>
    /// <returns></returns>
    public ITEM_TYPE GetPowerUpItemType()
    {
        return owner_powerUpUnit.GetPowerUpItemType();
    }

    /// <summary>
    /// 強化に必要な資源を取得
    /// </summary>
    /// <returns></returns>
    public List<IItem> GetResources()
    {
        return necessaryItems[totalFloor];
    }

    /// <summary>
    /// 人間の項目を設定、生成する
    /// </summary>
    /// <param name="_itemTypes"></param>
    public void SetUnit(List<ITEM_TYPE> _itemTypes)
    {
        owner_powerUpUnit.AllDestroy();
        foreach (ITEM_TYPE type in _itemTypes)
        {
            owner_powerUpUnit.Create(type);
        }
    }


    /// <summary>
    /// 強化成功時の処理
    /// </summary>
    public void OnCorrectPowerUp()
    {
        owner_powerUpUnit.CorrectPowerUp();
    }

    /// <summary>
    /// 建築時に呼ぶ処理
    /// </summary>
    public void OnBuilding(int _totalFloor)
    {
        totalFloor = _totalFloor;
        if (totalFloor < 0) totalFloor = 0;
        if (totalFloor >= MAX_POWER_UP_RESOURCE_NUM) totalFloor = MAX_POWER_UP_RESOURCE_NUM - 1;
    }

    /// <summary>
    /// ウィンドウを閉じる処理
    /// </summary>
    /// <returns></returns>
    public bool IsClose()
    {
        return isClose;
    }

    /// <summary>
    /// 必要な資源の初期化
    /// </summary>
    public void InitializeResouce()
    {
        for (int i = 0; i < (int)MAX_POWER_UP_RESOURCE_NUM; i++)
        {
            necessaryItems[i] = new List<IItem>();
            int cost = 0;
            switch (i)
            {
                case 0:
                    cost = 20;
                    break;
                case 1:
                    cost = 20;
                    break;
                case 2:
                    cost = 50;
                    break;
                case 3:
                    cost = 100;
                    break;
                case 4:
                    cost = 160;
                    break;
                case 5:
                    cost = 240;
                    break;
                case 6:
                    cost = 300;
                    break;
            }
            necessaryItems[i].Add(new Item(-cost, ITEM_TYPE.WOOD));
            necessaryItems[i].Add(new Item(-cost / 2, ITEM_TYPE.WHEAT));
            necessaryItems[i].Add(new Item(-cost, ITEM_TYPE.COTTON));
        }
    }
}
