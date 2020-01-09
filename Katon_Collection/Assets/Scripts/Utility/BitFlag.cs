using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ビット演算でフラグを管理するクラス
/// </summary>
public class BitFlag
{
    ulong bitFlag = 0;

    /// <summary>
    /// フラグを立てる
    /// </summary>
    /// <param name="_flag"></param>
    public void OnFlag(ulong _flag)
    {
        bitFlag =  bitFlag | _flag;
    }

    /// <summary>
    /// フラグを伏せる
    /// </summary>
    /// <param name="_flag"></param>
    public void OffFlag(ulong _flag)
    {
        bitFlag = bitFlag & ~_flag;
    }

    /// <summary>
    /// フラグが立っているかを取得
    /// </summary>
    /// <param name="_flag"></param>
    /// <returns></returns>
    public bool IsFlag(ulong _flag)
    {
        ulong buf = bitFlag & _flag;
        if (buf != 0) return true;
        return false;
    }
}
