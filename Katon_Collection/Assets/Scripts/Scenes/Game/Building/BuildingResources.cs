using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResources : MonoBehaviour
{
    //アイテムタイプ
    [SerializeField]
    private ITEM_TYPE m_itemType;

    //場所タイプ
    [SerializeField]
    private Type m_placeType;

    //カウント
    int m_count;


    //初期化
    public void Initialize()
    {
        m_count = 0;

    }

    //カウントの取得
    public int GetCount()
    {
        return m_count;
    }

    //アイテムタイプの取得
    public ITEM_TYPE GetItemType()
    {
        return m_itemType;
    }

    //場所タイプの取得
    public Type GetPlaceType()
    {
        return m_placeType;
    }
}
