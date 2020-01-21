using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTarget : MoveState
{
    public void Initialize()
    {
    }


    public MOVE_STATE_TYPE Excute(Human human)
    {
        MoveTargetPosition(human, human.GetTargetPosition());

        return MOVE_STATE_TYPE.GO_TO_TARGET;
    }

    // 目的の場所に移動する
    void MoveTargetPosition(Human human, Vector3 target)
    {
        Transform transform = human.transform;
        Vector3 velocity = human.Velocity;
        float length = (transform.position - target).magnitude;

        float t = 1.0f / (length / Human.SPEED);
        velocity = Vector3.Lerp(transform.position, target, t) - transform.position;
        velocity.y = 0;
        human.Velocity = velocity;
    }
}
