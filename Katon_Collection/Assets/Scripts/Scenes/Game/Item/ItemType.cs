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

    WOOD,               // 木

    HUMAN_NUM = WOOD,   // 人間の数

    ORE,                // 鉱石
    PARTS,              // 部品
    WHEAT,              // 麦

    NUM,
    BUILDING_RESOURCE_NUM = NUM - HUMAN_NUM // 資源の数
}
