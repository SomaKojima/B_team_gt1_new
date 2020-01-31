using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Time : MonoBehaviour
{
    //　トータル制限時間
    private float totalTime;
    //　制限時間（分）
    [SerializeField]
    private int minute;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;

   //　前回Update時の秒数
    private float oldSeconds;

    [SerializeField]
    GameObject SI_Game;

    // サウンド
    [SerializeField]
    Sound_MainGame sound;

    private Text timerText;
    int soundTime = 0;

    void Start()
    {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        timerText = GetComponentInChildren<Text>();

        SI_Game.GetComponent<SI_Game>().SetTime(totalTime);
    }

    void Update()
    {
        //　制限時間が0秒以下なら何もしない
        if (totalTime <= 0f)
        {
            return;
        }
        //　一旦トータルの制限時間を計測；
        //totalTime = minute * 60 + seconds;
        //totalTime -= Time.deltaTime;
        totalTime = SI_Game.GetComponent<SI_Game>().GetTime() - Time.deltaTime;
        SI_Game.GetComponent<SI_Game>().SetTime(totalTime);

        //　再設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        //　タイマー表示用UIテキストに時間を表示する
        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;
        //　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
        if (totalTime <= 0f)
        {
            Debug.Log("制限時間終了");
            // タイムアップ
            sound.PlaySound(SoundType_MainGame.TimeUp, 1.0f);
        }

        // カウントダウン
        if ((int)GetSecond() != soundTime &&
            (int)GetMinute() <= 0 &&
            (int)GetSecond() <= 10)
        {
            // カウントダウン
            Debug.Log("a");
            sound.PlaySound(SoundType_MainGame.CountDown, 1.0f);
            soundTime = (int)GetSecond();
        }
    }

    public float GetTotalTime()
    {
        return totalTime;
    }

    public float GetSecond()
    {
        return seconds;
    }

    public float GetMinute()
    {
        return minute;
    }
}
