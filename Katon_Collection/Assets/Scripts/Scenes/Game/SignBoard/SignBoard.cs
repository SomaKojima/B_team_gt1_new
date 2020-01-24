using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignBoard : MonoBehaviour
{
    [SerializeField]
    Type placeType;

    [SerializeField]
    CheckClick checkClick;

    [SerializeField]
    TextMeshPro numText;

    [SerializeField]
    TextMeshPro maxText;

    bool isClick = false;

    //視界内にいるかどうか
    bool isVisible = false;

    int num = 0;
    int max = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isClick = false;
        if (checkClick.IsClick)
        {
            isClick = true;
            checkClick.OnClickProcess();
        }
        numText.text = num.ToString();
        maxText.text = max.ToString();
    }

    public bool IsClick()
    {
        return isClick;
    }

    public Type GetPlaceType()
    {
        return placeType;
    }
    
    public bool IsVisible
    {
        get { return isVisible; }
        set { isVisible = value; }
    }

    public int Num
    {
        get { return num; }
        set { num = value; }
    }

    public int Max
    {
        get { return max; }
        set { max = value; }
    }
}
