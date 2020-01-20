using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_BuildingResource : MonoBehaviour
{
    [SerializeField]
    Manager_BuildingResources manager;

    [SerializeField]
    Factory_BuildingResources factory;

    [SerializeField, EnumListLabel(typeof(Type))]
    BuildingResources[] br;

    public void Initialize()
    {
        manager.Initialize();

        for (int i = 0; i < br.Length; i++)
        {
            if (br[i] == null) continue;
            manager.Add(br[i]);
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

    public BuildingResources GetBuildingResource(Type _type)
    {
        return manager.GetListOf(_type)[0];
    }
    
}
