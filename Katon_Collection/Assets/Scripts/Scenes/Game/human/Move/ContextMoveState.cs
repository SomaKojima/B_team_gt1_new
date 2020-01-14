using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMoveState
{
    MOVE_STATE_TYPE stateType = MOVE_STATE_TYPE.NONE;
    MoveState state = null;
    
    GoToTarget goToTarget = new GoToTarget();
    Collect collect = new Collect();
    Pick pick = new Pick();

    Vector3 target = Vector3.zero;
    
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="human"></param>
    public void Initialize(Human human)
    {
        Change(human, MOVE_STATE_TYPE.COLLECT);
    }

    /// <summary>
    /// 実行
    /// </summary>
    /// <param name="human"></param>
    public void Excute(Human human)
    {
        if (state != null)
        {
            Change(human, state.Excute(human));
        }

        if (human.IsPick)
        {
            Change(human, MOVE_STATE_TYPE.PICK);
        }
    }

    /// <summary>
    /// ステートの変更
    /// </summary>
    /// <param name="human"></param>
    /// <param name="type"></param>
    public void Change(Human human, MOVE_STATE_TYPE type)
    {
        if (type == stateType) return;
        stateType = type;
        human.Velocity = new Vector3(Human.SPEED, 0.0f, Human.SPEED); ;
        switch (type)
        {
            case MOVE_STATE_TYPE.GO_TO_TARGET:
                Debug.Log("initialize");
                goToTarget.Initialize(target);
                Debug.Log(target);
                state = goToTarget;
                break;
            case MOVE_STATE_TYPE.COLLECT:
                collect.Initialize(human, target);
                state = collect;
                break;
            case MOVE_STATE_TYPE.PICK:
                pick.Initialize(human);
                state = pick;
                break;
        }
    }

    public void SetTarget(Vector3 _position)
    {
        target = _position;
    }
}
