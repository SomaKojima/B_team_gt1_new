using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBoard : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    Owner_BuildingItemUnit owner_BuildingItemUnit;

    // Start is called before the first frame update
    void Start()
    {
        owner_BuildingItemUnit.Initialize();
        for (int i = 0; i < 4; i++)
        {
            owner_BuildingItemUnit.Create(ITEM_TYPE.WOOD, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
