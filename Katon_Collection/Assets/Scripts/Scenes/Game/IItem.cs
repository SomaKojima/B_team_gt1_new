using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void AddCount(int _count, ItemType.ITEM_TYPE _type);
    void SubCount(int _count, ItemType.ITEM_TYPE _type);

    ItemType.ITEM_TYPE GetItemType();
}
