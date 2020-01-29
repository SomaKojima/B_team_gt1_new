using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_SelectItemButton : MonoBehaviour
{
    [SerializeField]
    Factory_SelectItemsButton factory;

    [SerializeField]
    Manager_SelectItemsButton manager;

    SelectItemsButton clickButton = null;
    bool isClick = false;

    int total = 0;

    // 個数を保存する用
    Manager_Item currentItem = new Manager_Item();

    public void Initialize()
    {
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            Create((ITEM_TYPE)i);
        }
        currentItem.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isClick = false;
        total = 0;

        foreach (SelectItemsButton button in manager.GetItemList())
        {
            // 選択した個数を保存する
            int currentCount = button.GetItem().GetCount();
            ITEM_TYPE buttonType = button.GetItem().GetItemType();
            currentItem.GetItem(buttonType).SetCount(currentCount);

            // ボタンが押されたかどうかを判定する
            if (button.IsClick())
            {
                button.OnClickProcess();
                if (clickButton == null)
                {
                    isClick = true;
                    // 選択中のボタンを保存する
                    clickButton = button;
                }
            }
            total += button.GetItem().GetCount();
        }
    }

    // ボタンを作成
    public void Create(ITEM_TYPE type)
    {
        manager.Add(factory.Create(type));
    }

    // 選択中のアイテムの個数を変更する
    public void ChangeNormalCountOfClickButton(int count)
    {
        if (clickButton != null)
        {
            clickButton.GetItem().SetNormalCount(count);
        }
    }

    // 選択中の強化したアイテムの個数を変更する
    public void ChangePowerUpCountOfClickButton(int _count)
    {
        if (clickButton != null)
        {
            clickButton.GetItem().SetPowerUpCount(_count);
        }
    }

    // 変更し終わったら呼ぶ
    public void FinishChangeCount()
    {
        clickButton = null;
    }

    // 選択中のアイテムを取得
    public IItem GetItem()
    {
        if (clickButton == null) return null;
        return clickButton.GetItem();
    }
    
    // 何かしらボタンが押されたら
    public bool IsClick()
    {
        return isClick;
    }

    // アイテムの個数の合計値を取得
    public int GetTotal()
    {
        return total;
    }

    // 個数を保存したアイテムマネージャーを取得
    public Manager_Item GetManagerItem()
    {
        return currentItem;
    }
}
