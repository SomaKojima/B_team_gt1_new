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
        if(countUpBtn.IsClick())
        {
            countUpBtn.OnClickProcess();

            AddCount(1);
        }
        if (countDownBtn.IsClick())
        {
            countDownBtn.OnClickProcess();

            AddCount(-1);
        }
        if (applyBtn.IsClick())
        {
            applyBtn.OnClickProcess();
            gameObject.SetActive(false);
            isApply = true;
        }
        
        icon.SetNum(count);

    }

    public void Initialize(ITEM_TYPE type, int currentCnt)
    {
        gameObject.SetActive(true);
        count = currentCnt;
        UpdateSliderValue();
        icon.Initialize(type);

        isApply = false;
    }

    public void SetMaxCount(int _maxCount)
    {
        maxCount = _maxCount;
        sliderBtn.maxValue = maxCount;
    }

    public int GetCount()
    {
        return count;
    }

    public bool IsAplly()
    {
        return isApply;
    }

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

    public void OnChangeSliderValue()
    {
        int buf = (int)sliderBtn.value;
        count = buf;
        icon.SetNum(buf);
    }

    void UpdateSliderValue()
    {
        if(maxCount == 0)
        {
            sliderBtn.value = 0;
            return;
        }
        sliderBtn.value = count;
    }
}
