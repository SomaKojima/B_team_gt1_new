using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonUnitIcon : MonoBehaviour
{
    public Image icon = null;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// アイコンの初期化
    /// </summary>
    /// <param name="sprite">スプライト情報</param>
    /// <param name="_count">交換に必要な数</param>
    public void Initialize(Sprite sprite, int _count)
    {
        icon.sprite = sprite;
        count = _count;
    }
}
