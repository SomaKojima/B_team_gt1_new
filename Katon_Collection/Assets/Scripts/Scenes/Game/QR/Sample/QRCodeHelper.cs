//--------------------------------------------------------
// File.    QRCodeHelper.cs
// Summary. QRCodeHelperClass
//          Read QRcode. Create QRcode.
// Date.    2019/10/17
// Auther.  Miu Himing
//--------------------------------------------------------

// usingディレクトリ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;


public class QRCodeHelper
{
    // QRコードのサイズ
    const int QR_CODE_SIZE = 256;

    /// <summary>
    /// テクスチャから読み取り
    /// </summary>
    /// <param name="tex">テクスチャ情報</param>
    /// <returns>読みとった文字列</returns>
    static public string Read(Texture2D tex)
    {
        BarcodeReader reader = new BarcodeReader();
        int w = tex.width;
        int h = tex.height;
        var pixel32s = tex.GetPixels32();
        var r = reader.Decode(pixel32s, w, h);
        return r.Text;
    }

    /// <summary>
    /// ウェブカメラから読み取り
    /// 簡易エラー処理あり
    /// </summary>
    /// <param name="tex">WebCamTexture(カメラに映ったテクスチャ)</param>
    /// <returns>読み取り成功=読みとった文字列、読み取り失敗="error"文字列</returns>
    public static string Read(WebCamTexture tex)
    {
        // コードリーダーオブジェクト生成
        BarcodeReader reader = new BarcodeReader();
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
            return "error";
    }

    /// <summary>
    /// ウェブカメラから読み取り
    /// 簡易エラー処理なし
    /// </summary>
    /// <param name="tex">WebCamTexture(カメラに映ったテクスチャ)</param>
    /// <returns>読みとった文字列</returns>
    static public Result Read2(WebCamTexture tex)
    {
        // コードリーダーオブジェクト生成
        BarcodeReader reader = new BarcodeReader();
        // テクスチャの幅、高さ、色情報を設定
        int w = tex.width;
        int h = tex.height;
        var pixel32s = tex.GetPixels32();
        // テキスト情報取得
        Result r = reader.Decode(pixel32s, w, h);

        // テキスト情報を返す
        return r;
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
        var content = Write(str, width, height);
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
