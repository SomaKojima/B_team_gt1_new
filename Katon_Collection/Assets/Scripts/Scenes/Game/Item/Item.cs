using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    // 資源の種類
    private ITEM_TYPE type;
    // 資源の個数
    private int count = 0;

    // アイテム取得
    public Item GetItemInstance(){    return this;    }

    /// <summary>
    /// アイテムの値を初期化
    /// </summary>
    /// <param name="setType">設定するタイプ</param>
    /// <param name="setCount">設定する個数</param>
    /// <returns>アイテムのインスタンス</returns>
    public Item Initialize(ITEM_TYPE setType, int setCount)
    {
        Item itm = new Item();
        itm.type = setType;
        itm.count = setCount;

        return itm;
    }

    /// <summary>
    /// 資源を増やす
    /// </summary>
    /// <param name="_count">増やす数量</param>
    /// <param name="_type">増やすタイプ</param>
    public void AddCount(int _count, ITEM_TYPE _type)
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
    public void SubCount(int _count, ITEM_TYPE _type)
    {
        if (type == _type)
        {
            count -= _count;
        }
    }

    public ITEM_TYPE GetItemType() { return type; }

    public int GetItemCount() { return count; }
}
