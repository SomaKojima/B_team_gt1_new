using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LogWindow : MonoBehaviour
{
  
    //フラグ
    bool m_sizeFlag = false;

    //マネージャー
    [SerializeField]
    private UI_ManagerLog m_uI_Manager = null;

    //ファクトリー
    [SerializeField]
    private UI_FactoryLog m_uI_FactoryLog = null;

    //ログスクロール(マスクサイズを取得するために使用)
    [SerializeField]
    private LogScroll m_log_Scroll = null;

    [SerializeField]
    private Image m_image = null;


    //ロゴのタイム
    LogWindowType m_logType = LogWindowType.Little;

    private void Start()
    {
        AddLog("444", 6000);
        AddLog("444", 3000);


    }

    //ロゴを追加する
    public void AddLog(string _text,float _time)
    {
        m_uI_Manager.Add(m_uI_FactoryLog.Create(_text, _time));

    }

    //クリックイベント
    public void OnClick()
    {
        m_sizeFlag = !m_sizeFlag;


        if(m_sizeFlag)
        {
            m_logType = LogWindowType.Little;
        }
        else
        {
            m_logType = LogWindowType.Big;
        }

        if (m_logType == LogWindowType.Little)
        {
            LittleMode();
        }

        if (m_logType == LogWindowType.Big)
        {
            BigMode();
        }

      


    }

    //ビッグモード
    public void BigMode()
    {


        m_log_Scroll.GetMask.sizeDelta = new Vector2(540, 1447);


        m_image.rectTransform.sizeDelta = m_log_Scroll.GetMask.sizeDelta + new Vector2(100, 100);



        foreach (UI_Log log in m_uI_Manager.Logs)
        {
            log.BigMode();
        }

       
    }

    //スモールモード
    public void LittleMode()
    {

        //1447
        m_log_Scroll.GetMask.sizeDelta = new Vector2(540, 160);

        m_image.rectTransform.sizeDelta = m_log_Scroll.GetMask.sizeDelta + new Vector2(50, 50);

        foreach (UI_Log log in m_uI_Manager.Logs)
        {
            log.LittleMode();
        }

      
    }
}
