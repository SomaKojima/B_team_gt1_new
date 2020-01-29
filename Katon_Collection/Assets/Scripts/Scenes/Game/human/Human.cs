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

    [SerializeField]
    IconMove icon;

    [SerializeField]
    Renderer crown;

    [SerializeField]
    Animator animator;

    ContextMoveState move = new ContextMoveState();
    bool isCollect = false;
    bool isPick = false;

    Request request = new Request();

    Vector3 velocity = Vector3.zero;

    ITEM_TYPE type;
    
    Type placeType = Type.cave;

    Vector3 targetPosition = Vector3.zero;

    bool isPowerUp = false;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="_type"></param>
    public void Initialize(ITEM_TYPE _type, Type _placeType)
    {
        type = _type;
        placeType = _placeType;
        renderer.material = itemContextTable.GetItemContex(_type).GetMaterial();
        request.Initialize();
        // 場所を変更
        this.GetRequest().Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.POSITION_TO_PLACE);
        this.GetRequest().ChangePosition = this.gameObject.transform.position;

        animator.enabled = false;
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

        if(isCollect)
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.COLLECT);
            request.CollectItemType = ChangeItemType.HumanToBuildingResource(type);
            request.CollectPlaceType = placeType;
            request.IsDoubleCollect = isPowerUp;
        }

        // 王冠の表示・非表示
        crown.gameObject.SetActive(isPowerUp);

        UpdateReplayRequest();
    }

    void UpdateReplayRequest()
    {
        // 場所を変更
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.POSITION_TO_PLACE_SUCCESS))
        {
            if (request.ChangePlaceType != Type.fountain &&
                request.ChangePlaceType != Type.market)
            {
                placeType = request.ChangePlaceType;
                targetPosition = request.AreaCenterPosition;
            }
            //Debug.Log(request.AreaCenterPosition);
            //move.Change(this, MOVE_STATE_TYPE.GO_TO_TARGET);
        }
        // 収集成功
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.COLLECT_SUCCESS))
        {
            icon.Initialize(ChangeItemType.HumanToBuildingResource(type));
        }

        // 収集失敗
        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.COLLECT_FALIED))
        {

        }
        request.ReplayFlag.Clear();
    }

    public void Flip(bool isFlip)
    {
        if ((isFlip && renderer.gameObject.transform.localScale.x > 0) ||
            (!isFlip && renderer.gameObject.transform.localScale.x < 0))
        {
            Debug.Log("flip");
            renderer.gameObject.transform.localScale = new Vector3(renderer.gameObject.transform.localScale.x * -1, renderer.gameObject.transform.localScale.y, renderer.gameObject.transform.localScale.z);
        }
    }

    /// <summary>
    /// 収集
    /// </summary>
    public bool IsCollect
    {
        get { return isCollect; }
        set { isCollect = value; }
    }

    /// <summary>
    /// 掴まれている
    /// </summary>
    public bool IsPick
    {
        get { return isPick; }
        set { isPick = value;}
    }

    /// <summary>
    /// 速度
    /// </summary>
    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    /// <summary>
    /// 収集するアイテムのタイプ
    /// </summary>
    /// <returns></returns>
    public ITEM_TYPE GetItemType()
    {
        return type;
    }

    /// <summary>
    /// 今いる場所を取得
    /// </summary>
    /// <returns></returns>
    public Type GetPlaceType()
    {
        return placeType;
    }

    public Request GetRequest()
    {
        return request;
    }

    public Vector3 GetTargetPosition()
    {
        return targetPosition;
    }

    public void ClickEnter()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    IsPick = true;
        //}
    }

    void OnMouseDown()
    {
        isPick = true;
    }

    public void SetPowerUp(bool flag)
    {
        isPowerUp = flag;
    }

    public bool IsPowerUp()
    {
        return isPowerUp;
    }
}
