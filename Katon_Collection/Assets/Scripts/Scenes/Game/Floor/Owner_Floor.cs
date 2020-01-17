using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_Floor : MonoBehaviour
{
    const int MAX_BUILDING_RESOURCE_NUM = 4;

    [SerializeField]
    Manager_Floor[] manager_floor = null;

    [SerializeField]
    Factory_Floor factory_floor;

    [SerializeField]
    float buildingOffsetY = 10.0f;

    [SerializeField, EnumListLabel(typeof(Type))]
    Transform[] createPosition = new Transform[(int)Type.Max];

    List<IItem>[,] necessaryItems = new List<IItem>[(int)Type.Max, MAX_BUILDING_RESOURCE_NUM];

    Floor_Encode floorEncode = new Floor_Encode();

    bool isFirstBuilding = false;

    int totalFloor = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        manager_floor = new Manager_Floor[(int)Type.Max];
        for (int i = 0; i < (int)Type.Max; i++)
        {
            manager_floor[i] = new Manager_Floor();
        }

        for (int i = 0; i < (int)Type.Max; i++)
        {
            for (int j = 0; j < MAX_BUILDING_RESOURCE_NUM; j++)
            {
                necessaryItems[i, j] = new List<IItem>();

                //// 建築時に必要な資源を決める
                //if (j >= 1)
                //{
                //    Item item = new Item();
                //    item.Initialize(0, ITEM_TYPE.NONE);
                //    item.Initialize(-1000, ITEM_TYPE.WOOD);
                //    necessaryItems[i, j].Add(item);
                //}
                Set(i, j);
            }
        }
    }

    void Set(int i, int j)
    {
        switch (j)
        {
            case 0:
                break;
            case 1:
                necessaryItems[i, j].Add(new Item(-100, ITEM_TYPE.ORE));
                break;

            case 2:
                necessaryItems[i, j].Add(new Item(-150, ITEM_TYPE.ORE));
                necessaryItems[i, j].Add(new Item(-30, ITEM_TYPE.WOOD));
                break;
            case 3:
                necessaryItems[i, j].Add(new Item(-250, ITEM_TYPE.ORE));
                necessaryItems[i, j].Add(new Item(-100, ITEM_TYPE.PARTS));
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Building(Type type)
    {
        if (type == Type.none) return;
        Floor top = manager_floor[(int)type].GetTopFloorOf();
        // 二階目以降を建てる
        if (top != null)
        {
            Vector3 position = top.transform.position;
            position.y += buildingOffsetY;
            manager_floor[(int)type].Add(factory_floor.CreateFloor(position));
        }
        // 一階を建てる
        else
        {
            Vector3 position = createPosition[(int)type].position;
            position.y += buildingOffsetY;
            manager_floor[(int)type].Add(factory_floor.CreateBase(position));
        }
        isFirstBuilding = true;
        totalFloor++;

        // 開拓に必要な資源を更新
        UpdateReclamation(totalFloor);
    }

    public List<IItem> GetBuildingResource(Type _type)
    {
        if (_type == Type.none) return null;
        int index = manager_floor[(int)_type].Floors.Count;

        if (index < 0) return null;
        if (index >= MAX_BUILDING_RESOURCE_NUM) return necessaryItems[(int)_type, MAX_BUILDING_RESOURCE_NUM - 1];

        return necessaryItems[(int)_type, index];
    }

    public bool IsFirstBuilding()
    {
        return isFirstBuilding;
    }

    public int GetTotalFloor()
    {
        return totalFloor;
    }

    /// <summary>
    /// 開拓に必要な資源を更新
    /// </summary>
    void UpdateReclamation(int _totalFloor)
    {
        for (int i = 0; i < (int)Type.Max; i++)
        {
            necessaryItems[i, 0].Clear();
            int count = 200 * _totalFloor;
            necessaryItems[i, 0].Add(new Item(-count, ChangeItemType.PlaceToItemType((Type)i)));
        }
    }
}
