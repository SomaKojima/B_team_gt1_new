using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    // 資源の種類
    private ITEM_TYPE type;
    // 資源の個数
    private int count = 0;

    public void Initialize(int _count, ITEM_TYPE _type)
    {
        count = _count;
        type = _type;
    }

    /// <summary>
    /// 資源を増やす
    /// </summary>
    /// <param name="_count">増やす数量</param>
    /// <param name="_type">増やすタイプ</param>
    public void AddCount(int _count)
    {
        count += _count;

        if (count < 0)
        {
            count = 0;
        }
    }

    public ITEM_TYPE GetItemType()
    {
        return type;
    }

    public int GetCount()
    {
        return count;
    }

    public void SetCount(int _count)
    {
        count = _count;
    }
}
