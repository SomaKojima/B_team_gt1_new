using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTarget : MoveState
{
    Vector3 target = Vector3.zero;

    public void Initialize(Vector3 _target)
    {

    }


    public MOVE_STATE_TYPE Excute(Human human)
    {
        MoveTargetPosition(human, target);

        return MOVE_STATE_TYPE.NONE;
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
