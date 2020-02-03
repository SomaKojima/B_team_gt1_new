using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct CommonEncodeData
{
    public int time;
    public int neccesaryCount;
    public List<IItem> get_items;
    public List<IItem> pay_items;
    public bool is_item;

    public void Initailize()

    { 
        time = 0;
        neccesaryCount = 0;
        is_item = false;
        get_items = new List<IItem>();
        pay_items = new List<IItem>();
    }
}

public class Common_Encode
{
    Dictionary<string, ITEM_TYPE> itemTypeTable = new Dictionary<string, ITEM_TYPE>();

    List<CommonEncodeData> dataList = new List<CommonEncodeData>();

    public void Initialize()
    {
        for (int i = (int)ItemType.MinusHead; i < (int)ITEM_TYPE.NUM; i++)
        {
            string name = ((ITEM_TYPE)i).ToString();
            itemTypeTable.Add(name, (ITEM_TYPE)i);
        }
    }

    // エンコード
    public bool EncodeToItem(string code)
    {
        if (code == "") return false;
        StringReader strReader = new StringReader(code);

        string line = strReader.ReadLine();

        dataList.Clear();

        // ファイルの最後まで読み込む
        while (strReader.Peek() != -1)
        {
            // データの開始位置まで移動
            line = ContainsMoveLine(line, strReader, "DATA_START");

            // START_DATAまで移動できなかった
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

            // データを読み取る
            CommonEncodeData data = new CommonEncodeData();
            data.Initailize();
            while (line != null)
            {
                if (line.Contains("DATA_END"))
                {
                    break;
                }

                // 余分なカンマを削除
                line = RemoveConmma(line);

                EncodeToItem(line, ref data);
                // 次の行に移動
                line = strReader.ReadLine();
            }

            // リストに追加
            dataList.Add(data);

        }
        return true;
    }

    /// <summary>
    /// START_DATAからEND_DATAまでのエンコード
    /// </summary>
    /// <param name="line"></param>
    void EncodeToItem(string line, ref CommonEncodeData data)
    {
        if (line == "") return;

        // カンマ区切りで値を取得
        string[] valueStr = line.Split(',');

        // 一文字目で何のデータ化判定
        switch (valueStr[0])
        {
            // 時間の取得
            case "time":
                data.time = int.Parse(valueStr[1]);
                break;

            // 必要個数の取得
            case "necessary":
                data.neccesaryCount = int.Parse(valueStr[1]);
                break;

            // 手に入れるアイテムの取得
            case "get_item":
                EncodeItems(valueStr, data.get_items);
                break;

            // 支払うアイテムの取得
            case "pay_item":
                EncodeItems(valueStr, data.pay_items);
                break;

            case "is_item":
                data.is_item = bool.Parse(valueStr[1]);
                break;
        }
    }

    /// <summary>
    /// アイテムの取得
    /// </summary>
    /// <param name="valueStr"></param>
    /// <param name="refItems"></param>
    void EncodeItems(string[] valueStr, List<IItem> refItems)
    {
        int i = 1;
        while (i < valueStr.Length)
        {
            //　アイテムのタイプを取得
            string typeStr = valueStr[i];
            ITEM_TYPE type = itemTypeTable[typeStr];

            // itemの通常版の個数を取得
            i++;
            string countStr = valueStr[i];
            int normalCount = int.Parse(countStr);


            // itemの強化版の個数を取得
            i++;
            string powerUpCountStr = valueStr[i];
            int powerUpCount = int.Parse(powerUpCountStr);

            // itemのクラスを追加
            refItems.Add(new Item(normalCount, powerUpCount, type));
            i++;
        }
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

    public List<CommonEncodeData> GetDateList()
    {
        return dataList;
    }
}
