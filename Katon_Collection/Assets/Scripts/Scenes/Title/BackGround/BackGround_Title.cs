using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Title : MonoBehaviour
{

    [SerializeField]
    Fade_CloudEffect m_fade_CloudEffect;

    [SerializeField]
    CameraManager m_cameraManager;

    Type m_placeType;

    //ランダムな値
    int m_value;

    //ランダムの値を入れる箱
    int m_nextValue;

    float time = 0.0f;

    //テスト用のフラグ
    bool testFlag = false;

    void Update()
    {
        //テスト（ボタン押したらフェードが行われる）
        if (Input.GetMouseButtonDown(0))
        {
            testFlag = true;
        }

        //trueならフェード開始
        if (testFlag)
        {
           
            //フェードインする
            StartCoroutine(m_fade_CloudEffect.FadeIn());


            if (!m_fade_CloudEffect.GetIsProcess)
            {
                testFlag = false;

                //ランダムな値を入れる
                m_nextValue = m_value;

                ChangePlace();
            }
        }
        else
        {
            //フェードアウトの処理
            StartCoroutine(m_fade_CloudEffect.FadeOut());
        }

        time += Time.deltaTime;

        //if (time > testtime)
        //{
           

        //    if (testFlag)
        //    {
        //        //チェンジするタイミングでフェードインをかける
        //        StartCoroutine(m_fade_CloudEffect.FadeIn());
        //    }
           

        //    //フェードインが終わったら
        //    if (!m_fade_CloudEffect.GetIsProcess)
        //    {
        //        testFlag = false;
        //       // Debug.Log(m_fade_CloudEffect.GetIsProcess);
              
        //        ChangePlace();
        //        m_cameraManager.ChangeCamera(m_placeType);
        //    }
        //}



      
    }

    //場所を切り替える
    public void ChangePlace()
    {

        //ランダムな値を出して場所を切り替える
        if (m_nextValue == m_value)
        {
            m_value = Random.Range((int)Type.none, (int)Type.Max);

        }
        m_placeType = (Type)m_value;


        Debug.Log(m_placeType);


    } 
}
