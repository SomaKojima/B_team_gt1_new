using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_CommonUnitIcon : MonoBehaviour
{
    // アイコンのプレハブ
    [SerializeField]
    private GameObject iconPrefab;

    // CommonUnitIconオブジェクト
    [SerializeField]
    CommonUnitIcon mktUnitIcn;

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
    /// <param name="parent"></param>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public CommonUnitIcon Create(Transform parent, ITEM_TYPE type, int count)
    {
        // アイコンの生成
        GameObject obj = Instantiate(iconPrefab, parent);

        CommonUnitIcon cmnUnitIcn = new CommonUnitIcon();
        cmnUnitIcn.Initialize(obj.GetComponent<Sprite>(), count);

        return mktUnitIcn;
    }
}
