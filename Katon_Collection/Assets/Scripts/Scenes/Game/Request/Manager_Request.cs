using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Request
{

    // リクエストのリスト
    List<Request> requestList = new List<Request>();
    
    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
    }
    
    /// <summary>
    /// フラグを追加する
    /// </summary>
    /// <param name="_flag"></param>
    public void Add(Request _request)
    {
        requestList.Add(_request);
    }

    /// <summary>
    /// フェードのフラグを反映させる
    /// </summary>
    public void ReflectionFade()
    {
        foreach (Request request in requestList)
        {
            request.Flag.Reflection(REQUEST_BIT_FLAG_TYPE.FADE);
            request.Flag.Clear(REQUEST_BIT_FLAG_TYPE.FADE);
        }
    }
    
    /// <summary>
    /// リクエストリストのプロパティ
    /// </summary>
    public List<Request> RequestList
    {
        get { return requestList; }
    }
}
