using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QR_Reader : MonoBehaviour
{
    //QRコードの情報
    string m_infoQR;

    //コード情報の取得
    public string GetQRCode()
    {
        return m_infoQR;
    }
}
