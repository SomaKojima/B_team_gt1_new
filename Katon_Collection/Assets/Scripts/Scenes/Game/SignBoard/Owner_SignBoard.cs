using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Owner_SignBoard : MonoBehaviour
{
    [SerializeField]
    List<SignBoard> signBoards = new List<SignBoard>();
    
    // 視界内にいるかどうかを判定するデリゲート
    public delegate bool IsVisible(Vector3 _position);
    IsVisible isVisibleFunction = null;

    // 視界内にいる看板を入れる変数
    SignBoard visibleSignBoard = null;

    public void Initialize(IsVisible _function)
    {
        isVisibleFunction = _function;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        visibleSignBoard = null;
        foreach (SignBoard board in signBoards)
        {
            // カメラの視界内にいるかどうかを判定する
            if (isVisibleFunction != null)
            {
                board.IsVisible = isVisibleFunction(board.transform.position);
            }
            if (board.IsVisible)
            {
                visibleSignBoard = board;
                break;
            }
        }
    }

    public List<SignBoard> GetSignBoards()
    {
       
        return signBoards;
    }

    /// <summary>
    /// 建築ボードを出すリクエスト
    /// </summary>
    /// <returns></returns>
    public bool IsActiveBoard()
    {
        return visibleSignBoard != null;
    }

    /// <summary>
    /// 視界内の看板の場所を取得する
    /// </summary>
    /// <returns></returns>
    public Type GetVisiblePlaceType()
    {
        if (visibleSignBoard == null) return Type.none;
        return visibleSignBoard.GetPlaceType();
    }
}
