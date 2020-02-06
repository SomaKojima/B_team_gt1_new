﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItemType
{
    static public ITEM_TYPE BuildintResourceToHuman(ITEM_TYPE _type)
    {
        // もともと人間のタイプの場合
        if (ItemType.IsHumanType(_type))
        {
            return _type;
        }

        // 資源のタイプの場合
        switch (_type)
        {
            case ITEM_TYPE.WOOD:
                return ITEM_TYPE.LOOGER;
            case ITEM_TYPE.ORE:
                return ITEM_TYPE.COAL_MINER;
            case ITEM_TYPE.PARTS:
                return ITEM_TYPE.ENGINEER;
            case ITEM_TYPE.WHEAT:
                return ITEM_TYPE.FARMER;
            case ITEM_TYPE.COTTON:
                return ITEM_TYPE.FARMER_COTTON;

        }
        return ITEM_TYPE.NONE;
    }

    static public ITEM_TYPE HumanToBuildingResource(ITEM_TYPE _type)
    {
        // もともと資源のタイプの場合
        if (ItemType.IsBuildingResourceType(_type))
        {
            return _type;
        }

        switch (_type)
        {
            case ITEM_TYPE.LOOGER:
                return ITEM_TYPE.WOOD;
            case ITEM_TYPE.COAL_MINER:
                return ITEM_TYPE.ORE;
            case ITEM_TYPE.ENGINEER:
                return ITEM_TYPE.PARTS;
            case ITEM_TYPE.FARMER:
                return ITEM_TYPE.WHEAT;
            case ITEM_TYPE.FARMER_COTTON:
                return ITEM_TYPE.COTTON;
        }

        return ITEM_TYPE.NONE;
    }

    static public ITEM_TYPE PlaceToItemType(Type _type)
    {
        switch (_type)
        {
            case Type.cave:
                return ITEM_TYPE.ORE;
            case Type.forest:
                return ITEM_TYPE.WOOD;
            case Type.factory:
                return ITEM_TYPE.PARTS;
            case Type.farm:
                return ITEM_TYPE.WHEAT;
            case Type.cotton:
                return ITEM_TYPE.COTTON;
        }

        return ITEM_TYPE.NONE;
    }
}
