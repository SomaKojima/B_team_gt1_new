using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ビット演算でフラグを管理するクラス
/// </summary>
public class BitFlag
{
    int bitFlag = 0;
    

    /// <summary>
    /// フラグを立てる
    /// </summary>
    /// <param name="_flag"></param>
    public void OnFlag(int _flag)
    {
        int bufFlag = 1 << _flag;
        bitFlag =  bitFlag | bufFlag;
    }

    /// <summary>
    /// フラグを伏せる
    /// </summary>
    /// <param name="_flag"></param>
    public void OffFlag(int _flag)
    {
        int bufFlag = 1 << _flag;
        bitFlag = bitFlag & ~bufFlag;
    }

    /// <summary>
    /// フラグが立っているかを取得
    /// </summary>
    /// <param name="_flag"></param>
    /// <returns></returns>
    public bool IsFlag(int _flag)
    {
        int bufFlag = 1 << _flag;
        if ((bitFlag & bufFlag) != 0) return true;
        return false;
    }

    /// <summary>
    /// フラグすべてを伏せる
    /// </summary>
    public void Clear()
    {
        bitFlag = 0;
    }
}
