using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_MainGame
{
    Request request = new Request();

    Manager_Item managerItem = null;

    float frame = 0;
    float duringFrame = 10;

    List<int> buindingTime = new List<int>();
    int index = 0;

    List<int> humanTime = new List<int>();
    List<int> HumanCount = new List<int>();
    int humanIndex = 0;

    List<IItem> items = new List<IItem>();

    public void Initialize(Manager_Item _managerItem)
    {
        request.Initialize();
        managerItem = _managerItem;

        buindingTime.Add(0);
        buindingTime.Add(60);
        buindingTime.Add(150);
        buindingTime.Add(240);
        buindingTime.Add(330);
        buindingTime.Add(420);
        buindingTime.Add(510);

        //humanTime.Add(45);
        //HumanCount.Add(0);

        //Time.timeScale = 50.0f;
    }
    
    // Update is called once per frame
    public void Update()
    {
        frame += Time.deltaTime;

        if ((humanIndex + 1 <= humanTime.Count))
        {
            if (frame >= humanTime[humanIndex])
            {
                AddHuman();
                DumpItems();

                humanIndex++;
            }
        }

        if (index + 1 <= buindingTime.Count)
        {
            if (frame >= buindingTime[index])
            {
                index++;
                request.Flag.OnFlag(REQUEST_BIT_FLAG_TYPE.IMMEDIATELY, REQUEST.BUILDING);

                //ClearBuildignResource();
            }
        }

        if (request.ReplayFlag.IsFlag(REPLAY_REQUEST.BUILDING_SUCCESS))
        {
            DumpItems();
            //UnityEditor.EditorApplication.isPaused = true;
        }

        request.ReplayFlag.Clear();
    }

    void AddHuman()
    {
        for (int i = 0; i < HumanCount[humanIndex]; i++)
        {
            int random = Random.Range(0, ((int)ITEM_TYPE.HUMAN_NUM - 1));

            int count = managerItem.GetItem((ITEM_TYPE)random).GetCount();
            managerItem.GetItem((ITEM_TYPE)random).SetCount(count + 1);
        }
    }

    void ClearBuildignResource()
    {
        for (int i = (int)ITEM_TYPE.HUMAN_NUM; i < (int)ITEM_TYPE.NUM; i++)
        {
            managerItem.GetItem((ITEM_TYPE)i).SetCount(0);
        }
    }

    public Request GetRequest()
    {
        return request;
    }

    void DumpItems()
    {
        string dump = "\n\n\n" + frame + "\n";

        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            dump += (ITEM_TYPE)i + " : " + managerItem.GetItem((ITEM_TYPE)i).GetCount() + "\n";
        }

        Debug.Log(dump);
    }
}
