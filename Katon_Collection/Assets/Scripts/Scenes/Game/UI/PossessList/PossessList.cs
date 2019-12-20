using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossessList : MonoBehaviour
{
    // 表示/非表示ボタン
    [SerializeField]
    private UI_Button switchButton;

    // リストの幅
    private float listWidth = 0.0f;

    // 表示/非表示ボタンのテキスト
    private Text buttonText = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="width">リストの幅</param>
    public void Initialize(float width)
    {
        listWidth = width;
    }

    public float GetWidth()                 { return listWidth; }

    public void SetButtonText(string str)   { switchButton.GetComponentInChildren<Text>().GetComponent<Text>().text = str;}

    public bool GetClick()                  { return switchButton.IsClick(); }
    public void FinishClick()               { switchButton.OnClickProcess(); }
}
