using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button_Title : UI_Button
{

    //クリック
    private bool m_isClick = false;


    public void OnClick()
    {
        m_isClick = true;
    }


    //クリックフラグの取得
   public  bool IsClick()
    {
        return m_isClick;
    }

}
