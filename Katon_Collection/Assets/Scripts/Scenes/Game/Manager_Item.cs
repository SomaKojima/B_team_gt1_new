using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Item : MonoBehaviour
{
    private List<Item> itemList = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Item _item)
    {
        itemList.Add(_item);
    }

    /// <summary>
    /// 特定のアイテムを取得
    /// </summary>
    /// <param name="type">取得するアイテムの種類</param>
    /// <returns>アイテム</returns>
    public Item GetItem(ITEM_TYPE type)
    {
        return itemList[(int)type].GetItemInstance();
    }
}
