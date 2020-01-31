using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultGame : MonoBehaviour
{
    [SerializeField]
    private Congratulation congratulation = null;
    [SerializeField]
    RankIcon rankIcon;
    [SerializeField]
    CameraResultMove cameraResultMove;
    [SerializeField]
    Owner_Floor owner_Floor;
    //ボタンを押した
    [SerializeField]
    private UI_Button_GoToTitle gototitle_button = null;

    //吹き出し
    [SerializeField]
    UI_Fukidashi[] ui_Fukidashi = new UI_Fukidashi[4];

    float time = 0;
    Floor landingFloor = null;

    //プレイヤー数
    int[] playerResult = new int[4];

    //表示フラグ
    bool m_gotoTapButtonFlag = false;

    int TopScore = 8;
    int count = 0;


    void Awake()
    {
        Application.targetFrameRate = 30; //60FPSに設定
    }


    // Start is called before the first frame update
    void Start()
    {
        owner_Floor.Initialize();
        for (int i = 0; i < 4; i++)
        {
            playerResult[i] = i+i;
        }

       
    }

    // Update is called once per frame
    void Update()
    {
       
       
        time++;
        if(time > 60)
        {
            if (TopScore > count)
            {
                count++;
                owner_Floor.Building(Type.cave);
            }

           
            time = 0;
        }

        //一番高く建てた人のスコアを超えたら
        if (count >= TopScore)
        {
            m_gotoTapButtonFlag = true;
        }

        //勝利のパーティクル、テキスト、タイトルに戻るボタンを表示
        if (m_gotoTapButtonFlag)
        {
            gototitle_button.gameObject.SetActive(true);
            congratulation.gameObject.SetActive(true);

            congratulation.SetPlayerNumber(1);
        }

        landingFloor = owner_Floor.GetTopLandingOf(Type.cave);
        if (landingFloor != null)
        {

            // 現在建築された回数
            int total = owner_Floor.GetTotalLandingFloor(Type.cave);
            //Debug.Log(total);
            for (int i = 0; i < 4; i++)
            {
                if(owner_Floor.GetPlaceTotalFloor(Type.cave)<=playerResult[i])
                {
                    GameObject obj = landingFloor.gameObject;
                    //建った階数のところに吹き出しを出現させる
                    ui_Fukidashi[i].SetTarget(obj.transform.position);
                }
              
            }


            cameraResultMove.SetTarget(landingFloor.transform.position);
            cameraResultMove.Move();
        }

        //シーン切り替え
        if(gototitle_button.IsClick())
        {
            SceneManager.LoadScene("TitleScene");
        }
        
    }
}
