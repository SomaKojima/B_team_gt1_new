using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Cloud : MonoBehaviour
{

    //イメージ画像
    [SerializeField]
    RectTransform image;

    //インするときの場所
    [SerializeField]
    private RectTransform m_inPosition;

    //アウトするときの場所
    [SerializeField]
    private RectTransform m_outPosition;

    bool m_oneloopFlag = false;

    Vector3 inPosBuf;
    Vector3 outPosBuf;

    float time = 0;
    float duringTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        inPosBuf = m_inPosition.localPosition;
        outPosBuf = m_outPosition.localPosition;

    }

   
 

    //動き
    public void Move(bool isFadeIn)
    {

        if (m_oneloopFlag != isFadeIn)
        {
            time = 0.0f;
        }

        float t = CulcT();

        //trueだったら
        if (isFadeIn)
        {
            image.localPosition = Vector3.Lerp(image.localPosition, inPosBuf, t);
        }
        else
        {
            image.localPosition = Vector3.Lerp(image.localPosition, outPosBuf, t);
        }

        // 経過時間を計算
        time += Time.deltaTime;

        if (CulcT() >= 1.0f)
        {
            time = duringTime;
            isFadeIn = !isFadeIn;
        }

        m_oneloopFlag = isFadeIn;

       // Debug.Log(isFadeIn);

       
    }

    /// <summary>
    /// 補間の時間を計算
    /// </summary>
    /// <returns></returns>
    float CulcT()
    {
        if (duringTime == 0.0f)
        {
            return 1.0f;
        }
        else
        {
            return time / duringTime;
        }
    }
}
