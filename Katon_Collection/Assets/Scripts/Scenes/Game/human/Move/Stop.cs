using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MoveState
{
    public void Initialize()
    {

    }


    public MOVE_STATE_TYPE Excute(Human human)
    {
        return MOVE_STATE_TYPE.STOP;
    }
}
