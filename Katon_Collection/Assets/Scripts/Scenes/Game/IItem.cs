using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void AddCount(int _count, ITEM_TYPE _type);
    void SubCount(int _count, ITEM_TYPE _type);

    ITEM_TYPE GetItemType();
}
