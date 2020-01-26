using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PowerUp : MonoBehaviour
{
    [SerializeField]
    UI_Button powerUpButton;

    [SerializeField]
    PowerUpWindow powerUpWindow;

    bool isSetPlaceHuman = false;
    bool isActive = false;
    bool isChangeActive = false;

    public void Initialzie()
    {
        powerUpWindow.UnActive();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        powerUpWindow.Initialize(_type);
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

    public void UnActive()
    {
        if (!gameObject.activeSelf) return;
        gameObject.SetActive(false);
        powerUpWindow.UnActive();
    }
}
