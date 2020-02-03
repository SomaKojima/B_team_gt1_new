using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_SelectItemsButton : MonoBehaviour
{
    List<SelectItemsButton> itemList = new List<SelectItemsButton>();
    List<SelectItemsButton> humanList = new List<SelectItemsButton>();
    List<SelectItemsButton> brList = new List<SelectItemsButton>();

    [SerializeField]
    SelectItemsButton selectItmBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(SelectItemsButton btn)
    {
        itemList.Add(btn);
        if (ItemType.IsHumanType(btn.GetItem().GetItemType()))
        {
            humanList.Add(btn);
        }
        else
        {
            brList.Add(btn);
        }
    }

    public List<SelectItemsButton> GetItemList()
    {
        return itemList;
    }
}
