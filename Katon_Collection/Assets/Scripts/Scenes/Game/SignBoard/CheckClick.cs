using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClick : MonoBehaviour
{
    bool isClick = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickProcess()
    {
        isClick = false;
    }

    public void OnClick()
    {
        isClick = true;
    }

    public bool IsClick
    {
        get { return isClick; }
    }
}
