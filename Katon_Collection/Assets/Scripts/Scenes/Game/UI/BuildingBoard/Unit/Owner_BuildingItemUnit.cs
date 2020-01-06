using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_BuildingItemUnit : MonoBehaviour
{
    [SerializeField]
    Factory_BuildingItemUnit factory;

    [SerializeField]
    Manager_BuildingItemUnit manager;


    public void Initialize()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create(ITEM_TYPE _type, int _count)
    {
        manager.Add(factory.Create(_type, _count));
    }
}
