using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_BuildingItemUnit : MonoBehaviour
{
    List<BuildingItemUnit> units = new List<BuildingItemUnit>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(BuildingItemUnit _unit)
    {
        units.Add(_unit);
    }

    public List<BuildingItemUnit> GetUnits()
    {
        return units;
    }
}
