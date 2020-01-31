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
    public bool ChangeFlag = false;

    // Start is called before the first frame update
    void Start()
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

    // 送受信
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("b");
        if (stream.isWriting)
        {
            //データの送信
            Send(stream);
        }
        else
        {
            //データの受信
            Receive(stream);
        }
    }

    void Send(PhotonStream stream)
    {

        for (int i = 0; i < (int)Type.Max; i++)
        {
            stream.SendNext(placePoint[i]);
        }
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            stream.SendNext(itemCount[i]);
        }

        stream.SendNext(name);
        stream.SendNext(isExcange);
        stream.SendNext(ChangeFlag);
    }

    void Receive(PhotonStream stream)
    {
        for (int i = 0; i < (int)Type.Max; i++)
        {
            placePoint[i] = (int)stream.ReceiveNext();
        }
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            itemCount[i] = (int)stream.ReceiveNext();
            stream.SendNext(itemCount[i]);
        }

        name = (string)stream.ReceiveNext();
        isExcange = (bool)stream.ReceiveNext();
        ChangeFlag = (bool)stream.ReceiveNext();
    }
}
