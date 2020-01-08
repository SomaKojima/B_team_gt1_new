using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Floor_Encode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool EncodeCsvToItem(TextAsset csvFile, List<IItem> refList)
    {
        return EncodeToItem(csvFile.text, refList);
    }

    public bool EncodeToItem(string code, List<IItem> refList)
    {
        if (code == "") return false;
        StringReader strReader = new StringReader(code);

        string line = strReader.ReadLine();
        
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

            if (line != "")
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
                IItem item = new Item();
                item.Initialize(values[1], (ITEM_TYPE)values[0]);
                refList.Add(item);
                bufIndex++;
            }

            line = strReader.ReadLine();
        }

        return true;
    }
}
