using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCountWindow1 : MonoBehaviour
{
    [SerializeField]
    UI_Button[] numButtons = new UI_Button[10];

    [SerializeField]
    UI_Button applyBtn;

    [SerializeField]
    ChangeCountIcon normalIcon;
    
    [SerializeField]
    ChangeCountIcon powerUpicon;

    [SerializeField]
    Image selectFrameImage;

    [SerializeField]
    UI_Button deleteBtn;

    int normalCount = 0;
    int powerUpCount = 0;
    int normalMaxCount = 0;
    int powerUpMaxCount = 0;

    bool isApply = false;
    bool isSelectNormal = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numButtons.Length; i++)
        {
            // 番号が押された
            if (numButtons[i].IsClick())
            {
                numButtons[i].OnClickProcess();
                UpdateAddNum(i);
            }
        }

        if (deleteBtn.IsClick())
        {
            deleteBtn.OnClickProcess();
            if (isSelectNormal)
            {
                normalCount = DeleteNum(normalCount);
            }
            else
            {
                powerUpCount = DeleteNum(powerUpCount);
            }

        }

        if (normalIcon.IsClick())
        {
            isSelectNormal = true;
        }
        if (powerUpicon.IsClick())
        {
            isSelectNormal = false;
        }

        // 決定ボタン
        if (applyBtn.IsClick())
        {
            applyBtn.OnClickProcess();
            isApply = true;
        }

        UpdateSelectFrameImage();
        normalIcon.SetNum(normalCount);
        powerUpicon.SetNum(powerUpCount);
    }

    void UpdateAddNum(int addNum)
    {
        if (isSelectNormal)
        {
            normalCount = AddNum(normalCount, addNum);
            if (normalCount > normalMaxCount)
            {
                normalCount = normalMaxCount;
            }
        }
        else
        {
            powerUpCount = AddNum(powerUpCount, addNum);
            if (powerUpCount > powerUpMaxCount)
            {
                powerUpCount = powerUpMaxCount;
            }
        }
    }

    // 番号の追加
    int AddNum(int value ,int addValue)
    {
        value *= 10;
        value += addValue;

        if (value >= 10000)
        {
            value = 9999;
        }
        return value;
    }

    int DeleteNum(int value)
    {
        value /= 10;
        return value;
    }

    void UpdateSelectFrameImage()
    {
        Vector3 target = selectFrameImage.transform.position;
        if (isSelectNormal)
        {
            target = normalIcon.transform.position;
        }
        else
        {
            target = powerUpicon.transform.position;
        }
        selectFrameImage.transform.position = Vector3.Lerp(selectFrameImage.transform.position, target, 0.3f);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="type"></param>
    /// <param name="currentCnt"></param>
    public void Initialize(ITEM_TYPE type, int currentCnt, int currentPowerUpCnt)
    {
        normalCount = currentCnt;
        powerUpCount = currentPowerUpCnt;
        if (ItemType.IsHumanType(type))
        {
            powerUpicon.gameObject.SetActive(true);
            powerUpicon.Initialize(type, true);
        }
        else
        {
            powerUpicon.gameObject.SetActive(false);
        }
        normalIcon.Initialize(type, false);

        isApply = false;
        selectFrameImage.transform.position = normalIcon.transform.position;
        isSelectNormal = true;

    }

    /// <summary>
    /// 最大値の設定
    /// </summary>
    /// <param name="_maxCount"></param>
    public void SetNormalMaxCount(int _maxCount)
    {
        normalMaxCount = _maxCount;
    }

    public void SetPowerUpMaxCount(int _maxCount)
    {
        powerUpMaxCount = _maxCount;
    }

    /// <summary>
    /// 個数を取得
    /// </summary>
    /// <returns></returns>
    public int GetNormalCount()
    {
        return normalCount;
    }

    public int GetPoerUpCount()
    {
        return powerUpCount;
    }

    /// <summary>
    /// 決定ボタンが押されたかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsAplly()
    {
        return isApply;
    }
    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
        isApply = false;
        isSelectNormal = false;
    }
}
