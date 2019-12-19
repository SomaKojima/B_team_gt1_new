using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MoveState
{
    float length = 0;

    public void Initialize(Human human)
    {
        human.Velocity = Vector3.zero;
        length = (human.transform.position - Camera.main.transform.position).magnitude;
    }

    public MOVE_STATE_TYPE Excute(Human human)
    {
        if (Input.GetMouseButtonUp(0)) return MOVE_STATE_TYPE.GO_TO_TARGET;

        MovePoisition(human);
        return MOVE_STATE_TYPE.NONE;
    }
    
    private void MovePoisition(Human human)
    {

        Vector3 mousePos = Input.mousePosition;
        
        mousePos.z = length;

        Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);


        human.transform.position = new Vector3(position.x, position.y, human.transform.position.z);
        if (human.transform.position.y < 0)
        {
            human.transform.position = new Vector3(position.x, human.transform.position.y, human.transform.position.z);
        }
    }

}
