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

    float addFrame = 0;
    float addFrameScale = 0.5f;
    float addFrameDuring = 0.5f;
    float addFrameMaxDuring = 0.0f;
    float addFrameMinDuring = 0.02f;
    bool isAddCount = false;
    int addCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        addFrameMaxDuring = addFrameDuring;
    }

    // Update is called once per frame
    void Update()
    {
        // 個数を増やす
        if(countUpBtn.IsClick())
        {
            countUpBtn.OnClickProcess();
        }

        // 個数を減らす
        if (countDownBtn.IsClick())
        {
            countDownBtn.OnClickProcess();
        }

        // ボタンを離した時
        if (Input.GetMouseButtonUp(0))
        {
            addFrameDuring = addFrameMaxDuring;
            addFrame = 0;
            isAddCount = false;
            addCount = 0;
        }
        // ボタンが押されていれば
        if(Input.GetMouseButton(0) && addCount != 0)
        {
            if (isAddCount)
            {
                AddCount(addCount);
            }
            UpdateAddFrame();
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

    void UpdateAddFrame()
    {
        addFrame += Time.deltaTime;
        if (addFrame > addFrameDuring)
        {
            addFrame = 0;
            addFrameDuring = addFrameDuring * addFrameScale;
            if (addFrameDuring < addFrameMinDuring)
            {
                addFrameDuring = addFrameMinDuring;
            }
            isAddCount = true;
        }
    }

    /// <summary>
    /// 個数の追加
    /// </summary>
    /// <param name="cnt"></param>
    private void AddCount(int cnt)
    {
        isAddCount = false;
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

    public void OnPlusButton()
    {
        isAddCount = true;
        addCount = 1;
    }

    public void OnMinusButton()
    {
        isAddCount = true;
        addCount = -1;
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
