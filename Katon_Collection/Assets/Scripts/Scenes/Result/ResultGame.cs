using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGame : MonoBehaviour
{
    [SerializeField]
    Congratulation congratulation;
    [SerializeField]
    RankIcon rankIcon;
    [SerializeField]
    UI_Button_GoToTitle ui_Button_GoToTitle;
    [SerializeField]
    CameraResultMove cameraResultMove;
    [SerializeField]
    Owner_Floor owner_Floor;

    //吹き出し
    [SerializeField]
    UI_Fukidashi[] ui_Fukidashi = new UI_Fukidashi[4];

    float time = 0;
    Floor landingFloor = null;

    //プレイヤー数
    int[] playerResult = new int[4];

    int TopScore = 50;
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
        //Debug.Log(Screen.currentResolution.height);
       
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
        landingFloor = owner_Floor.GetTopLandingOf(Type.cave);
        if (landingFloor != null)
        {

            // 現在建築された回数
            int total = owner_Floor.GetTotalLandingFloor(Type.cave);
            //Debug.Log(total);
            for (int i = 0; i < 4; i++)
            {
                // 各プレイヤーの結果と同じ階だった場合
                if (total == playerResult[i])
                {
                    GameObject obj = landingFloor.gameObject;
                    //建った階数のところに吹き出しを出現させる
                    ui_Fukidashi[i].SetTarget(obj.transform.position);


                    Debug.Log("a");
                }
            }


            cameraResultMove.SetTarget(landingFloor.transform.position);
            cameraResultMove.Move();
        }
    }
}
