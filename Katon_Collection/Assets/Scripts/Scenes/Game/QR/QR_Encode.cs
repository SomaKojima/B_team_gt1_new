﻿using System.Collections;
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
        bool isNotFinedID = true;
        while (line != null)
        {
            if (line.Contains("ID"))
            {
                line = strReader.ReadLine();
                isNotFinedID = false;
                break;
            }
            line = strReader.ReadLine();
        }

        if (isNotFinedID) return false;
        
        id = int.Parse(line);

        // データの開始位置まで移動
        bool isNotFinedStartData = true;
        while (line != null)
        {
            if (line.Contains("START_DATA"))
            {
                line = strReader.ReadLine();
                isNotFinedStartData = false;
                break;
            }
            line = strReader.ReadLine();
        }

        if (isNotFinedStartData) return false;

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
            line = line.TrimStart(',');
            line = line.TrimEnd(',');

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
