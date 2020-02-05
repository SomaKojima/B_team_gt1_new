using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmploymentWindow : MonoBehaviour
{
    const int MAX_POWER_UP_RESOURCE_NUM = 7;

    /// <summary>
    /// 必要な資源を表示させる項目オブジェクト
    /// </summary>
    [SerializeField]
    Owner_BuildingItemUnit resourceUnit;

    /// 雇用ボタン
    [SerializeField]
    UI_Button employmentButton;

    [SerializeField]
    JudgeClickUI judgeClickUI;

    List<IItem>[] necessaryItems = new List<IItem>[MAX_POWER_UP_RESOURCE_NUM];
    int totalFloor = 0;

    bool isClose = false;

    // 雇用する
    bool isEmployment = false;

    public void Initialize()
    {
        InitializeResouce();
        resourceUnit.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
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

        isEmployment = false;
        if (employmentButton.IsClick())
        {
            employmentButton.OnClickProcess();
            isEmployment = true;
        }
    }

    public void Active()
    {
        if (this.gameObject.activeSelf) return;
        this.gameObject.SetActive(true);
        resourceUnit.SetUnits(necessaryItems[totalFloor]);
    }

    public void UnActive()
    {
        if (!this.gameObject.activeSelf) return;
        this.gameObject.SetActive(false);
        isClose = false;
        isEmployment = false;
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
    /// 雇用するかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsEmployment()
    {
        return isEmployment;    
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
    /// 建築時に呼ぶ処理
    /// </summary>
    public void OnBuilding(int _totalFloor)
    {
        totalFloor = _totalFloor;
        if (totalFloor < 0) totalFloor = 0;
        if (totalFloor >= MAX_POWER_UP_RESOURCE_NUM) totalFloor = MAX_POWER_UP_RESOURCE_NUM - 1;
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
                    cost = 10;
                    break;
                case 1:
                    cost = 50;
                    break;
                case 2:
                    cost = 100;
                    break;
                case 3:
                    cost = 160;
                    break;
                case 4:
                    cost = 240;
                    break;
                case 5:
                    cost = 300;
                    break;
                case 6:
                    cost = 300;
                    break;
            }
            necessaryItems[i].Add(new Item(-cost, ITEM_TYPE.WHEAT));
            necessaryItems[i].Add(new Item(-cost, ITEM_TYPE.COTTON));
        }
    }
}
