using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public const float SPEED = 0.25f;
    public const float COLLECT_DURING_TIME = 0.5f;

    [SerializeField]
    ItemContextTable itemContextTable;

    [SerializeField]
    Renderer renderer;

    ContextMoveState move = new ContextMoveState();
    bool isCollect = false;

    Vector3 velocity = Vector3.zero;

    public void Initialize(ITEM_TYPE _type)
    {
        renderer.material = itemContextTable.GetItemContex(_type).GetMaterial();
    }

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(Human.SPEED, 0.0f, Human.SPEED);
        move.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity;
        move.Excute(this);
        
    }

    public bool IsCollect
    {
        get { return isCollect; }
        set { isCollect = value; }
    }

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
}
