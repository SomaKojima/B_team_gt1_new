using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButtonWidnow : MonoBehaviour
{
    [SerializeField]
    ChangeCountWindow normalWindow = null;
    [SerializeField]
    ChangeCountWindow powerUpWindow = null;

    [SerializeField]
    Owner_SelectItemButton owner_selectItemButton;

    [SerializeField]
    Text totalItemNumText;

    Manager_Item managerItem = null;

    List<IItem> exchangeItems = new List<IItem>();

    Manager_Item refItem = null;

    List<IItem> seletItems = new List<IItem>();

    public void Initialize(Manager_Item _managerItem)
    {
        managerItem = _managerItem;
        owner_selectItemButton.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 支払いの素材を選択
        if (owner_selectItemButton.IsClick())
        {
            IItem selectItem = owner_selectItemButton.GetItem();
            // アイテムが人間だった場合
            if (ItemType.IsHumanType(selectItem.GetItemType()))
            {
                powerUpWindow.Active();
                powerUpWindow.Initialize(selectItem.GetItemType(), selectItem.GetPowerUpCount(), true);
            }
            normalWindow.Active();
            normalWindow.Initialize(selectItem.GetItemType(), selectItem.GetNormalCount(), false);

            AddSelectItems(selectItem);
        }

        // 支払い素材の個数を選択中に、選択画面の最大所持数を更新する
        if (owner_selectItemButton.GetItem() != null)
        {
            IItem selectItem = owner_selectItemButton.GetItem();
            int currentCount = managerItem.GetItem(selectItem.GetItemType()).GetNormalCount();
            normalWindow.SetMaxCount(currentCount);

            currentCount = managerItem.GetItem(selectItem.GetItemType()).GetPowerUpCount();
            powerUpWindow.SetMaxCount(currentCount);
        }

        // 支払い素材の個数を変更
        owner_selectItemButton.ChangeNormalCountOfClickButton(normalWindow.GetCount());
        owner_selectItemButton.ChangePowerUpCountOfClickButton(powerUpWindow.GetCount());
        if (normalWindow.IsAplly() || powerUpWindow.IsAplly())
        {
            owner_selectItemButton.FinishChangeCount();

            powerUpWindow.UnActive();
            normalWindow.UnActive();
        }

        // 合計値を更新する
        totalItemNumText.text = owner_selectItemButton.GetTotal().ToString();
        
    }

    public int GetTotal()
    {
        return owner_selectItemButton.GetTotal();
    }

    public Manager_Item GetManagerItem()
    {
        return owner_selectItemButton.GetManagerItem();
    }

    public List<IItem> GetExchangeItems()
    {
        return exchangeItems;
    }

    // 選択できるアイテムを制限する
    public void SetLimitSelectType(bool isHuman, bool isBr)
    {
        owner_selectItemButton.SetActiveButton(isHuman, isBr);
    }

    void AddSelectItems(IItem item)
    {
        if (seletItems.Count != 0)
        {
            if (seletItems[seletItems.Count - 1] == item)
            {
                return;
            }
        }
        seletItems.Add(item);

        if (seletItems.Count > 10)
        {
            seletItems.RemoveAt(0);
        }
    }

    List<IItem> GetSelectItems()
    {
        return seletItems;
    }

    public IItem GetLastSelectItem()
    {
        if (seletItems.Count == 0) return null;
        if (seletItems[seletItems.Count - 1].GetCount() == 0) seletItems.RemoveAt(seletItems.Count - 1);
        if (seletItems.Count != 0)
        {
            return seletItems[seletItems.Count - 1];
        }
        return null;
    }

}
