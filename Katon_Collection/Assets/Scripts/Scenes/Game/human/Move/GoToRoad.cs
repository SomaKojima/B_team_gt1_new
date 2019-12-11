using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToRoad : MoveState
{
    float x =0.0f;
    bool isLeft = false;

    public void Initialize(float _x)
    {
        x = _x;
    }
    public MOVE_STATE_TYPE Excute(Human human)
    {
        return MOVE_STATE_TYPE.NONE;
    }
}
