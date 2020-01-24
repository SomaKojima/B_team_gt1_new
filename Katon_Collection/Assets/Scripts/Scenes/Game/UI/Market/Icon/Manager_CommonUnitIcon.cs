using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_CommonUnitIcon : MonoBehaviour
{
    // アイコンリスト
    private List<CommonUnitIcon> icons = new List<CommonUnitIcon>();

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

    public void AllDestory()
    {
        foreach (CommonUnitIcon icon in icons)
        {
            Destroy(icon.gameObject);
        }

        icons.Clear();
    }

    public List<CommonUnitIcon> Icons
    {
        get { return icons; }
    }
}
