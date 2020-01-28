using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWindow : MonoBehaviour
{
    [SerializeField]
    Owner_PowerUpUnit owner_powerUpUnit;

    [SerializeField]
    Owner_BuildingItemUnit resourceUnit;

    bool isClick = false;
    bool isClickPowerUp = false;

    bool isClickEnterWindow = false;
    RectTransform rectTransform;


    public void Initialize()
    {
        
        resourceUnit.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        isClickEnterWindow = false;
        if (!isClick && !owner_powerUpUnit.IsPointerEnter())
        {
            Debug.Log("a");
            isClickEnterWindow = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isClick = false;
        }
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
        isClick = false;
    }

    public bool IsPowerUp()
    {
        return owner_powerUpUnit.IsPowerUp();
    }

    public ITEM_TYPE GetPowerUpItemType()
    {
        return owner_powerUpUnit.GetPowerUpItemType();
    }

    public void OnPointerDown()
    {
        isClick = true;
    }

    public void OnPointerEnter()
    {
        isClick = true;
    }

    public void OnPointerExit()
    {
        isClick = false;
    }

    public bool IsClick()
    {
        return isClick;
    }

    public bool IsClickEnterWindow()
    {
        return isClickEnterWindow;
    }

    public void SetResourceItems(List<IItem> item)
    {
        resourceUnit.SetUnits(item);
    }

    public void SetUnit(List<ITEM_TYPE> _itemTypes)
    {
        owner_powerUpUnit.AllDestroy();
        foreach (ITEM_TYPE type in _itemTypes)
        {
            owner_powerUpUnit.Create(type);
        }
    }


    public void CorrectPowerUp()
    {
        owner_powerUpUnit.CorrectPowerUp();
    }
}
