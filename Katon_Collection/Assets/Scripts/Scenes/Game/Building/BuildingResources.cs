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

    // 収集アイテムのタイプを決めるときに使う変数
    int[] randomBuf = new int[(int)ItemType.BuildingResourceMax];

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

        // 初期化
        for (int i = 0; i < (int)ItemType.BuildingResourceMax; i++)
        {
            randomBuf[i] = i + (int)ItemType.BuildingResourceHead;
        }
        
    }

    //カウントの取得
    public List<IItem> GetItems(ITEM_TYPE _type, bool isDouble)
    {
        correctItems.Clear();

        // 確定枠
        if (_type != ITEM_TYPE.NONE)
        {
            ITEM_TYPE type = ChangeItemType.HumanToBuildingResource(_type);
            int itemCount = GetItemCount(type, isDouble);
            bufCorrectItems[(int)type].SetCount(itemCount);
            correctItems.Add(bufCorrectItems[(int)type]);
        }

        // 取得アイテムの素材を計算
        
        ShuffleArray.shuffle(randomBuf, randomBuf.Length);
        for (int i = 0; correctItems.Count < 3; i++)
        {
            // 確定枠と同じ場合は飛ばす
            if ((ITEM_TYPE)randomBuf[i] == _type)
            {
                continue;
            }
            int itemCount = GetItemCount((ITEM_TYPE)randomBuf[i], isDouble);
            bufCorrectItems[randomBuf[i]].SetCount(itemCount);
            correctItems.Add(bufCorrectItems[randomBuf[i]]);
        }

        return correctItems;
    }

    private int GetItemCount(ITEM_TYPE _type, bool isDouble)
    {
        // 取得数の計算
        int itemCount = 1;
        if (_type == m_itemType)
        {
            itemCount = 5;
        }
        if (isDouble)
        {
            return itemCount * 2;
        }
        return itemCount;
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
        if (ItemType.IsHumanType(m_itemType)) return false;
        return true;
    }
}
