using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MoveState
{
    float length = 0;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="human"></param>
    public void Initialize(Human human)
    {
        human.Velocity = Vector3.zero;
        length = (human.transform.position - Camera.main.transform.position).magnitude;
    }

    /// <summary>
    /// 実行
    /// </summary>
    /// <param name="human"></param>
    /// <returns></returns>
    public MOVE_STATE_TYPE Excute(Human human)
    {
        if (Input.GetMouseButtonUp(0))
        {
            human.IsPick = false;
            human.GetRequest().Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_SCROLL);
            return MOVE_STATE_TYPE.COLLECT;
        }

        human.GetRequest().Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_OUT_RANGE);

        human.IsPick = true;
        MovePoisition(human);
        return MOVE_STATE_TYPE.PICK;
    }
    
    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="human"></param>
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
