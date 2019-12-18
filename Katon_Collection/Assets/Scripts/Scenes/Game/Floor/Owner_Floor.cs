using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_Floor : MonoBehaviour
{
    [SerializeField]
    Manager_Floor manager_floor;

    [SerializeField]
    Factory_Floor factory_floor;

    [SerializeField]
    float buildingOffsetY = 10.0f;

    [SerializeField, EnumListLabel(typeof(Type))]
    Transform[] createPosition = new Transform[(int)Type.Max];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Building(Type type)
    {
        Floor top = manager_floor.GetTopFloorOf(type);
        // 二階目以降を建てる
        if (top != null)
        {
            Vector3 position = top.transform.position;
            position.y += buildingOffsetY;
            manager_floor.Add(type, factory_floor.CreateFloor(position));
        }
        // 一階を建てる
        else
        {
            Vector3 position = createPosition[(int)type].position;
            position.y += buildingOffsetY;
            manager_floor.Add(type, factory_floor.CreateBase(position));
        }
    }
    
}
