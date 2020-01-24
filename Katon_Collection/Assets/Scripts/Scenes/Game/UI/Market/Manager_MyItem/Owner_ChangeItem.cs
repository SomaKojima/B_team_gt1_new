using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_ChangeItem : MonoBehaviour
{
    [SerializeField]
    Factory_ChangeItem factory;

    [SerializeField]
    Manager_ChangeItem manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create(ITEM_TYPE type, int count)
    {
        manager.Add(factory.Create(type, count));
    }
}
