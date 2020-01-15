using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 資源の種類
public enum ITEM_TYPE
{
    NONE = -1,
    LOOGER,
    COAL_MINER,
    ENGINEER,

    WOOD,
    HUMAN_NUM = WOOD,   // 人間の最後の値はWOOD
    ORE,
    PARTS,

    NUM,
    BUILDING_RESOURCE_NUM = NUM - HUMAN_NUM
}
