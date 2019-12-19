using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToRoad : MoveState
{
    float z = 0.0f;

    public void Initialize(float _z)
    {
        z = _z;
    }

    public MOVE_STATE_TYPE Excute(Human human)
    {
        return MOVE_STATE_TYPE.NONE;
    }
}
