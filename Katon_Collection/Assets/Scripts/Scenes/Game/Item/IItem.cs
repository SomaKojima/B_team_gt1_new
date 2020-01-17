﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void Initialize(int _count, ITEM_TYPE _type);
    void AddCount(int _count);
    void SetCount(int count);
    void SetType(ITEM_TYPE _type);

    ITEM_TYPE GetItemType();
    int GetCount();
}
