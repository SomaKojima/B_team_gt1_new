using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_BuildingResources : MonoBehaviour
{
    //建材のリスト
    List<BuildingResources>[] brs = new List<BuildingResources>[(int)Type.Max];

    List<BuildingResources> all = new List<BuildingResources>();


    public void Initialize()
    {
        for (int i = 0; i < (int)Type.Max; i++)
        {
            brs[i] = new List<BuildingResources>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        all.Clear();
        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            all.AddRange(brs[i]);
        }
    }

    //リストへの追加
    public void Add(BuildingResources _buildingResources)
    {
        brs[(int)_buildingResources.GetPlaceType()].Add(_buildingResources);
    }


    public void Delete(Type type, int count)
    {
        List<BuildingResources> list = GetListOf(type);
        Debug.Log(list.Count);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Destroy(list[i].gameObject);
            list.RemoveAt(i);
            count--;
            if (count < 0) break;
        }
    }

    //建材の取得
    public List<BuildingResources> GetList()
    {
        return all;
    }

    public List<BuildingResources> GetListOf(Type type)
    {
        return brs[(int)type];
    }
}
