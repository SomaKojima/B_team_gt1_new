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
            currentItem.GetItem(button.GetItem().GetItemType()).SetCount(button.GetItem().GetCount());
            if (button.IsClick())
            {
                button.OnClickProcess();
                if (clickButton == null)
                {
                    isClick = true;
                    clickButton = button;
                }
            }
                total += button.GetItem().GetCount();
        }
    }

    public void Create(ITEM_TYPE type)
    {
        manager.Add(factory.Create(type));
    }

    public void ChangeCountOfClickButton(int count)
    {
        if (clickButton != null)
        {
            clickButton.GetItem().SetCount(count);
        }
    }

    public void FinishChangeCount()
    {
        clickButton = null;
    }

    public IItem GetItem()
    {
        if (clickButton == null) return null;
        return clickButton.GetItem();
    }
    
    public bool IsClick()
    {
        return isClick;
    }

    public int GetTotal()
    {
        return total;
    }

    public Manager_Item GetManagerItem()
    {
        return currentItem;
    }
}
