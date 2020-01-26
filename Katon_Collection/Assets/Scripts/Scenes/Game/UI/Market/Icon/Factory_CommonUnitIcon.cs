﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory_CommonUnitIcon : MonoBehaviour
{
    // アイコンのプレハブ
    [SerializeField]
    private GameObject iconPrefab;

    [SerializeField]
    Transform parent;

    [SerializeField]
    ItemContextTable table;

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
    public CommonUnitIcon Create(ITEM_TYPE type, int count, bool isPowerUp)
    {
        // アイコンの生成
        GameObject obj = Instantiate(iconPrefab, parent);
        obj.transform.SetParent(parent);
        // オブジェクトからCommonUnitIconのコンポーネントを取得
        CommonUnitIcon cmnUnitIcn = obj.GetComponent<CommonUnitIcon>();

        Sprite sprite = null;
        if (isPowerUp)
        {
            sprite = table.GetItemContex(type).GetPowerUpSprite();
        }
        else
        {
            sprite = table.GetItemContex(type).GetSprite();
        }

        // アイコンの初期化
        cmnUnitIcn.Initialize(sprite, count);

        return cmnUnitIcn;
    }

  

}
