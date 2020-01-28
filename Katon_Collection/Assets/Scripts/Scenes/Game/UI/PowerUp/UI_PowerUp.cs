using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PowerUp : MonoBehaviour
{
    const int MAX_POWER_UP_RESOURCE_NUM = 10;
    [SerializeField]
    UI_Button powerUpButton;

    [SerializeField]
    PowerUpWindow powerUpWindow;

    bool isSetPlaceHuman = false;
    bool isActive = false;
    bool isChangeActive = false;

    List<IItem>[] necessaryItems = new List<IItem>[MAX_POWER_UP_RESOURCE_NUM];

    int total = 0;

    public void Initialzie()
    {
        powerUpWindow.UnActive();
        powerUpWindow.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)MAX_POWER_UP_RESOURCE_NUM; i++)
        {
            necessaryItems[i] = new List<IItem>();
            int cost = 0;
            switch (i)
            {
                case 0:
                    break;
                case 1:
                    necessaryItems[i].Add(new Item(-cost, ITEM_TYPE.WOOD));
                    break;
                case 2:
                    necessaryItems[i].Add(new Item(-cost, ITEM_TYPE.PARTS));
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        isSetPlaceHuman = false;
        isChangeActive = false;


        if (powerUpButton.IsClick())
        {
            powerUpButton.OnClickProcess();
            isSetPlaceHuman = true;

            if (powerUpWindow.gameObject.activeSelf)
            {
                powerUpWindow.UnActive();
            }
            else
            {
                powerUpWindow.SetResourceItems(necessaryItems[0]);
                powerUpWindow.Active();
            }
        }

        if (powerUpWindow.gameObject.activeSelf != isActive)
        {
            //Debug.Log(powerUpWindow.gameObject.activeSelf);
            isChangeActive = true;
        }
        isActive = powerUpWindow.gameObject.activeSelf;
    }

    public void SetPlaceHuman(List<ITEM_TYPE> _type)
    {
        powerUpWindow.SetUnit(_type);
    }

    public bool IsSetPlaceHuman()
    {
        return isSetPlaceHuman;
    }

    public bool IsPowerUp()
    {
        return powerUpWindow.IsPowerUp();
    }

    public ITEM_TYPE GetPowerUpItemType()
    {
        return powerUpWindow.GetPowerUpItemType();
    }

    public bool IsActive()
    {
        return isActive;
    }

    public bool IsChangeActive()
    {
        return isChangeActive;
    }

    public void Active()
    {
        if (gameObject.activeSelf) return;
        gameObject.SetActive(true);
    }

    public void SetTotalFloor(int totalFloor)
    {
        total = totalFloor;
        powerUpWindow.SetResourceItems(necessaryItems[GetFloorIndex(totalFloor)]);
    }

    public void UnActive()
    {
        if (!gameObject.activeSelf) return;
        gameObject.SetActive(false);
        powerUpWindow.UnActive();
    }

    public List<IItem> GetResources()
    {
        return necessaryItems[GetFloorIndex(total)];
    }

    int GetFloorIndex(int totalFloor)
    {
        int index = totalFloor;
        if (totalFloor < 0) index = 0;
        if (totalFloor >= MAX_POWER_UP_RESOURCE_NUM) index = MAX_POWER_UP_RESOURCE_NUM - 1;
        return index;
    }


    public void CorrectPowerUp()
    {
        powerUpWindow.CorrectPowerUp();
    }
}
