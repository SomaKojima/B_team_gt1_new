using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRcodeManager : MonoBehaviour
{
    // ウェブカメラのテクスチャ
    private WebCamTexture webCam = null;
    //カメラ起動フラグ
    private bool isPlayCamera = false;

    // QRコードを生成する文字列
    private string createQRStrList = "null";

    // QR表示オブジェクト
    [SerializeField]
    RawImage qrImage = null;

    // 文字列から生成して表示するか読み取りを表示するか
    bool isRead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        // ウェブカメラのデバイス数を取得
        WebCamDevice[] devices = WebCamTexture.devices;
        // デバイスが存在しなかったら、もしくはすでに起動していたら
        if (devices == null || devices.Length == 0 || isPlayCamera)
            //yield break;
            return false;

        // ウェブカメラオブジェクトを生成
        webCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 12);
        // ウェブカメラを起動
        webCam.Play();

        // 起動成功
        return true;
    }

    /// <summary>
    /// Webカメラの停止
    /// </summary>
    /// <returns></returns>
    public void DeactivationWebCamera()
    {
        webCam.Stop();
    }

    /// <summary>
    /// 文字列をQRコードテクスチャにする
    /// Texture型で返す
    /// </summary>
    /// <param name="str">QRコードを生成する文字列</param>
    /// <returns>QRコードテクスチャ</returns>
    public Texture CreateQRcode(string str)
    {
        // 指定文字列からQRテクスチャ生成
        return QRCodeHelper.CreateQRCode(str, 256, 256);
    }

    /// <summary>
    ///  カメラ起動フラグの取得、設定
    /// </summary>
    public WebCamTexture WebCam
    {
        get { return webCam; }
    }

    /// <summary>
    ///  カメラ起動フラグの取得、設定
    /// </summary>
    public bool IsPlayCamera
    {
        get { return isPlayCamera; }
        set { isPlayCamera = value; }
    }

    /// <summary>
    /// QRコードの状態を設定
    /// </summary>
    /// <param name="flag">ture=アクティブ、false=非アクティブ</param>
    public void SetActiveQRcode(bool flag)
    {
        if (flag)
            qrImage.gameObject.SetActive(true);
        else
            qrImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// QRコード生成文字列の取得、設定
    /// </summary>
    public string CreateQRStrList
    {
        get { return createQRStrList; }
        set { createQRStrList = value; }
    }

    /// <summary>
    /// QRコードの取得、設定
    /// </summary>
    public Texture QRImage
    {
        get { return qrImage.texture; }
        set { qrImage.texture = value; }
    }

    /// <summary>
    /// フラグの取得
    /// </summary>
    public bool IsRead
    {
        get { return isRead; }
    }
    /// <summary>
    /// フラグの反転
    /// </summary>
    public void SetIsRead()
    {
        isRead = !isRead;
    }

}
