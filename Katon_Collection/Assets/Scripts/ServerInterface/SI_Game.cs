using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SI_Game : MonoBehaviour
{
    float rimitTime;
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
        if(rimitTime<0)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
