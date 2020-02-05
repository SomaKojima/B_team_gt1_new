using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    // 資源の種類
    private ITEM_TYPE type;
    // 資源の個数
    private int count = 0;

    int powerUpCount = 0;

    public Item()
    {

    }

    public Item(int _count, ITEM_TYPE _type)
    {
        Initialize(_count, _type);
    }

    public Item(int _normal, int _powerUp, ITEM_TYPE _type)
    {
        Initialize(_normal, _powerUp, _type);
    }

    public void Initialize(int _count, ITEM_TYPE _type)
    {
        count = _count;
        type = _type;
    }


    public void Initialize(int _normal, int _powerUp, ITEM_TYPE _type)
    {
        SetCount(_normal + _powerUp);
        SetPowerUpCount(_powerUp);

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
    }

    public void AddPowerUpCount(int _count)
    {
        SetPowerUpCount(powerUpCount + _count);
    }

    public void AddNormalCount(int _count)
    {
        SetNormalCount(GetNormalCount() + _count);
    }

    /// <summary>
    /// 強化する
    /// </summary>
    /// <param name="_count"></param>
    public void SetPowerUpCount(int _count)
    {
        powerUpCount = _count;
    }

    public void SetNormalCount(int _count)
    {
        count = _count + powerUpCount;

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
        if (count < powerUpCount) SetPowerUpCount(count);
    }

    public void SetType(ITEM_TYPE _type)
    {
        type = _type;
    }

    public int GetPowerUpCount()
    {
        return powerUpCount;
    }

    public int GetNormalCount()
    {
        return count - powerUpCount;
    }

    public void ClearCount()
    {
        SetPowerUpCount(0);
        SetNormalCount(0);
        SetCount(0);
    }
}
