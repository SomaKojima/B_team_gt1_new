//--------------------------------------------------------
// File.    SampleQRReader.cs
// Summary. SampleQRReaderClass
//          QRcode reader.
// Date.    2019/10/18
// Auther.  Miu Himing
//--------------------------------------------------------

// usingディレクトリ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SampleQRReader : MonoBehaviour
{
    // ウェブカメラのテクスチャ
    private WebCamTexture webCam = null;

    // QRコードを生成する文字列
    [SerializeField]
    private string qrStr = null;
    // QRコードテクスチャ
    private Texture2D qrTexture = null;

    // RawImageオブジェクト
    public RawImage image = null;
    public RawImage picImage = null;

    // QRコード読みとりテキスト
    private string readStr = null;

    // 読み取りに成功した文字列を保存
    private List<string> readStrList = new List<string>();


    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
            Debug.LogFormat("no camera.");
            yield break;
        }
        Debug.LogFormat("camera ok.");

        // ウェブカメラのデバイス数を取得
        WebCamDevice[] devices = WebCamTexture.devices;
        // デバイスが存在しなかったら
        if (devices == null || devices.Length == 0)
            yield break;

        // ウェブカメラオブジェクトを生成
        webCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 12);
        // ウェブカメラを起動
        webCam.Play();
    }

    void Update()
    {
        // ウェブカメラが存在していたら
        if (webCam != null)
        {
            // QRコード読み取り
            readStr = QRCodeHelper.Read(webCam);
            // 結果を表示
            Debug.LogFormat("result : " + readStr);
        }

        // 読み取った文字列があれば
        if(readStr != null && readStr != "error")
        {
            // 読み取った文字列のチェック
            if (CheckContainStr(readStrList, readStr))
            {
                // まだ追加されていなければ文字列の追加
                readStrList.Add(readStr);
                qrStr = readStr;
            }
        }
        
        // 文字列が指定されていたら
        if (qrStr != null)
        {
            // 指定文字列からQRテクスチャ生成
            qrTexture = QRCodeHelper.CreateQRCode(qrStr, 256, 256);
        }

        // テクスチャを表示
        if (qrTexture != null)
        {
            // そのまま表示
            image.texture = qrTexture;
        }

        // Webカメラが存在していたら
        if(webCam != null)
        {
            // 映っているものをそのまま表示
            picImage.texture = webCam;
        }
        // Bキー押下で
        if (Input.GetKeyDown(KeyCode.B))
        {
            // ファイルの存在確認
            if(CheckFile("Resources/QRcode/qrcode.png"))
            {
                Debug.Log("めっけー");
            }
            else
            {
                Debug.Log("どこー");
            }
        }

        // SPACEキー押下で
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 画像を保存
            SaveQRCodeJPG(qrTexture, "Resources/QRcode", "qrcode");
        }
    }

    /// <summary>
    /// 文字列がリストに入っているかをチェックする
    /// </summary>
    /// <param name="checkList">チェックするリスト</param>
    /// <param name="checkStr">チェックする文字列</param>
    /// <returns>true=入っていない、false=入っている</returns>
    private bool CheckContainStr(List<string> checkList, string checkStr)
    {
        for(int i = 0; i < checkList.Count; i++)
        {
            if (checkList[i] == checkStr) return false;
        }
        return true;
    }

    /// <summary>
    /// ファイルが存在するかどうかを判断
    /// Assets直下からパスを指定
    /// </summary>
    /// <param name="checkFile">チェックするファイル</param>
    /// <returns>true=存在、false=存在しない</returns>
    private bool CheckFile(string checkFile)
    {
        // フォルダが存在しなかったら
        if (!File.Exists("Assets/" + checkFile))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// ディレクトリが存在するかどうかを判断
    /// Assets直下からパスを指定
    /// </summary>
    /// <param name="checkDir">チェックするディレクトリ</param>
    /// <returns>true=存在、false=存在しない</returns>
    private bool CheckDirectory(string checkDir)
    {
        // フォルダが存在しなかったら
        if (!Directory.Exists("Assets/" + checkDir))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// QRコードをJPG画像として保存
    /// </summary>
    /// <param name="saveTex">保存するテクスチャ</param>
    /// <param name="savePath">保存するディレクトリ</param>
    /// <param name="saveName">保存するファイル名</param>
    private void SaveQRCodeJPG(Texture2D saveTex, string savePath, string saveName)
    {
        // フォルダが存在しなかったら
        if (!CheckDirectory(savePath))
        {
            // フォルダを作成
            Directory.CreateDirectory("Assets/" + savePath);
        }

        // エンコード形式を設定
        var bytes = saveTex.EncodeToJPG();
        // ファイルに保存(パス、ファイル名を指定する)
        File.WriteAllBytes(Application.dataPath + "/" + savePath + "/" + saveName + ".jpg", bytes);

        // 結果を表示
        string saveDirectory = Application.dataPath + "/" + savePath + "/" + saveName + ".jpg";
        Debug.LogFormat("画像を保存しました：" + saveDirectory);
    }

    /// <summary>
    /// QRコードをPNG画像として保存
    /// </summary>
    /// <param name="saveTex">保存するテクスチャ</param>
    /// <param name="savePath">保存するディレクトリ</param>
    /// <param name="saveName">保存するファイル名</param>
    private void SaveQRCodePNG(Texture2D saveTex, string savePath, string saveName)
    {
        // フォルダが存在しなかったら
        if (!CheckDirectory(savePath))
        {
            // フォルダを作成
            Directory.CreateDirectory("Assets/" + savePath);
        }
        
        // エンコード形式を設定
        var bytes = saveTex.EncodeToPNG();
        // ファイルに保存(パス、ファイル名を指定する)
        File.WriteAllBytes(Application.dataPath + "/" + savePath + "/" + saveName + ".png", bytes);

        // 結果を表示
        string saveDirectory = Application.dataPath + "/" + savePath + "/" + saveName + ".png";
        Debug.LogFormat("画像を保存しました：" + saveDirectory);
    }

    //------------------------------Getter,Setter------------------------------//
    /// <summary>
    /// URL文字列取得・設定関数
    /// </summary>
    public string QRStr { get { return qrStr; } set { qrStr = value; } }
    //-------------------------------------------------------------------------//
}