﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
    

    //クリック
    bool m_click = false;

    public void Start()
    {
    }
    //クリックしたよ
    public void IsClickEnter()
    {
        m_click = true;
    }

    //クリックし終わった
    public void OnClickProcess()
    {
        m_click = false;
    }

    //クリックの状態の取得
    public bool IsClick()
    {
       return m_click; 
    }

   

}
