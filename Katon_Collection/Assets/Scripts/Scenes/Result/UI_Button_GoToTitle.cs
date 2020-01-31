using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button_GoToTitle : UI_Button
{
    //リザルトからタイトルボタンへ遷移する
    [SerializeField]
    private UI_Button m_goto_result_button = null;


    private void Start()
    {
        m_goto_result_button.gameObject.SetActive(false);
    }

    public void ClickgotoResultButton()
    {
        m_goto_result_button.IsClickEnter();
        m_goto_result_button.OnClickProcess();
    }


    public bool IsClickGotoTitle()
    {
        return m_goto_result_button.IsClick();
    }


}
