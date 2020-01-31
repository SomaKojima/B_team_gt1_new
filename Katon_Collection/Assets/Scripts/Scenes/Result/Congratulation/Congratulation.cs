using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Congratulation : MonoBehaviour
{

    //テキスト格納庫
    [SerializeField]
    private TextMeshProUGUI[] m_text = new TextMeshProUGUI[4];

    int index = 0;


    // Start is called before the first frame update
    private void Start()
    {
        this.gameObject.SetActive(false);

        for(int i=0;i<4;i++)
        {
            m_text[i].gameObject.SetActive(false);
        }
    }

    //一位のプレイヤーを取得
    public void SetPlayerNumber(int num)
    {
        index = num;


        switch(index)
        {
            case 0:
                m_text[0].gameObject.SetActive(true);
                break;
            case 1:
                m_text[1].gameObject.SetActive(true);
                break;
            case 2:
                m_text[2].gameObject.SetActive(true);
                break;
            case 3:
                m_text[3].gameObject.SetActive(true);
                break;
        }

    }


}
