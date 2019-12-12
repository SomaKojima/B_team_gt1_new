using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button_Market : UI_Button
{
    // ボタン(セール)
    [SerializeField]
    UI_Button saleButton = null;
    // ボタン(コモン)
    [SerializeField]
    UI_Button commonButton = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// セールボタンが押された
    /// </summary>
    public void ClickSaleButton()
    {
        saleButton.IsClickEnter();
        commonButton.OnClickProcess();
    }
    /// <summary>
    /// コモンボタンが押された
    /// </summary>
    public void ClickCommonButton()
    {
        commonButton.IsClickEnter();
        saleButton.OnClickProcess();
    }

    /// <summary>
    /// セールボタンが押されたか
    /// </summary>
    /// <returns>押下状態</returns>
    public bool IsClickSale()
    {
        return saleButton.IsClick();
    }
    /// <summary>
    /// コモンボタンが押されたか
    /// </summary>
    /// <returns>押下状態</returns>
    public bool IsClickCommon()
    {
        return commonButton.IsClick();
    }
}
