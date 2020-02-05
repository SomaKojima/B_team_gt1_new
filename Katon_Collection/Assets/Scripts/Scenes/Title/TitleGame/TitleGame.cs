using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGame : MonoBehaviour
{

    [SerializeField]
    private BackGround_Title m_backGround_Title = null;

    [SerializeField]
    private UI_Button_Title m_uI_Title_Button = null;

    float time = 0.0f;

    // サウンド
    [SerializeField]
    Sound_Title sound;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        // BGMを鳴らす
        sound.PlaySound(SoundType_Title.BGM,1.0f);
        Application.targetFrameRate = 30; //30FPSに設定

        m_uI_Title_Button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(!m_backGround_Title.ChangeFlag)
        {
            m_backGround_Title.ChangePlace();
        }
        else
        {
            m_backGround_Title.ChangeRobyScene();
        }


        time += Time.deltaTime;

        Debug.Log(time);
        if (time > 2)
        {
            m_uI_Title_Button.gameObject.SetActive(true);
        }

       


        if(m_uI_Title_Button.IsClick())
        {
            // スタート音を鳴らす
            sound.PlaySound(SoundType_Title.Start,1.0f);

            m_backGround_Title.ChangeFlag = true;
            m_uI_Title_Button.OnClickProcess();
        }

    }
}
