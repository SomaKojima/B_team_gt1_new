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
            isApply = true;
        }
        
        icon.SetNum(count);

    }

    public void Initialize(int currentCnt)
    {
        count = currentCnt;
        sliderBtn.value = count;

        isApply = false;
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
        sliderBtn.value = count;
    }

    public void OnChangeSliderValue()
    {
        count = (int)sliderBtn.value;
        icon.SetNum((int)sliderBtn.value);
    }
}
