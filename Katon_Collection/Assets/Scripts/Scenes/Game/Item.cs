using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    // 資源の種類
    private ItemType.ITEM_TYPE type;
    // 資源の個数
    private int count = 0;

    // アイテム取得
    public Item GetItemInstance(){    return this;    }

    /// <summary>
    /// 資源を増やす
    /// </summary>
    /// <param name="_count">増やす数量</param>
    /// <param name="_type">増やすタイプ</param>
    public void AddCount(int _count, ItemType.ITEM_TYPE _type)
    {
        if(type == _type)
        {
            count += _count;
        }
    }

    /// <summary>
    /// 資源を減らす
    /// </summary>
    /// <param name="_count">減らす数量</param>
    /// <param name="_type">減らすタイプ</param>
    public void SubCount(int _count, ItemType.ITEM_TYPE _type)
    {
        if (type == _type)
        {
            count -= _count;
        }
    }

    public ItemType.ITEM_TYPE GetItemType()
    {
        return type;
    }
}
