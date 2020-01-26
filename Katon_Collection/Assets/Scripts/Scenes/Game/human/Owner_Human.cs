using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_Human : MonoBehaviour
{
    [SerializeField]
    Manager_Human manager_human;

    [SerializeField]
    Factory_Human factory_human;

    [SerializeField, EnumListLabel(typeof(Type))]
    BoxCollider[] createPositon = new BoxCollider[(int)Type.Max];

    bool isShop;

    // 誰か掴まれている
    bool isPick = false;

    // リクエスト用のフラグ
    Request request = new Request();

    List<Request> bufRequests = new List<Request>();

    bool isCollect = false;

    // 強化された人間の数
    int[] powerUpCount = new int[(int)ITEM_TYPE.HUMAN_NUM];
    int[] bufPowerUpCount = new int[(int)ITEM_TYPE.HUMAN_NUM];

    public void Intialize()
    {
        manager_human.Initialize();
        request.Initialize();
        for(int i = 0; i < (int)ITEM_TYPE.HUMAN_NUM; i++)
        {
            powerUpCount[i] = 0;
            bufPowerUpCount[i] = 0;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 掴まれている人間を確認
        if (isPick)
        {
            request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_SCROLL);
        }
        isPick = false;

        Collider hitColider = GetHitCollider();

        bufRequests.Clear();

        for (int i = 0; i < (int)ITEM_TYPE.HUMAN_NUM; i++)
        {
            bufPowerUpCount[i] = 0;
        }


        // すべての人間の処理
        foreach (Human human in manager_human.GetList())
        {
            // 掴まれている
            //if (RayCheck(human.GetComponent<Collider>(), hitColider))
            if (human.IsPick)
            {
                //human.IsPick = true;
                isPick = true;
                request.Flag.OffFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_SCROLL);
                request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.CAMERA_OUT_RANGE);
            }

            // 強化された数を数える
            ITEM_TYPE type = human.GetItemType();
            if (human.IsPowerUp())
            {
                bufPowerUpCount[(int)type]++;
            }

            // リクエストを追加
            bufRequests.Add(human.GetRequest());
        }
    }


    /// <summary>
    /// 強化の数を合わせる
    /// </summary>
    void MatchPowerUpHuman()
    {

    }

    public void MatchItemsHumans(IItem items, Type _placeType)
    {
        MatchItemsHumans(items, createPositon[(int)_placeType], _placeType);
    }

    void MatchItemsHumans(IItem item, BoxCollider collider, Type placeType)
    {
        MatchItemsHumans(item,
            collider.gameObject.transform.position,
            collider.size.x,
            collider.size.y,
            collider.size.z,
            placeType);
    }

    // アイテムマネージャーと実体の人間の数を同じにする(人間を生成・削除する)
    void MatchItemsHumans(IItem item, Vector3 position, float width, float height, float depth, Type placeType)
    {
        ITEM_TYPE type = item.GetItemType();
        int differenceCount = item.GetCount() - manager_human.GetListOf(type).Count;
        // 追加
        if (differenceCount > 0)
        {
            for (int j = 0; j < differenceCount; j++)
            {
                manager_human.Add(factory_human.CreateRandomPosition(position, width, height, depth, type, placeType));
            }
        }
        // 削除
        else if (differenceCount < 0)
        {
            manager_human.Delete(type, -differenceCount);
        }

        // 強化の数を合わせる
        differenceCount = item.GetPowerUpCount() - bufPowerUpCount[(int)type];
        int index = 0;
        // 強化する
        if (differenceCount > 0)
        {
            foreach (Human human in manager_human.GetListOf(type))
            {
                if(!human.IsPowerUp())
                {
                    human.SetPowerUp(true);
                    index++;
                }
                if (index >= differenceCount)
                {
                    break;
                }
            }
        }
        // 強化を解除する
        else if (differenceCount < 0)
        {
            foreach (Human human in manager_human.GetListOf(type))
            {
                if (human.IsPowerUp())
                {
                    human.SetPowerUp(false);
                    index--;
                }
                if (index <= differenceCount)
                {
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 掴まれているかどうか
    /// </summary>
    /// <param name="human"></param>
    /// <returns></returns>
    private bool RayCheck(Collider humanCollider, Collider hitCollider)
    {
        if (!Input.GetMouseButton(0)) return false;
        if (humanCollider == null) return false;
        if (hitCollider == null) return false;
        Debug.Log(hitCollider.gameObject.name);

        if (hitCollider == humanCollider)
        {
            return true;
        }

        return false;
    }

    public Collider GetHitCollider()
    {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            return hit.collider;
        }
        return null;
    }


    public Request GetRequest()
    {
        return request;
    }

    public List<Request> GetRequests()
    {
        return bufRequests;
    }

    public List<Human> GetHumans()
    {
        return manager_human.GetList();
    }

    public int GetPlaceCount(Type _placeType)
    {
        return manager_human.GetListOf(_placeType).Count;
    }

    public List<Human> GetPlaceHuman(Type _placeType)
    {
        return manager_human.GetListOf(_placeType);
    }
}
