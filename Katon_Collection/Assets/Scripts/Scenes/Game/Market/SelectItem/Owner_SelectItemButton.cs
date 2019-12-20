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
}
