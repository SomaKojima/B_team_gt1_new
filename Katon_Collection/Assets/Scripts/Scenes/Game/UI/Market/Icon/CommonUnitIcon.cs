using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonUnitIcon : MonoBehaviour
{
    // アイコン画像
    [SerializeField]
    private Image icon = null;
    // 資材の数
    [SerializeField]
    private int count = 0;
    // 資材の数(テキスト)
    [SerializeField]
    private Text numText = null;

    [SerializeField]
    Image redImage;

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
        numText.text = _count.ToString();
        redImage.gameObject.SetActive(false);
        UnActiveRed();
    }

    public int GetCount(){ return count; }

    public void ActiveRed() { redImage.gameObject.SetActive(true); }
    public void UnActiveRed() { redImage.gameObject.SetActive(false); }
}
