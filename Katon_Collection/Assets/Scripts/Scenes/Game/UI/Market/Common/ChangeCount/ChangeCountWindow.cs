using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCountWindow : MonoBehaviour
{
    [SerializeField]
    UI_Button countUpBtn;
    [SerializeField]
    UI_Button countDownBtn;
    [SerializeField]
    Slider sliderBtn;
    [SerializeField]
    UI_Button applyBtn;

    [SerializeField]
    ChangeCountIcon icon;

    int count = 0;
    int maxCount = 0;

    bool isApply = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 個数を増やす
        if(countUpBtn.IsClick())
        {
            countUpBtn.OnClickProcess();

            AddCount(1);
        }

        // 個数を減らす
        if (countDownBtn.IsClick())
        {
            countDownBtn.OnClickProcess();

            AddCount(-1);
        }

        // 決定ボタン
        if (applyBtn.IsClick())
        {
            applyBtn.OnClickProcess();
            isApply = true;
        }
        
        icon.SetNum(count);

    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="type"></param>
    /// <param name="currentCnt"></param>
    public void Initialize(ITEM_TYPE type, int currentCnt, bool isPowerUp)
    {
        count = currentCnt;
        UpdateSliderValue();
        icon.Initialize(type, isPowerUp);

        isApply = false;
    }

    /// <summary>
    /// 最大値の設定
    /// </summary>
    /// <param name="_maxCount"></param>
    public void SetMaxCount(int _maxCount)
    {
        maxCount = _maxCount;
        sliderBtn.maxValue = maxCount;
    }

    /// <summary>
    /// 個数を取得
    /// </summary>
    /// <returns></returns>
    public int GetCount()
    {
        return count;
    }

    /// <summary>
    /// 決定ボタンが押されたかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsAplly()
    {
        return isApply;
    }

    /// <summary>
    /// 個数の追加
    /// </summary>
    /// <param name="cnt"></param>
    private void AddCount(int cnt)
    {
        count += cnt;
        if (count < 0)
        {
            count = 0;
        }
        else if (count > maxCount)
        {
            count = maxCount;
        }
        UpdateSliderValue();
    }

    /// <summary>
    /// スライダーの値が変わったときの処理
    /// </summary>
    public void OnChangeSliderValue()
    {
        int buf = (int)sliderBtn.value;
        count = buf;
        icon.SetNum(buf);
    }

    /// <summary>
    /// スライダーの更新処理
    /// </summary>
    void UpdateSliderValue()
    {
        if(maxCount == 0)
        {
            sliderBtn.value = 0;
            return;
        }
        sliderBtn.value = count;
    }


    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
        isApply = false;
    }
}
