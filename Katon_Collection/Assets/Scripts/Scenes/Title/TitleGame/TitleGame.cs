using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGame : MonoBehaviour
{

    [SerializeField]
    private BackGround_Title m_backGround_Title = null;

    [SerializeField]
    private UI_Button_Title m_uI_Title_Button = null;

    // サウンド
    [SerializeField]
    Sound_Title sound;

    void Awake()
    {
        Application.targetFrameRate = 30; //30FPSに設定
    }

    // Start is called before the first frame update
    void Start()
    {
        // BGMを鳴らす
        sound.PlaySound(SoundType_Title.BGM, 1.0f);
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
       


        if(m_uI_Title_Button.IsClick())
        {
            // スタート音を鳴らす
            sound.PlaySound(SoundType_Title.Start,1.6f);

            m_backGround_Title.ChangeFlag = true;
            m_uI_Title_Button.OnClickProcess();
        }

    }
}
