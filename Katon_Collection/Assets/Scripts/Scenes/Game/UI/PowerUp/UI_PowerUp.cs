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
        if (powerUpButton.IsClick())
        {
            powerUpButton.OnClickProcess();
            isSetPlaceHuman = true;
            powerUpWindow.Active();
        }
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
}
