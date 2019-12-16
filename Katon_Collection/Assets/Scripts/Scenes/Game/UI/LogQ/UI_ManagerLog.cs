using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ManagerLog : MonoBehaviour
{
   

    //ロゴのリスト
    [SerializeField]
    private List<UI_Log> logs = new List<UI_Log>();

    //ロゴリストに追加
    public void Add(UI_Log _log)
    {
        logs.Add(_log);

    }

    //ロゴリストの取得
    public List<UI_Log> Logs
    {
        get { return logs; }
    }


    



}
