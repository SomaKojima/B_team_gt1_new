﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_BuildingItemUnit : MonoBehaviour
{
    [SerializeField]
    Factory_BuildingItemUnit factory;

    [SerializeField]
    Manager_BuildingItemUnit manager;

    bool isFirstInitialize = true;
    
    public void Initialize()
    {
        if (isFirstInitialize)
        {
            isFirstInitialize = false;
            for (int i = 0; i < 4; i++)
            {
                Create(ITEM_TYPE.WOOD, 0);
            }
        }

        foreach (BuildingItemUnit unit in manager.GetUnits())
        {
            unit.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create(ITEM_TYPE _type, int _count)
    {
        manager.Add(factory.Create(_type, _count));
    }

    public void SetUnits(List<IItem> _items)
    {
        int i = 0;
        foreach (IItem item in _items)
        {
            if (i >= manager.GetUnits().Count) break;
            manager.GetUnits()[i].gameObject.SetActive(true);
            manager.GetUnits()[i].Initialize(item.GetItemType(), item.GetCount());
            i++;
        }
    }
}
