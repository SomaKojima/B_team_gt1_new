using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 資源の種類
public enum ITEM_TYPE
{
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

    NUM,


    RANDOM,
    RANDOM_HUMAN,
    RANDOM_BUILIDNG_RESOURCE,

}

public class ItemType
{

    // 他の要素の最大を取得
    static public ITEM_TYPE OtherMax
    {
        get { return ITEM_TYPE.RANDOM_BUILIDNG_RESOURCE; }
    }

    // 人間のタイプの先頭を返す
    static public ITEM_TYPE HumanHead
    {
        get { return 0; }
    }

    // 人間のタイプの最後尾を返す
    static public ITEM_TYPE HumanTail
    {
        get { return ITEM_TYPE.FARMER_COTTON; }
    }

    // 人間のタイプの数を返す
    static public int HumanMax
    {
        get { return (int)HumanTail + 1; }
    }

    static public ITEM_TYPE BuildingResourceHead
    {
        get { return ITEM_TYPE.WOOD; }
    }

    static public ITEM_TYPE BuildingResourceTail
    {
        get { return ITEM_TYPE.COTTON; }
    }

    // 資源の最大値を取得
    static public int BuildingResourceMax
    {
        get { return (int)ITEM_TYPE.COTTON + 1 - (int)HumanMax; }
    }


    ///------------------------------------------------------------------------------------------
    /// 以下は判定や取得の関数
    ///------------------------------------------------------------------------------------------

    /// <summary>
    /// 人間のタイプか判定
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    static public bool IsHumanType(ITEM_TYPE type)
    {
        if (type >= HumanHead &&  type < HumanTail) return true;
        return false;
    }

    /// <summary>
    /// 資源のタイプか判定
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    static public bool IsBuildingResourceType(ITEM_TYPE type)
    {
        if (type >= BuildingResourceHead && type <= BuildingResourceTail ) return true;
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

    /// <summary>
    /// ランダムでタイプを取得
    /// </summary>
    /// <returns></returns>
    static public ITEM_TYPE RandomType()
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

    /// <summary>
    /// 資源のタイプをランダムで取得
    /// </summary>
    /// <returns></returns>
    static public ITEM_TYPE RandomBuildingResource()
    {
        return (ITEM_TYPE)Random.Range((int)ITEM_TYPE.WOOD, (int)ItemType.BuildingResourceMax);
    }
}
