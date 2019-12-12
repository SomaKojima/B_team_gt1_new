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
    
    public void Initialize(Human human)
    {
        Change(human, MOVE_STATE_TYPE.COLLECT);
    }

    public void Excute(Human human)
    {
        if (state != null)
        {
            Change(human, state.Excute(human));
        }

        if (RayCheck(human) && stateType != MOVE_STATE_TYPE.PICK)
        {
            Debug.Log("change");
            Change(human, MOVE_STATE_TYPE.PICK);
        }
    }

    public void Change(Human human, MOVE_STATE_TYPE type)
    {
        stateType = type;
        switch (type)
        {
            case MOVE_STATE_TYPE.GO_TO_TARGET:
                goToTarget.Initialize(Vector3.zero);
                state = goToTarget;
                break;
            case MOVE_STATE_TYPE.COLLECT:
                collect.Initialize(human, Vector3.zero);
                state = collect;
                break;
            case MOVE_STATE_TYPE.PICK:
                pick.Initialize(human);
                state = pick;
                break;
        }
    }


    private bool RayCheck(Human human)
    {
        if (!Input.GetMouseButton(0)) return false;

        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == human.gameObject.GetComponent<Collider>())
        {
            Debug.Log("true");
            return true;
        }

        return false;
    }
}
