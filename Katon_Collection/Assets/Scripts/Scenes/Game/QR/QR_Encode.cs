using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QR_Encode
{
    const string ERROR_TEXT = "error";
    IItem[] buf = new IItem[40];

    // Start is called before the first frame update
    public void Initialize()
    {
        for (int i = 0; i < 40; i++)
        {
            buf[i] = new Item();
            buf[i].Initialize(0, ITEM_TYPE.NONE);
        }
    }
    
    // エンコード
    public bool EncodeToItem(string code, List<IItem> refList, ref int id)
    {
        if (code == "") return false;
        StringReader strReader = new StringReader(code);

        string line = strReader.ReadLine();

        // データの開始位置まで移動
        line = ContainsMoveLine(line, strReader, "ID");

        // IDまで移動できなかった
        if (line == null)
        {
            return false;
        }
        else
        {
            // IDを読み込み
            line = strReader.ReadLine();
            id = int.Parse(line);
        }

        // データの開始位置まで移動
        line = ContainsMoveLine(line, strReader, "START_DATA");

        // IDまで移動できなかった
        if (line == null)
        {
            return false;
        }
        else
        {
            // IDを読み込み
            line = strReader.ReadLine();
        }

        // データをエンコードする
        int bufIndex = 0;
        refList.Clear();
        while (line != null)
        {
            if (line.Contains("END_DATA"))
            {
                break;
            }

            // 余分なカンマを削除
            line = RemoveConmma(line);

            if(line != "")
            {
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
            }

            line = strReader.ReadLine();
        }

        return true;
    }

    /// <summary>
    /// 余分なカンマを削除
    /// </summary>
    /// <param name="_line"></param>
    string RemoveConmma(string _line)
    {
        // 余分なカンマを削除
        _line = _line.TrimStart(',');
        _line = _line.TrimEnd(',');
        return _line;
    }


    /// <summary>
    /// 文字が含まれているところまで移動
    /// </summary>
    /// <param name="line"></param>
    /// <param name="strReader"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    string ContainsMoveLine(string line, StringReader strReader, string value)
    {
        while (line != null)
        {
            // 文字が含まれているかどうか
            if (line.Contains(value))
            {
                return line;
            }
            line = strReader.ReadLine();
        }
        return null;
    }

    public bool EncodeToQRCode(List<IItem> itemList, ref string code)
    {
        if (itemList.Count == 0) return false;

        code = "ID\n";
        code += PhotonNetwork.player.ID;
        code += "\n";

        code += "START_DATA\n";

        foreach(IItem item in itemList)
        {
            code += ((int)item.GetItemType());
            code += ",";
            code += item.GetCount();
            code += "\n";
        }

        code += "\nEND_DATA";
        return true;
    }
}
