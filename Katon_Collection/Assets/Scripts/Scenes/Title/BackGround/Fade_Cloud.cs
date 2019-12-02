using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Cloud : MonoBehaviour
{

    //インするときの場所
    [SerializeField]
    private RectTransform m_inPosition;

    //アウトするときの場所
    [SerializeField]
    private RectTransform m_outPosition;

    bool m_inFlag = false;

   


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //動き
    bool Move(bool isFadeIn)
    {
        if (isFadeIn)
        {
            //動く


        }
        else
        {
            //動かない
        }

        return isFadeIn;
    }
}
