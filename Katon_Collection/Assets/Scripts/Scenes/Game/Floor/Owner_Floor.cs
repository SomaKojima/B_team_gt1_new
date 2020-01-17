using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_Floor : MonoBehaviour
{
    const int MAX_BUILDING_RESOURCE_NUM = 2;

    [SerializeField]
    Manager_Floor[] manager_floor = null;

    [SerializeField]
    Factory_Floor factory_floor;

    [SerializeField]
    float buildingOffsetY = 10.0f;

    [SerializeField, EnumListLabel(typeof(Type))]
    Transform[] createPosition = new Transform[(int)Type.Max];

    List<IItem>[,] itemString = new List<IItem>[(int)Type.Max, MAX_BUILDING_RESOURCE_NUM];

    Floor_Encode floorEncode = new Floor_Encode();

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
                itemString[i, j] = new List<IItem>();
                Item item = new Item();
                item.Initialize(0, ITEM_TYPE.WOOD);
                if(j >= 1) item.Initialize(-10000, ITEM_TYPE.WOOD);
                itemString[i, j].Add(item);
            }
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
    }

    public List<IItem> GetBuildingResource(Type _type)
    {
        int index = manager_floor[(int)_type].Floors.Count;

        if (index < 0) return null;
        if (index >= MAX_BUILDING_RESOURCE_NUM) return itemString[(int)_type, MAX_BUILDING_RESOURCE_NUM - 1];
        Debug.Log(index);
        return itemString[(int)_type, index];
    }
}
