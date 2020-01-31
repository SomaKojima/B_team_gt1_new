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
    ITEM_TYPE[] randomBuf = new ITEM_TYPE[(int)ItemType.BuildingResourceMax];

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
            randomBuf[i] = (ITEM_TYPE)(i + (int)ItemType.HumanMax);
        }

        ShuffleRandomType();
    }

    //カウントの取得
    public List<IItem> GetItems(ITEM_TYPE _type, bool isDouble)
    {
        correctItems.Clear();

        ShuffleRandomType();

        // 確定枠
        if (_type != ITEM_TYPE.NONE)
        {
            int itemCount = GetItemCount(_type, isDouble);
            bufCorrectItems[(int)_type].SetCount(itemCount);
            correctItems.Add(bufCorrectItems[(int)_type]);
        }

        // 取得アイテムの素材を計算
        int i = 0;
        while (correctItems.Count < 3)
        {
            // 確定枠と同じ場合は飛ばす
            if (randomBuf[i] == _type)
            {
                i++;
                continue;
            }
            int itemCount = GetItemCount(randomBuf[i], isDouble);
            bufCorrectItems[(int)randomBuf[i]].SetCount(itemCount);
            correctItems.Add(bufCorrectItems[(int)randomBuf[i]]);
            i++;
        }

        return correctItems;
    }

    public void ShuffleRandomType()
    {
        for (int i = 0; i < 5; i++)
        {
            int maxNum = (int)ItemType.BuildingResourceMax;
            int randomOne = Random.Range(0, (maxNum - 1) * 100) % maxNum;
            int randomTwo = Random.Range(0, (maxNum - 1) * 100) % maxNum;

            // 入れ替え処理
            ITEM_TYPE buf = randomBuf[randomOne];
            randomBuf[randomOne] = randomBuf[randomTwo];
            randomBuf[randomTwo] = buf;
        }
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
        if (m_itemType == ITEM_TYPE.NONE) return false;
        return true;
    }
}
