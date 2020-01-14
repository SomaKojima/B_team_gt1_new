using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LogWindow : MonoBehaviour
{
    const int MAX_LOG = 10;
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
        //UpdateMode(m_logType);
        if (m_uI_Manager.Logs.Count == 0)
        {
            m_image.gameObject.SetActive(false);
        }
    }

    //ロゴを追加する
    public void AddLog(string _text, float _time)
    {
        Debug.Log(_time);
        m_uI_Manager.Add(m_uI_FactoryLog.Create(_text, _time));

        if (m_uI_Manager.Logs.Count > MAX_LOG)
        {
            m_uI_Manager.Delete(0);
        }

        m_image.gameObject.SetActive(true);
    }

    //クリックイベント
    public void OnClick()
    {
        m_sizeFlag = !m_sizeFlag;


        if(m_logType == LogWindowType.Little)
        {
            m_logType = LogWindowType.Big;
        }
        else
        {
            m_logType = LogWindowType.Little;
        }


        //UpdateMode(m_logType);
    }

    private void UpdateMode(LogWindowType type)
    {
        if (type == LogWindowType.Little)
        {
            LittleMode();
        }

        if (type == LogWindowType.Big)
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
