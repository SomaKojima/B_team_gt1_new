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
    QR_ResultWindow missExchangeResultWindow;

    [SerializeField]
    UI_Button backButton;

    List<IItem> items = new List<IItem>();

    IItem[] buf = new IItem[20];

    QR_Encode qr_encode = new QR_Encode();

    bool isExchange = false;

    int otherID = -1;

    bool isReader = false;

    bool isActive = false;

    bool isBack = false;

    public void Initialize()
    {
        
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
        isBack = false;
        if (backButton.IsClick())
        {
            backButton.OnClickProcess();
            isBack = true;
        }

        if (qrReader.IsCorrectRead() && !qrReader.IsStop())
        {
            qrReader.StopRead();
            //QRコードをItemにエンコードする
            if (!qr_encode.EncodeToItem(qrReader.GetQRCode(), items, ref otherID))
            {
                // 読み込みエラー
                missResultWindow.Active();
            }
            else
            {
                Debug.Log(items);
                // 読み込み成功
                correctResultWindow.Active();
            }
        }

        // 交換をする
        isReader = false;
        if (correctResultWindow.IsClickYes())
        {
            qrReader.Initialize();
            correctResultWindow.Initialize();
            Exchange();
            isReader = true;
        }

        // 読み込みエラーの場合と　成功時のキャンセルボタンの処理
        if (missResultWindow.IsClickYes() || missResultWindow.IsClickNo() ||
            correctResultWindow.IsClickNo() || missExchangeResultWindow.IsClickNo() ||
            missExchangeResultWindow.IsClickYes())
        {
            qrReader.Initialize();
            correctResultWindow.Initialize();
            missResultWindow.Initialize();
            missExchangeResultWindow.Initialize();
        }
    }

    public void Active()
    {
        if (gameObject.activeSelf) return;
        gameObject.SetActive(true);
        qr_encode.Initialize();
        qrReader.Initialize();
        correctResultWindow.Initialize();
        missResultWindow.Initialize();
        isExchange = false;
        isActive = true;
        isBack = false;
    }

    public void UnActive()
    {
        if (!gameObject.activeSelf) return;
        gameObject.SetActive(false);
        isActive = false;
    }

    

    // 交換処理が終わったときに呼ぶ
    public void FinishExchange(bool _isExchangable)
    {
        if (_isExchangable)
        {
            // 交換成功時の処理
            gameObject.SetActive(false);
        }
        else
        {
            // 交換失敗時の処理
            missExchangeResultWindow.Active();
        }
        isExchange = false;
    }

    public bool IsExchange()
    {
        return isExchange;
    }

    private void Exchange()
    {
        isExchange = true;
    }

    public List<IItem> GetItems()
    {
        return items;
    }

    public int GetOtherID()
    {
        return otherID;
    }

    public bool IsReader()
    {
        return isReader;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public bool IsBack()
    {
        return isBack;
    }
}
