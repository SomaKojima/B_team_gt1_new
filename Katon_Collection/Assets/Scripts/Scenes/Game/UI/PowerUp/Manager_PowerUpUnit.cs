using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_PowerUpUnit : MonoBehaviour
{
    List<PowerUpUnit> units = new List<PowerUpUnit>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Add(PowerUpUnit _unit)
    {
        units.Add(_unit);
    }

    // すべてを消す
    public void AllDestory()
    {
        foreach (PowerUpUnit units in units)
        {
            Destroy(units.gameObject);
        }

        units.Clear();
    }

    public List<PowerUpUnit> Units
    {
        get { return units; }
    }
}
