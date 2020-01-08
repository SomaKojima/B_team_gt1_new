using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_ChangeItem : MonoBehaviour
{
    // 交換アイテムのブレハブ
    [SerializeField]
    GameObject changeItemPrefab = null;
    // プレハブを生成する場所(親オブジェクト)
    [SerializeField]
    Transform prefabParent = null;

    // Factory_CommonUnitIconオブジェクト
    [SerializeField]
    Factory_CommonUnitIcon factory_CommonUnitIcon;

    [SerializeField]
    ItemContextTable table;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 交換するアイテムを生成
    /// </summary>
    /// <param name="changeItems">交換するアイテムのリスト</param>
    /// <returns>交換するアイテム(情報)</returns>
    public CommonUnitIcon Create(ITEM_TYPE type, int count)
    {
        // 交換するアイテム(アイコン)(実体)の生成
        GameObject obj = Instantiate(changeItemPrefab, prefabParent);
        obj.tag = "CountingItem";
        // オブジェクトからChangeItemのコンポーネントを取得
        CommonUnitIcon cngItm = obj.GetComponent<CommonUnitIcon>();
        // 種類に応じたスプライトを適用
        Sprite sprite = table.GetItemContex(type).GetSprite();
        // 交換するアイテムの初期化
        cngItm.Initialize(sprite, count);

        return cngItm;
    }
}
