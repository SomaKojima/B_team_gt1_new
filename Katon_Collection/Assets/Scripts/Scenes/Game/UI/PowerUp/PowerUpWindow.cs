using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWindow : MonoBehaviour
{
    [SerializeField]
    Owner_PowerUpUnit owner_powerUpUnit;

    public void Initialize(List<ITEM_TYPE> _itemTypes)
    {
        owner_powerUpUnit.AllDestroy();
        foreach (ITEM_TYPE type in _itemTypes)
        {
            owner_powerUpUnit.Create(type);
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

    public void Active()
    {
        if (this.gameObject.activeSelf) return;
        this.gameObject.SetActive(true);
    }

    public void UnActive()
    {
        if (!this.gameObject.activeSelf) return;
        this.gameObject.SetActive(false);
    }

    public bool IsPowerUp()
    {
        return owner_powerUpUnit.IsPowerUp();
    }

    public ITEM_TYPE GetPowerUpItemType()
    {
        return owner_powerUpUnit.GetPowerUpItemType();
    }
}
