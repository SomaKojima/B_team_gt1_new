using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SI_Player
{
    private int[] placePoint;
    private int[] itemCount;
    private int id;
    private new string name;
    private bool isExcange;
    private bool ChangeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPlacePoint(int index)
    {
        return placePoint[index];
        ChangeFlag = true;
    }

    public void SetPlacePoint(int point,int index)
    {
        placePoint[index] = point;
        ChangeFlag = true;
    }

    public int GetItemCount(int index)
    {
        return itemCount[index];
    }

    public void SetItemCount(int point, int index)
    {
        itemCount[index] = point;
        ChangeFlag = true;
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public bool IsExcange
    {
        get { return isExcange; }
        set { isExcange = value;
            ChangeFlag = true; }
    }

    public bool GetChangeFlag()
    {
        if(ChangeFlag)
        {
            ChangeFlag = false;
            return true;
        }
        else
        {
            return ChangeFlag;
        }
    }
}
