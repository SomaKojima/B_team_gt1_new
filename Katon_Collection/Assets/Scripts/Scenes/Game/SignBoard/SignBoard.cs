using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBoard : MonoBehaviour
{
    [SerializeField]
    Type placeType;

    [SerializeField]
    CheckClick checkClick;

    bool isClick = false;

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
    }

    public bool IsClick()
    {
        return isClick;
    }

    public Type GetPlaceType()
    {
        return placeType;
    }
}
