using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 資源の種類
public enum ITEM_TYPE
{
    RANDOM = -3,
    RANDOM_HUMAN =-2,
    RANDOM_BUILIDNG_RESOURCE = -1,

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

    static public bool IsHumanType(ITEM_TYPE type)
    {
        if (type < ITEM_TYPE.WOOD) return true;
        return false;
    }

    static public bool IsBuildingResourceType(ITEM_TYPE type)
    {
        if (type >= ITEM_TYPE.WOOD) return true;
        return false;
    }

    /// <summary>
    /// 人間のタイプがランダムで入っている配列を取得
    /// タイプは被らない
    /// 配列の長さは人間の種類数
    /// </summary>
    /// <returns></returns>
    static public int[] RandomHumanArray()
    {
        int[] randomArray = new int[(int)ItemType.HumanMax];
        for (int i = 0; i < (int)ItemType.HumanMax; i++)
        {
            randomArray[i] = i;
        }
        ShuffleArray.shuffle(randomArray, (int)ItemType.HumanMax);
        return randomArray;
    }

    static public ITEM_TYPE Random()
    {
        return (ITEM_TYPE)Random.Range(0, (int)ITEM_TYPE.NUM);
    }

    /// <summary>
    /// 人間のタイプをランダムで取得
    /// </summary>
    /// <returns></returns>
    static public ITEM_TYPE RandomHuman()
    {
        return (ITEM_TYPE)Random.Range(0, (int)ItemType.HumanMax);
    }

    static public ITEM_TYPE RandomBuildingResource()
    {
        return (ITEM_TYPE)Random.Range((int)ITEM_TYPE.WOOD, (int)ItemType.);
    }
}
