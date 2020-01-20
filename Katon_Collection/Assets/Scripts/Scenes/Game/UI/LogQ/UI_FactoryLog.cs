using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FactoryLog : MonoBehaviour
{
    //ロゴのプレファブ
    [SerializeField]
    private GameObject m_prefab = null;

    //トランスフォーム
    [SerializeField]
    private Transform m_parent = null;

    float m_seconds;

    float m_minute;

    string st;

    //タイマー、テキスト生成
    public UI_Log Create(string _text, float _time, bool _isBigMode)
    {

        GameObject instance = Instantiate(m_prefab);
        instance.transform.SetParent(m_parent.transform, false);

        UI_Log log = instance.GetComponent<UI_Log>();


        //変換をしている処理
        int minute = (int)_time / 60;     //分
        //int minute = (int)_minute;

        float second = (int)_time % 60;   //秒

        //int msecond = (int)(_time * 1000 % 1000);

        st = minute.ToString() + ":" + second.ToString();// + msecond.ToString();


        log.Initialize(_text,st, _isBigMode);


        return log;
    }

   

 



   
}
