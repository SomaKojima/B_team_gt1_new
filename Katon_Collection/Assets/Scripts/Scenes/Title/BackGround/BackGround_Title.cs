using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    float m_changeTime = 10.0f;

    //場所を切り替える
    public void ChangePlace()
    {

        time += Time.deltaTime;

        //テスト（ボタン押したらフェードが行われる）
        if (time> m_changeTime)
        {
            testFlag = true;
            time = 0;
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
            m_fade_CloudEffect.StartFadeIn();


            if (m_fade_CloudEffect.GetIsProcess)
            {
                testFlag = false;





                m_cameraMove.ChangePosition(m_placeType);

               
            }

        }
        else
        {
            //フェードアウトの処理
            m_fade_CloudEffect.StartFadeOut();


        }
   
    }

    bool m_changeRoby = false;

    public void ChangeRobyScene()
    {
        if(m_changeRoby)
        {
            //フェードインする
            m_fade_CloudEffect.StartFadeIn();

            if (m_fade_CloudEffect.GetIsProcess)
            {
                
                SceneManager.LoadScene(1);

            }
        }
    }

    public bool ChangeFlag
    {
        get { return m_changeRoby; }
        set { m_changeRoby = value; }
    }
  
}
