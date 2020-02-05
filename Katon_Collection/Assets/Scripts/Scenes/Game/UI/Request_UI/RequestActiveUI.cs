using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIVE_BIT_FLAG_TYPE
{
    IMMEDIATELY,    // 即座に実行
    FADE,           // フェードインが終わったタイミングで実行

    MAX
}

/// <summary>
/// 有効にするUIの種類
/// </summary>
public enum ACTIVE_UI
{
    PLACE_BAR = 1 << 1,
    FOUNTAIN = 1 << 2,
    MARKET = 1 << 3,
    QR_READER = 1 << 4,
    BUILDIGN_BOARD = 1 << 5,
    FIRST_TEXT = 1 << 6,
    SELECT_QR = 1 << 7,
    INFO_WINDOW = 1 << 8, 
    HUMAN_WINDOW = 1 << 9,

    MAX
}

/// <summary>
/// UIのアクティブのリクエストを管理する
/// </summary>
public class RequestActiveUI
{

    // UIを有効化する用のビットフラグ
    BitFlag[] bitActiveFlag = new BitFlag[(int)ACTIVE_BIT_FLAG_TYPE.MAX];

    // UIを無効化する用のビットフラグ
    BitFlag[] bitUnActiveFlag = new BitFlag[(int)ACTIVE_BIT_FLAG_TYPE.MAX];

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initailize()
    {
        for (int i = 0; i < (int)ACTIVE_BIT_FLAG_TYPE.MAX; i++)
        {
            bitActiveFlag[i] = new BitFlag();
            bitUnActiveFlag[i] = new BitFlag();
        }
    }

    /// <summary>
    /// 有効化するUIのビットフラグを立てる
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_ui"></param>
    public void Active_OnFlag(ACTIVE_BIT_FLAG_TYPE _type, ACTIVE_UI _ui)
    {
        bitActiveFlag[(int)_type].OnFlag((int)_ui);
    }

    /// <summary>
    /// 有効化するUIのビットフラグを伏せる
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_ui"></param>
    public void Active_OffFlag(ACTIVE_BIT_FLAG_TYPE _type, ACTIVE_UI _ui)
    {
        bitActiveFlag[(int)_type].OffFlag((int)_ui);
    }

    /// <summary>
    /// 無効化するUIのビットフラグを立てる
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_ui"></param>
    public void UnActive_OnFlag(ACTIVE_BIT_FLAG_TYPE _type, ACTIVE_UI _ui)
    {
        bitUnActiveFlag[(int)_type].OnFlag((int)_ui);
    }

    /// <summary>
    /// 無効化するUIのビットフラグを伏せる
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_ui"></param>
    public void UnActive_OffFlag(ACTIVE_BIT_FLAG_TYPE _type, ACTIVE_UI _ui)
    {
        bitUnActiveFlag[(int)_type].OffFlag((int)_ui);
    }

    /// <summary>
    /// 有効化するかどうかを取得する
    /// </summary>
    /// <param name="_ui"></param>
    /// <returns></returns>
    public bool IsActive(ACTIVE_UI _ui)
    {
        return bitActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].IsFlag((int)_ui);
    }


    /// <summary>
    /// 無効化するかどうかを取得する
    /// </summary>
    /// <param name="_ui"></param>
    /// <returns></returns>
    public bool IsUnActive(ACTIVE_UI _ui)
    {
        return bitUnActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].IsFlag((int)_ui);
    }

    /// <summary>
    /// ビットフラグをリクエストに反映させる
    /// </summary>
    /// <param name="_type"></param>
    public void Reflection_ActiveFlag(ACTIVE_BIT_FLAG_TYPE _type)
    {
        int bufBitFlag = bitActiveFlag[(int)_type].GetBitFlag();
        bitActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].OnFlag((int)bufBitFlag);
    }


    /// <summary>
    /// ビットフラグをリクエストに反映させる
    /// </summary>
    /// <param name="_type"></param>
    public void Reflection_UnActiveFlag(ACTIVE_BIT_FLAG_TYPE _type)
    {
        int bufBitFlag = bitUnActiveFlag[(int)_type].GetBitFlag();
        bitUnActiveFlag[(int)ACTIVE_BIT_FLAG_TYPE.IMMEDIATELY].OnFlag(bufBitFlag);
    }

    /// <summary>
    /// フラグをすべて伏せる
    /// </summary>
    /// <param name="_type"></param>
    public void ClearActiveFlag(ACTIVE_BIT_FLAG_TYPE _type)
    {
        bitActiveFlag[(int)_type].Clear();
    }

    /// <summary>
    /// フラグをすべて伏せる
    /// </summary>
    /// <param name="_type"></param>
    public void ClearUnActiveFlag(ACTIVE_BIT_FLAG_TYPE _type)
    {
        bitUnActiveFlag[(int)_type].Clear();
    }

}
