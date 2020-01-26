using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUnit : MonoBehaviour
{
    [SerializeField]
    CommonUnitIcon icon;

    [SerializeField]
    ItemContextTable itemContextTable;

    [SerializeField]
    UI_Button powerUpButton;

    ITEM_TYPE type;

    bool isPowerUp = false;
    bool isPointerEnter = false;

    public void Initialize(ITEM_TYPE _type)
    {
        type = _type;
        // アイコンの初期化
        icon.Initialize(itemContextTable.GetItemContex(_type).GetSprite(), 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isPowerUp = false;
        // 強化ボタンが押された
        if (powerUpButton.IsClick())
        {
            powerUpButton.OnClickProcess();
            isPowerUp = true;
        }
    }

    public bool IsPowerUp()
    {
        return isPowerUp;
    }

    public void OnPointerEnter()
    {
        isPointerEnter = true;
    }

    public void OnPointerExit()
    {
        isPointerEnter = false;
    }

    public ITEM_TYPE GetItemType()
    {
        return type;
    }

    public bool IsPointerEnter()
    {
        return isPointerEnter;
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
        isPointerEnter = false;
        isPowerUp = false;
    }
}
