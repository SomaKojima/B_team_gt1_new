using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Human : MonoBehaviour
{
    //List<Human> humans = new List<Human>();
    List<Human>[] humans = new List<Human>[(int)ITEM_TYPE.WOOD];
    List<Human>[] placeHumans = new List<Human>[(int)Type.Max];
    List<Human> all = new List<Human>();

    public void Initialize()
    {
        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            humans[i] = new List<Human>();
        }

        for (int i = 0; i < (int)Type.Max; i++)
        {
            placeHumans[i] = new List<Human>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 初期化
        for (int i = 0; i < (int)Type.Max; i++)
        {
            placeHumans[i].Clear();
        }
        all.Clear();


        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            foreach (Human human in humans[i])
            {
                if (human.GetPlaceType() == Type.none) continue;

                placeHumans[(int)human.GetPlaceType()].Add(human);
            }
            all.AddRange(humans[i]);
        }


    }

    public void Add(Human human)
    {
        humans[(int)human.GetItemType()].Add(human);
    }

    public void Delete(ITEM_TYPE type, int count)
    {
        List<Human> list = GetListOf(type);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Destroy(list[i].gameObject);
            list.RemoveAt(i);
            count--;
            if (count < 0) break;
        }
    }

    public List<Human> GetList()
    {
        return all; 
    }

    public List<Human> GetListOf(ITEM_TYPE type)
    {
        return humans[(int)type];
    }

    public List<Human> GetListOf(Type _placeType)
    {
        return placeHumans[(int)_placeType];
    }
}
