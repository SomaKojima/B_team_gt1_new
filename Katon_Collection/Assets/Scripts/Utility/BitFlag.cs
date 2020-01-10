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
    /// ビットでフラグを立てる
    /// </summary>
    /// <param name="_bit"></param>
    public void OnFlag(int _bit)
    {
        bitFlag = bitFlag | _bit;
    }

    /// <summary>
    /// ビットでフラグを伏せる
    /// </summary>
    /// <param name="_bit"></param>
    public void OffFlag(int _bit)
    {
        bitFlag = bitFlag & ~_bit;
    }


    /// <summary>
    /// フラグが立っているかを取得
    /// </summary>
    /// <param name="_flag"></param>
    /// <returns></returns>
    public bool IsFlag(int _bit)
    {
        if ((bitFlag & _bit) == _bit) return true;
        return false;
    }

    /// <summary>
    /// フラグすべてを伏せる
    /// </summary>
    public void Clear()
    {
        bitFlag = 0;
    }

    public int GetBitFlag()
    {
        return bitFlag;
    }
}
