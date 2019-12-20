using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButtonWidnow : MonoBehaviour
{
    [SerializeField]
    ChangeCountWindow changeCountWindow = null;
    [SerializeField]
    Owner_SelectItemButton owner_selectItemButton;

    [SerializeField]
    Text totalItemNumText;

    Manager_Item managerItem = null;

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
            changeCountWindow.Initialize(owner_selectItemButton.GetItem().GetItemType(), owner_selectItemButton.GetItem().GetCount());
        }

        // 支払い素材の個数を選択中に、選択画面の最大所持数を更新する
        if (owner_selectItemButton.GetItem() != null)
        {
            ITEM_TYPE type = owner_selectItemButton.GetItem().GetItemType();
            changeCountWindow.SetMaxCount(managerItem.GetItem(type).GetCount());
        }

        // 支払い素材の個数を変更
        owner_selectItemButton.ChangeCountOfClickButton(changeCountWindow.GetCount());
        if (changeCountWindow.IsAplly())
        {
            owner_selectItemButton.FinishChangeCount();
        }

        // 合計値を更新する
        totalItemNumText.text = owner_selectItemButton.GetTotal().ToString();

    }

    public int GetTotal()
    {
        return owner_selectItemButton.GetTotal();
    }
}
