using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Title : MonoBehaviour
{

    [SerializeField]
    Fade_CloudEffect m_fade_CloudEffect = null;

    [SerializeField]
    TitleCameraMove m_cameraMove = null;

    Type m_placeType;

    //ランダムな値
    int m_value;

    //ランダムの値を入れる箱
    int m_nextValue;

    float time = 0.0f;

    //テスト用のフラグ
    bool testFlag = false;

    //場所を切り替える
    public void ChangePlace()
    {
        //テスト（ボタン押したらフェードが行われる）
        if (Input.GetMouseButtonDown(0))
        {
            testFlag = true;
        }



        //trueならフェード開始
        if (testFlag)
        {
            //ランダムな値を入れる
            m_nextValue = m_value;

            //ランダムな値を出して場所を切り替える
            if (m_nextValue == m_value)
            {
                m_value = Random.Range((int)Type.market, (int)Type.farm);

            }
            m_placeType = (Type)m_value;

            //フェードインする
            StartCoroutine(m_fade_CloudEffect.FadeIn());


            if (!m_fade_CloudEffect.GetIsProcess)
            {
                testFlag = false;





                m_cameraMove.ChangePosition(m_placeType);
            }
        }
        else
        {
            //フェードアウトの処理
            StartCoroutine(m_fade_CloudEffect.FadeOut());


        }

        time += Time.deltaTime;






    } 
}
