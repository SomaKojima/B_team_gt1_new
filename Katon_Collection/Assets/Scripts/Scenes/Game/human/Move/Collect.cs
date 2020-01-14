using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MoveState
{
    Vector3 target;

    float width = 10.0f;
    float depth = 10.0f;

    float frame = 0.0f;
    float duringFrame = 1.0f;

    // Start is called before the first frame update
    public void Initialize(Human human, Vector3 _target)
    {
        target = _target;
    }

    public MOVE_STATE_TYPE Excute(Human human)
    {

        float minX = target.x - (width * 0.5f);
        float maxX = target.x + (width * 0.5f);
        float minZ = target.z - (depth * 0.5f);
        float maxZ = target.z + (depth * 0.5f);
        TurnInOutArea(human, minX, maxX, minZ, maxZ);

        human.IsCollect = false;
        frame += Time.deltaTime;
        if (frame > duringFrame)
        {
            frame = 0.0f;
            human.IsCollect = true;
        }
        
        return MOVE_STATE_TYPE.COLLECT;
    }

    void TurnInOutArea(Human human, float minX, float maxX, float minZ, float maxZ)
    {
        Transform transform = human.transform;
        if (IsTurn(minX, maxX, transform.position.x, human.Velocity.x))
        {
            human.Velocity = new Vector3(human.Velocity.x * -1, human.Velocity.y, human.Velocity.z);

            // スプライトを反転
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (IsTurn(minZ, maxZ, transform.position.z, human.Velocity.z))
        {
            human.Velocity = new Vector3(human.Velocity.x, human.Velocity.y, human.Velocity.z * -1);
        }
    }

    /// <summary>
    /// 画面外に出ようとしたら反転するように設定
    /// </summary>
    /// <param name="maxPosX">X値最大値</param>
    /// <param name="minPosX">X値最小値</param>
    /// <param name="maxPosZ">Z値最大値</param>
    /// <param name="minPosZ">Z値最小値</param>
    public bool IsTurn(float minPos, float maxPos, float position, float velocity)
    {
        // 範囲外に出ようとしたら
        if (position > maxPos && velocity >= 0)
        {
            // 反転するように設定
            return true;
        }
        else if (position < minPos && velocity <= 0)
        {
            return true;
        }
        return false;
    }
}
