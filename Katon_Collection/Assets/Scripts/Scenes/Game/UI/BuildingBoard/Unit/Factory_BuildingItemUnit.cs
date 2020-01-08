using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_BuildingItemUnit : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Transform parent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BuildingItemUnit Create(ITEM_TYPE _type, int _count)
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(parent, false);

        BuildingItemUnit unit = instance.GetComponent<BuildingItemUnit>();
        unit.Initialize(_type, _count);

        return unit;
    }
}
