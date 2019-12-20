using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_CommonUnitButton : MonoBehaviour
{
    [SerializeField]
    Factory_CommonUnitButton factory;
    [SerializeField]
    Manager_CommonUnitButton manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create(List<IItem> items, int requiredNum)
    {
        manager.Add(factory.Create(items, requiredNum));
    }
}
