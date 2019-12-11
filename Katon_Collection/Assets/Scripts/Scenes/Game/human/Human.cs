using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public const float SPEED = 0.1f;
    public const float COLLECT_DURING_TIME = 0.5f;

    ContextMoveState move = new ContextMoveState();
    bool isCollect = false;

    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        move.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity;
        move.Excute(this);
    }

    public void Initialize()
    {
        
    }

    public bool IsCollect()
    {
        return isCollect;
    }

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
}
