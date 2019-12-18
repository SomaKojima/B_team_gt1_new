using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class QR_ReaderWindow : MonoBehaviour
{
    [SerializeField]
    QR_Reader qrReader;

    [SerializeField]
    QR_ResultWindow correctResultWindow;

    [SerializeField]
    QR_ResultWindow missResultWindow;

    [SerializeField]
    UI_Button backButton;

    List<IItem> items = new List<IItem>();

    IItem[] buf = new IItem[20];

    bool isExchange = false;

    public void Initialize()
    {
        gameObject.SetActive(true);
        qrReader.Initialize();
        correctResultWindow.Initialize();
        missResultWindow.Initialize();
        isExchange = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            buf[i] = new Item();
            buf[i].Initialize(0, ITEM_TYPE.NONE);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (backButton.IsClick())
        {
            backButton.OnClickProcess();
            gameObject.SetActive(false);
        }

        if (qrReader.IsCorrectRead() && !qrReader.IsStop())
        {
            Debug.Log("DEBUG");
            qrReader.StopRead();
            //QRコードをItemにエンコードする
            if (!EncodeToItem(qrReader.GetQRCode(), items))
            {
                // 読み込みエラー
                missResultWindow.Active();
            }
            else
            {
                // 読み込み成功
                correctResultWindow.Active();
            }

            Debug.Log(qrReader.GetQRCode());
        }

        // 交換をする
        if (correctResultWindow.IsClickYes())
        {
            qrReader.Initialize();
            correctResultWindow.Initialize();
            Exchange();
        }

        // 読み込みエラーの場合と　成功時のキャンセルボタンの処理
        if (missResultWindow.IsClickYes() || missResultWindow.IsClickNo() ||
            correctResultWindow.IsClickNo())
        {
            qrReader.Initialize();
            correctResultWindow.Initialize();
            missResultWindow.Initialize();
        }
    }

    // エンコード
    bool EncodeToItem(string code, List<IItem> refList)
    {
        StringReader strReader = new StringReader(code);

        bool isNotFinedStartData = true;
        string line = strReader.ReadLine();
        // データの開始位置まで移動
        while (line != null)
        {
            if (line.Contains("START_DATA"))
            {
                line = strReader.ReadLine();
                isNotFinedStartData = false;
                break;
            }
            line = strReader.ReadLine();
        }

        if (isNotFinedStartData) return false;

        // データをエンコードする

        int bufIndex = 0;
        items.Clear();
        while (line != null)
        {
            if (line.Contains("END_DATA"))
            {
                break;
            }
            // 余分なカンマを削除
            line = line.TrimStart(',');
            line = line.TrimEnd(',');

            // カンマ区切りで値を取得
            string[] valueStr = line.Split(',');
            int[] values = new int[valueStr.Length];
            // 文字列を数値に変換
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = int.Parse(valueStr[i]);
            }


            // クラスを作成-------------------------------------------------------------------
            buf[bufIndex].Initialize(values[1], (ITEM_TYPE)values[0]);
            refList.Add(buf[bufIndex]);
            bufIndex++;

            line = strReader.ReadLine();
        }

        return true;
    }

    // 交換処理が終わったときに呼ぶ
    public void FinishExchange()
    {
        isExchange = false;
    }

    public bool IsExchange()
    {
        return isExchange;
    }

    private void Exchange()
    {
        gameObject.SetActive(false);
        isExchange = true;
    }

    public List<IItem> GetItems()
    {
        return items;
    }
}
