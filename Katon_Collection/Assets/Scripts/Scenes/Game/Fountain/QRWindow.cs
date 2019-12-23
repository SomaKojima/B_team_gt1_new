using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRWindow : MonoBehaviour
{
    [SerializeField]
    UI_Button back;

    [SerializeField]
    QR_Code qrCode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (back.IsClick())
        {
            back.OnClickProcess();
            UnActive();
        }
    }

    public void Active(string str)
    {
        qrCode.Initialize(str);
        gameObject.SetActive(true);
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
    }
}
