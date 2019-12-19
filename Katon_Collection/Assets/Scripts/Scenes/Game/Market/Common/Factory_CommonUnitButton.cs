using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_CommonUnitButton : MonoBehaviour
{
    // Commonボタンのブレハブ
    [SerializeField]
    GameObject commonUnitPrefab = null;
    // プレハブを生成する場所(親オブジェクト)
    [SerializeField]
    Transform  prefabParent = null;

    // CommonUnitButtonオブジェクト
    [SerializeField]
    CommonUnitButton commonUnitButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ボタンを生成
    /// </summary>
    /// <param name="getItems">入手できるアイテムのリスト</param>
    /// <param name="requiredNum">要求するアイテムの総数</param>
    /// <returns>Commonボタン(情報)</returns>
    public CommonUnitButton Create(List<IItem> getItems, int requiredNum)
    {
        // ボタン(実体)の生成
        GameObject obj = Instantiate(commonUnitPrefab, prefabParent);
        // オブジェクトからCommonUnitButtonのコンポーネントを取得
        CommonUnitButton cmnUnitBtn = obj.GetComponent<CommonUnitButton>();
        // Commonボタンの初期化
        cmnUnitBtn.Initialize(getItems, requiredNum);

        return cmnUnitBtn;
    }
}
