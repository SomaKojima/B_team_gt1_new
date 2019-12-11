using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoveState
{
    MOVE_STATE_TYPE Excute(Human human);
}
