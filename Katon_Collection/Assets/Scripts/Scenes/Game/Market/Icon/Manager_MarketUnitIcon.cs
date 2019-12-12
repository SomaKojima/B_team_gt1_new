using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_MarketUnitIcon : MonoBehaviour
{
    // アイコンリスト
    private List<MarketUnitIcon> icons;

    // MarketUnitIconオブジェクト
    [SerializeField]
    MarketUnitIcon mktUnitIcn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// アイコンの追加
    /// </summary>
    /// <param name="icon">追加するアイコン</param>
    public void Add(MarketUnitIcon icon)
    {
        // リストに追加
        icons.Add(icon);
    }
}
