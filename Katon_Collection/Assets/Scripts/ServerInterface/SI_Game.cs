using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SI_Game : MonoBehaviour
{
    float rimitTime;
    bool isGameSet = false;
    public static int[] Scores;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameSet();
    }

    public void SetTime(float time)
    {
        rimitTime = time;
    }

    public float GetTime()
    {
        return rimitTime;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(rimitTime);
        }
        else
        {
            //データの受信
            this.rimitTime = (float)stream.ReceiveNext();
        }
    }

    void GameSet()
    {
        if(isGameSet)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }

    public void SetScore(int[] PLPoint)
    {
        Scores = new int[PLPoint.Length];
        for (int i = 0; i < PLPoint.Length; i++) 
        {
            Scores[i] = PLPoint[i];
        }
    }

    public bool IsGameSet
    {
        get { return isGameSet; }
        set { isGameSet = value; }
    }

    public static int[] GetScore()
    {
        return Scores;
    }
}
