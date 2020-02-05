using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory_PowerUpUnit : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    RectTransform parent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PowerUpUnit Create(ITEM_TYPE _type)
    {
        GameObject instance = Instantiate(prefab, parent);

        PowerUpUnit unit = instance.GetComponent<PowerUpUnit>();

        unit.Initialize(_type);

        return unit;
    }
}
