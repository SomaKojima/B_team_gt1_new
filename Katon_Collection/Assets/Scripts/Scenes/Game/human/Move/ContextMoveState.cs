using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMoveState
{
    MoveState state = null;
    
    GoToTarget goToTarget = new GoToTarget();
    
    public void Initialize()
    {
        goToTarget.Initialize(Vector3.zero);

        state = goToTarget;
    }

    public void Excute(Human human)
    {
        if (state != null)
        {
            Change(state.Excute(human));
        }
    }

    public void Change(MOVE_STATE_TYPE type)
    {
        switch (type)
        {
            case MOVE_STATE_TYPE.GO_TO_TARGET:
                state = goToTarget;
                break;
        }
    }
}
