using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QR_Code : MonoBehaviour
{
    // camera表示オブジェクト
    [SerializeField]
    RawImage cameraImage = null;

    public void Initialize(string str)
    {
        cameraImage.texture = CreateQRCode(str,
            256,
            256);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// QRコード作成
    /// </summary>
    /// <param name="str">QRコード生成元の文字列</param>
    /// <param name="width">テクスチャの幅</param>
    /// <param name="height">テクスチャの高さ</param>
    /// <returns>テクスチャ情報(QRコード)</returns>
    static public Texture2D CreateQRCode(string str, int width, int height)
    {
        // テクスチャ情報からテクスチャオブジェクト生成
        var tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        // QRコード生成のためのカラー情報を設定
        var content = Write(str, tex.width, tex.height);
        // テクスチャ情報を設定
        tex.SetPixels32(content);
        // テクスチャ情報適用
        tex.Apply();

        // テクスチャ情報を返す
        return tex;
    }

    static Color32[] Write(string content, int w, int h)
    {
        Debug.Log(content + " / " + w + " / " + h);

        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Width = w,
                Height = h
            }
        };
        return writer.Write(content);
    }
}
