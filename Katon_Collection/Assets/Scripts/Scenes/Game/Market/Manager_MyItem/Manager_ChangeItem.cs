using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_ChangeItem : MonoBehaviour
{
    // 交換するアイテムのリスト
    public List<CommonUnitIcon> myItems;

    // アイテム追加アイコンのブレハブ
    [SerializeField]
    GameObject addItemPrefab = null;
    // プレハブを生成する場所(親オブジェクト)
    [SerializeField]
    Transform addPrefabParent = null;
    // アイテム無しのブレハブ
    [SerializeField]
    GameObject enptyItemPrefab = null;
    // プレハブを生成する場所(親オブジェクト)
    [SerializeField]
    Transform enptyPrefabParent = null;

    // 交換するアイテムの種類の最大数
    public static int MAX_CHANGE_ITEM = 12;

    // 交換するアイテムの合計数
    private int totalItemCount = 0;
    // 交換するアイテムの合計数(テキスト)
    [SerializeField]
    Text totalItemCountText = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 交換するアイテムの残りを表示
    /// 交換するアイテムの種類が増減したらこの関数を呼ぶ
    /// </summary>
    public void LineupRemainItem()
    {
        if (myItems.Count < MAX_CHANGE_ITEM)
        {
            int difCount = MAX_CHANGE_ITEM - myItems.Count;

            for (int i = 0; i < difCount; i++)
            {
                // 初回は追加アイコンにする
                if (i == 0) Instantiate(addItemPrefab, addPrefabParent);
                // 残りの余りの枠をアイテム無しのアイコンで埋める
                else Instantiate(enptyItemPrefab, enptyPrefabParent);
            }
        }
    }

    /// <summary>
    /// 交換するアイテムの合計数を表示
    /// 交換するアイテムの個数が増減したらこの関数を呼ぶ
    /// </summary>
    public void DisplayTotalCount()
    {
        foreach (CommonUnitIcon cmnUntIcn in myItems)
        {
            totalItemCount += cmnUntIcn.GetCount();
        }

        totalItemCountText.text = "合計：" + totalItemCount + "個";
    }

    /// <summary>
    /// アイテムを増やす
    /// </summary>
    /// <param name="item">増やすボタン</param>
    public void Add(CommonUnitIcon item)
    {
        myItems.Add(item);
    }

    /// <summary>
    /// 交換するアイテムリストを取得
    /// </summary>
    /// <returns>ボタンのリスト</returns>
    public List<CommonUnitIcon> GetChangeItems()
    {
        return myItems;
    }
}
