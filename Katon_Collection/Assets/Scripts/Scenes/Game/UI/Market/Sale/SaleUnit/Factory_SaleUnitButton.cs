using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory_SaleUnitButton : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    RectTransform parent;


    /// <summary>
    /// ボタンを生成
    /// </summary>
    /// <param name="getItems">入手できるアイテムのリスト</param>
    /// <param name="requiredNum">要求するアイテムの総数</param>
    /// <returns>Commonボタン(情報)</returns>
    public SaleUnitButton Create(List<IItem> getItems, List<IItem> payItems)
    {
        // ボタン(実体)の生成
        GameObject obj = Instantiate(prefab, parent);
        // オブジェクトからCommonUnitButtonのコンポーネントを取得
        SaleUnitButton saleUnitBtn = obj.GetComponent<SaleUnitButton>();
        // Commonボタンの初期化
        saleUnitBtn.Initialize(getItems, payItems);

        return saleUnitBtn;
    }
}
