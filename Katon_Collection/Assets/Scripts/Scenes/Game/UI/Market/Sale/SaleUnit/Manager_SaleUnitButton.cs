using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_SaleUnitButton : MonoBehaviour
{

    // ボタンのリスト
    public List<SaleUnitButton> buttons;


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
    public void Add(SaleUnitButton button)
    {
        buttons.Add(button);
    }

    /// <summary>
    /// ボタンを取得
    /// </summary>
    /// <returns>ボタンのリスト</returns>
    public List<SaleUnitButton> GetButtons()
    {
        return buttons;
    }
}
