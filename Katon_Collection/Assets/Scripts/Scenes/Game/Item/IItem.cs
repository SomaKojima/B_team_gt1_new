using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void Initialize(int _count, ITEM_TYPE _type);
    void AddCount(int _count);

    ITEM_TYPE GetItemType();
    int GetCount();
}
