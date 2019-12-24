using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_Human : MonoBehaviour
{
    [SerializeField]
    Manager_Human manager_human;

    [SerializeField]
    Factory_Human factory_human;

    [SerializeField]
    BoxCollider foutaninCreatePosition;

    [SerializeField]
    BoxCollider shopCreatePosition;

    bool isShop;

    public void Intialize()
    {
        manager_human.Initialize();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MatchItemsHumans(IItem items, bool isShop)
    {
        if (isShop)
        {
            MatchItemsHumans(items,shopCreatePosition);
        }
        else
        {
            MatchItemsHumans(items,foutaninCreatePosition);
        }
    }

    void MatchItemsHumans(IItem item, BoxCollider collider)
    {
        MatchItemsHumans(item,
            collider.gameObject.transform.position,
            collider.size.x,
            collider.size.y,
            collider.size.z);
    }

    // アイテムマネージャーと実体の人間の数を同じにする(人間を生成・削除する)
    void MatchItemsHumans(IItem item, Vector3 position, float width, float height, float depth)
    {
        ITEM_TYPE type = item.GetItemType();
        int differenceCount = item.GetCount() - manager_human.GetListOf(type).Count;
        // 追加
        if (differenceCount > 0)
        {
            for (int j = 0; j < differenceCount; j++)
            {
                manager_human.Add(factory_human.CreateRandomPosition(position, width, height, depth, type));
            }
        }
        // 削除
        else if (differenceCount < 0)
        {
            Debug.Log(differenceCount);
            manager_human.Delete(type, -differenceCount);
        }
    }
}
