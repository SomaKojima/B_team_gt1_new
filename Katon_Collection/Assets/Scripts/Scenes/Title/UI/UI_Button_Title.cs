using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button_Title : UI_Button
{

    [SerializeField]
    UI_Button m_title_button = null;


    //タイトルボタンが押された
    public void ClickTitileButton()
    {
        m_title_button.IsClickEnter();
        m_title_button.OnClickProcess();
    }

    //タイトルボタンが押されたかを取得する

    public bool IsClickTitle()
    {
        return m_title_button.IsClick();
    }

}
