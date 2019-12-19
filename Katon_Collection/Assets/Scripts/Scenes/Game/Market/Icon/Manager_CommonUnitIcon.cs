using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_CommonUnitIcon : MonoBehaviour
{
    // アイコンリスト
    private List<CommonUnitIcon> icons;

    // CommonUnitIconオブジェクト
    [SerializeField]
    CommonUnitIcon cmnUnitIcn;

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
    public void Add(CommonUnitIcon icon)
    {
        // リストに追加
        icons.Add(icon);
    }
}
