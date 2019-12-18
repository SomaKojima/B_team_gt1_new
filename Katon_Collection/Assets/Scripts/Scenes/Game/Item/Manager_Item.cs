using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Item : MonoBehaviour
{
    private Item[] item = new Item[(int)ITEM_TYPE.NUM];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Initialize()
    {
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            item[i] = new Item();
            item[i].Initialize(0, (ITEM_TYPE)i);
        }
    }

    /// <summary>
    /// 特定のアイテムを取得
    /// </summary>
    /// <param name="type">取得するアイテムの種類</param>
    /// <returns>アイテム</returns>
    public Item GetItem(ITEM_TYPE type)
    {
        return item[(int)type];
    }
}
