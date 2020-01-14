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



    // 収集するアイテム
    List<IItem> correctItems = new List<IItem>();

    //収集のアイテムを決める時に使う変数
    List<IItem> bufCorrectItems = new List<IItem>();

    private void Start()
    {
        Initialize();
    }


    //初期化
    public void Initialize()
    {
        m_count = 0;
        for(int i = 0;  i < (int)ITEM_TYPE.NUM; i++)
        {
            Item _item = new Item();
            _item.Initialize(0, (ITEM_TYPE)i);
            bufCorrectItems.Add(_item);
        }
    }

    //カウントの取得
    public List<IItem> GetItems(ITEM_TYPE _type)
    {
        correctItems.Clear();
        bufCorrectItems[(int)ITEM_TYPE.WOOD].SetCount(10);
        correctItems.Add(bufCorrectItems[(int)ITEM_TYPE.WOOD]);
        return correctItems;
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

    /// <summary>
    /// 収集可能かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsCollectable(ITEM_TYPE _type)
    {
        return true;
    }
}
