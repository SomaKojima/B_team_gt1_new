using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;


public class QR_Reader : MonoBehaviour
{
    const string ERROR_TEXT = "error";
    //QRコードの情報
    string m_infoQR = "";

    // ウェブカメラのテクスチャ
    private WebCamTexture webCam = null;
    //カメラ起動フラグ
    private bool isPlayCamera = false;
    
    // camera表示オブジェクト
    [SerializeField]
    RawImage cameraImage = null;

    bool isStop = false;

    BarcodeReader reader = null;

    WebCamDevice[] devices = null;

    float readFrame = 0;
    float readDuring = 2.0f;

    private void Update()
    {
        if (webCam != null)
        {
            cameraImage.rectTransform.rotation = Quaternion.AngleAxis(webCam.videoRotationAngle, Vector3.back);
        }
        if (webCam != null && !isStop)
        {
            readFrame += Time.deltaTime;
            if (readFrame > readDuring)
            {
                readFrame = 0;
                m_infoQR = Read(webCam);
            }
        }
    }

    public void Initialize()
    {
        Debug.Log("start");
        m_infoQR = "";
        isStop = false;
        if (reader == null)
        {
            reader = new BarcodeReader()
            {
                AutoRotate = true,
                Options = {
                PossibleFormats = new[] { BarcodeFormat.QR_CODE }
            }
            };
        }
        // ウェブカメラのデバイス数を取得
        if (devices == null)
        {
            devices = WebCamTexture.devices;
        }

        ActivationWebCamera();

        StartRead();
    }


    //コード情報の取得
    public string GetQRCode()
    {
        return m_infoQR;
    }

    // コードを読み取れたかどうか
    public bool IsCorrectRead()
    {
        if (m_infoQR == "") return false;
        if (m_infoQR == ERROR_TEXT) return false;
        return true;
    }

    /// <summary>
    /// Webカメラの起動
    /// </summary>
    /// <returns></returns>
    public bool ActivationWebCamera()
    {
        //yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        //if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        //{
        //    Debug.LogFormat("no camera.");
        //    yield break;
        //}
        //Debug.LogFormat("camera ok.");



        Debug.Log(devices);
        // デバイスが存在しなかったら、もしくはすでに起動していたら
        if (devices == null || devices.Length == 0 || isPlayCamera)
            //yield break;
            return false;
        
        // ウェブカメラオブジェクトを生成
        webCam = new WebCamTexture(devices[0].name, (int)(Screen.width), (int)(Screen.height), 60);
        cameraImage.texture = webCam;
        // ウェブカメラを起動
        webCam.Play();
        isPlayCamera = true; 

        // 起動成功
        return true;
    }


    /// <summary>
    /// ウェブカメラから読み取り
    /// 簡易エラー処理あり
    /// </summary>
    /// <param name="tex">WebCamTexture(カメラに映ったテクスチャ)</param>
    /// <returns>読み取り成功=読みとった文字列、読み取り失敗="error"文字列</returns>
    public string Read(WebCamTexture tex)
    {
        if (tex == null) return "";
        // コードリーダーオブジェクト生成
        // テクスチャの幅、高さ、色情報を設定
        int w = tex.width;
        int h = tex.height;
        var pixel32s = tex.GetPixels32();
        // テキスト情報取得
        var r = reader.Decode(pixel32s, w, h);

        // 読み取れたらテキスト情報を、
        // 読み取れなかったら"ERROR"を返す
        if (r != null)
            return r.Text;
        else
            return ERROR_TEXT;
    }


    public void StartRead()
    {
        isStop = false;
    }

    public void StopRead()
    {
        isStop = true;
    }

    public bool IsStop()
    {
        return isStop;
    }
}
