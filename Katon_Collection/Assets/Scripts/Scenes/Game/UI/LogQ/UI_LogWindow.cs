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
    private RectTransform viewTransform;

    [SerializeField]
    private Image m_image = null;


    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    RectTransform bigModeRectTransform;


    //ロゴのタイム
    LogWindowType m_logType = LogWindowType.Little;

    // ログのスクロールを一番下にする
    bool isScrollUnder = false;

    Vector2 littleSize = Vector2.zero;

    Vector2 bigSize = Vector2.zero;

    // クリック中かどうか
    bool isClick = false;

    // スクロールをやめたかどうか
    bool isNotClick = false;

    bool isDrag = false;

    private void Start()
    {
        littleSize = GetComponent<RectTransform>().sizeDelta;
        bigSize = bigModeRectTransform.sizeDelta;

        UpdateMode(m_logType);
        if (m_uI_Manager.Logs.Count == 0)
        {
            m_image.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isScrollUnder)
        {
            isScrollUnder = false;
            scrollRect.verticalNormalizedPosition = 0;
        }

        isNotClick = false;
        if (Input.GetMouseButtonUp(0))
        {
            if (!isDrag && isClick)
            {
                m_sizeFlag = !m_sizeFlag;


                if (m_logType == LogWindowType.Little)
                {
                    m_logType = LogWindowType.Big;
                }
                else
                {
                    m_logType = LogWindowType.Little;
                }

                UpdateMode(m_logType);
            }
            isDrag = false;
            if (isClick)
            {
                isNotClick = true;
            }
            isClick = false;
        }
    }

    //ロゴを追加する
    public void AddLog(string _text, float _time)
    {
        m_uI_Manager.Add(m_uI_FactoryLog.Create(_text, _time,(m_logType == LogWindowType.Big)));

        if (m_uI_Manager.Logs.Count > MAX_LOG)
        {
            m_uI_Manager.Delete(0);
        }

        m_image.gameObject.SetActive(true);
        isScrollUnder = true;
    }
    

    private void UpdateMode(LogWindowType type)
    {
        //isScrollUnder = true;
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
        viewTransform.sizeDelta = bigSize;


        //m_image.rectTransform.sizeDelta = viewTransform.sizeDelta + new Vector2(100, 100);



        foreach (UI_Log log in m_uI_Manager.Logs)
        {
            log.BigMode();
        }

       
    }

    //スモールモード
    public void LittleMode()
    {
        //1447
        viewTransform.sizeDelta = littleSize;

        //m_image.rectTransform.sizeDelta = viewTransform.sizeDelta + new Vector2(50, 50);

        foreach (UI_Log log in m_uI_Manager.Logs)
        {
            //log.LittleMode();
        }
    }

    public void OnPointerDown()
    {
        isClick = true;
    }

    public void OnDrag()
    {
        isDrag = true;
    }

    public bool IsClick()
    {
        return isClick;
    }

    public bool IsNotClick()
    {
        return isNotClick;
    }
}
