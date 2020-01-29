using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 資源の種類
public enum ITEM_TYPE
{

    RANDOM = -2,
    /// </summary>
    NONE = -1,
    LOOGER,             // 木こり
    COAL_MINER,         // 炭鉱夫
    ENGINEER,           // エンジニア
    FARMER,             // 農家
    FARMER_COTTON,      // 綿農家

    WOOD,               // 木
    ORE,                // 鉱石
    PARTS,              // 部品
    WHEAT,              // 麦
    COTTON,             // 綿

    NUM
}

public class ItemType
{

    static public ITEM_TYPE HumanMax
    {
        get { return ITEM_TYPE.FARMER_COTTON + 1; }
    }

    static public ITEM_TYPE BuildingResourceMax
    {
        get { return ITEM_TYPE.COTTON + 1 - (int)HumanMax; }
    }
}
