using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    // 初期化
    Item Initialize(ITEM_TYPE setType, int setCount);

    // 個数を調整
    void AddCount(int _count, ITEM_TYPE _type);
    void SubCount(int _count, ITEM_TYPE _type);

    // 値取得関数
    ITEM_TYPE GetItemType();
    int GetItemCount();
}
