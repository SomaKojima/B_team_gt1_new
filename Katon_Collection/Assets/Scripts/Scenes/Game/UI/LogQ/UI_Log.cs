using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Log : MonoBehaviour
{
    //テキスト
    [SerializeField]
    private Text m_text = null;

    //タイム
    [SerializeField]
    private Text m_time = null;

    //タイムを表示するかしないかを判断するフラグ
    bool m_timeActiveFlag = false;

    
    //初期化
    public void Initialize(string _text,string _time)
    {
        m_text.text = _text;
        m_time.text = _time.ToString();

        
    }

    //大きいウィンドウ
    public void BigMode()
    {

        m_timeActiveFlag = true;
        if (m_time == null) return;

        m_time.gameObject.SetActive(true);

   


    }

    //小さいウィンドウ
    public void LittleMode()
    {
        if (m_time == null) return;

        m_time.gameObject.SetActive(false);


    }




    //テキストの取得
    public string Text
    {
        get { return m_text.text; }
        set { m_text.text = value; }
    }

    
}
