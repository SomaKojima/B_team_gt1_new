using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_PowerUpUnit : MonoBehaviour
{
    [SerializeField]
    Manager_PowerUpUnit manager;

    [SerializeField]
    Factory_PowerUpUnit factory;
    
    bool isPowerUp = false;

    PowerUpUnit powerUpUnits = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isPowerUp = false;
        foreach (PowerUpUnit unit in manager.Units)
        {
            if (unit.IsPowerUp())
            {
                powerUpUnits = unit;
                isPowerUp = true;
            }
        }
    }

    public void Create(ITEM_TYPE _type)
    {
        manager.Add(factory.Create(_type));
    }

    public void AllDestroy()
    {
        manager.AllDestory();
    }

    public bool IsPowerUp()
    {
        return isPowerUp;
    }

    public ITEM_TYPE GetPowerUpItemType()
    {
        if (powerUpUnits == null) return ITEM_TYPE.NONE;
        return powerUpUnits.GetItemType();
    }
}
