using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Owner_SignBoard : MonoBehaviour
{
    [SerializeField, EnumListLabel(typeof(Type))]
    SignBoard[] signBoards;
    
    // 視界内にいるかどうかを判定するデリゲート
    public delegate bool IsVisible(Vector3 _position);
    IsVisible isVisibleFunction = null;

    // 場所の階数を取得するデリゲート
    public delegate int GetPlaceCount(Type _placeType);
    GetPlaceCount getPlaceCountFanction;


    public delegate int GetMoveIn(Type _placeType);
    GetMoveIn getMoveIn;

    // 視界内にいる看板を入れる変数
    SignBoard visibleSignBoard = null;

    public void Initialize(IsVisible _isVisible, GetPlaceCount _getPlaceCountFanction, GetMoveIn _getMoveIn)
    {
        isVisibleFunction = _isVisible;
        getPlaceCountFanction = _getPlaceCountFanction;
        getMoveIn = _getMoveIn;
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
            if (board == null) continue;
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

        foreach (SignBoard board in signBoards)
        {
            if (board == null) continue;
            board.Num = getPlaceCountFanction(board.GetPlaceType());
            board.Max = getMoveIn(board.GetPlaceType());
        }
    }

    public SignBoard[] GetSignBoards()
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

    public void SetPlaceBuildingTotal(Type _placeType, int _buildingTotal)
    {
        if (signBoards[(int)_placeType] == null) return;
        signBoards[(int)_placeType].Max = _buildingTotal;
    }
}
