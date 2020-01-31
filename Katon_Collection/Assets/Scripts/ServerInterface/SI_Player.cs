using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SI_Player : Photon.MonoBehaviour
{
    public int[] placePoint;// = new int[(int)Type.Max];
    public int[] itemCount = new int[(int)ITEM_TYPE.NUM];
    public int id = -1;
    public new string name = "";
    public bool isExcange = false;
    
    // Start is called before the first frame update
    public void Initialize()
    {
        placePoint = new int[(int)Type.Max];

        for (int i = 0; i < placePoint.Length; i++)
        {
            placePoint[i] = 0;
        }
        itemCount = new int[(int)ITEM_TYPE.NUM];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int GetPlacePoint(int index)
    {
        return placePoint[index];
    }

    public void SetPlacePoint(int point,int index)
    {
        placePoint[index] = point;
    }

    public int GetItemCount(int index)
    {
        return itemCount[index];
    }

    public void SetItemCount(int point, int index)
    {
        itemCount[index] = point;
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
        set { isExcange = value; }
    }
}
