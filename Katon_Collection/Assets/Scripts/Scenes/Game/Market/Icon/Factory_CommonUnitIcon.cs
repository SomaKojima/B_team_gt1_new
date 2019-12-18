using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory_CommonUnitIcon : MonoBehaviour
{
    // アイコンのプレハブ
    [SerializeField]
    private GameObject iconPrefab;

    // CommonUnitIconオブジェクト
    //[SerializeField]
    //CommonUnitIcon cmnUnitIcn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// アイコンの生成
    /// </summary>
    /// <param name="parent">アイコンを生成するための親オブジェクト</param>
    /// <param name="type">アイコン(資源)の種類</param>
    /// <param name="count">資材の数</param>
    public CommonUnitIcon Create(Transform parent, ITEM_TYPE type, int count)
    {
        // アイコンの生成
        GameObject obj = Instantiate(iconPrefab, parent);
        // オブジェクトからCommonUnitIconのコンポーネントを取得
        CommonUnitIcon cmnUnitIcn = obj.GetComponent<CommonUnitIcon>();

        // 種類に応じてスプライトを差し替える
        Sprite sprite = DependSpriteImage(type);
        obj.transform.FindChild("icon").GetComponent<Image>().sprite = sprite;
        // アイコンの初期化
        cmnUnitIcn.Initialize(obj.transform.FindChild("icon").GetComponent<Image>().sprite, count);

        return cmnUnitIcn;
    }

    /// <summary>
    /// 資材の種類に応じたスプライト情報を返す
    /// </summary>
    /// <param name="type">資材の種類</param>
    /// <returns>スプライト情報</returns>
    public Sprite DependSpriteImage(ITEM_TYPE type)
    {
        // 種類に応じたスプライトを返す
        Sprite sprite = null;
        switch (type)
        {
            // 木こり
            case ITEM_TYPE.LOOGER:
                sprite = Resources.Load<Sprite>("Images/Scenes/Game/Icon/icon_logger");
                break;
            // 炭鉱夫
            case ITEM_TYPE.COAL_MINER:
                sprite = Resources.Load<Sprite>("Images/Scenes/Game/Icon/icon_coalMiner");
                break;
            // エンジニア
            case ITEM_TYPE.ENGINEER:
                sprite = Resources.Load<Sprite>("Images/Scenes/Game/Icon/icon_enginer");
                break;

            // 建材
            case ITEM_TYPE.WOOD:
                sprite = Resources.Load<Sprite>("Images/Scenes/Game/Icon/icon_wood");
                break;
            // 鉱石
            case ITEM_TYPE.ORE:
                sprite = Resources.Load<Sprite>("Images/Scenes/Game/Icon/icon_ore");
                break;
            // パーツ
            case ITEM_TYPE.PARTS:
                sprite = Resources.Load<Sprite>("Images/Scenes/Game/Icon/icon_parts");
                break;

            default:
                Debug.Log("そんなものはねぇ");
                break;
        }

        return sprite;
    }
}
