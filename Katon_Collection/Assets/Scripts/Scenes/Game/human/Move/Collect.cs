using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MoveState
{
    Vector3 target;

    // Start is called before the first frame update
    public void Initialize(Vector3 _target)
    {
        target = _target;
    }

    public MOVE_STATE_TYPE Excute(Human human)
    {
        return MOVE_STATE_TYPE.NONE;
    }
}
