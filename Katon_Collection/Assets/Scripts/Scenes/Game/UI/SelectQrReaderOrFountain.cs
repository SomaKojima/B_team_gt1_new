using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectQrReaderOrFountain : MonoBehaviour
{
    [SerializeField]
    QR_ReaderWindow qrReader;

    [SerializeField]
    FountainWindow fountain;

    [SerializeField]
    UI_Button selectQr;

    [SerializeField]
    UI_Button selectFountain;

    [SerializeField]
    UI_Button backButton;

    bool isSelectQrReader = false;

    bool isSelectFountain = false;

    bool isBack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isSelectQrReader = false;
        if (selectQr.IsClick())
        {
            selectQr.OnClickProcess();
            isSelectQrReader = true;
        }

        isSelectFountain = false;
        if (selectFountain.IsClick())
        {
            selectFountain.OnClickProcess();
            isSelectFountain = true;
        }

        isBack = false;
        if (backButton.IsClick())
        {
            backButton.OnClickProcess();
            UnActive();
            isBack = true;
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
        isSelectQrReader = false;
        isSelectFountain = false;
    }

    public bool IsSelectQrReader()
    {
        return isSelectQrReader;
    }

    public bool IsSelectFountain()
    {
        return isSelectFountain;
    }

    public bool IsBack()
    {
        return isBack;
    }
}
