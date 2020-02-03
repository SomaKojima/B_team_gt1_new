using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void Initialize(int _count, ITEM_TYPE _type);
    
    void AddCount(int _count);
    void AddPowerUpCount(int _count);
    void AddNormalCount(int _count);
    void SetCount(int count);
    void SetType(ITEM_TYPE _type);
    void SetPowerUpCount(int _count);
    void SetNormalCount(int _count);

    ITEM_TYPE GetItemType();
    int GetCount();
    int GetPowerUpCount();
    int GetNormalCount();

    void ClearCount();
}
