using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_CommonUnitButton : MonoBehaviour
{
    // ボタンのリスト
    List<CommonUnitButton> buttons = new List<CommonUnitButton>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ボタンを増やす
    /// </summary>
    /// <param name="button">増やすボタン</param>
    public void Add(CommonUnitButton button)
    {
        buttons.Add(button);
    }

    /// <summary>
    /// ボタンを取得
    /// </summary>
    /// <returns>ボタンのリスト</returns>
    public List<CommonUnitButton> GetButtons()
    {
        return buttons;
    }
}
