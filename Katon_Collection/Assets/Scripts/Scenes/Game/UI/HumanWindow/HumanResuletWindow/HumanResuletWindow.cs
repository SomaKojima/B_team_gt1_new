using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanResuletWindow : MonoBehaviour
{
    public enum RESULT
    {
        POWER_UP_SUCCESS,       // 強化成功時
        EMPLOYMENT_SUCCESS,     // 雇用成功時
        NOT_HAVE_RESOUTCE,      // 素材が足りない

        MAX
    }
    [SerializeField]
    ItemContextTable itemContextTable;

    [SerializeField]
    Image humanImage;

    // 結果を表示するテキスト
    [SerializeField]
    Text resultText;

    [SerializeField]
    UI_Button yesButton;
    
    [SerializeField, EnumListLabel(typeof(RESULT))]
    string[] resultString = new string[(int)RESULT.MAX];

    [SerializeField]
    Sprite noneSprite;

    [SerializeField]
    JudgeClickUI judgeClickUI;

    bool isClickYes = false;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isClickYes = false;
        if (yesButton.IsClick())
        {
            yesButton.OnClickProcess();
            //UnActive();
            isClickYes = true;
        }
    }


    public void Active(ITEM_TYPE _humanType, RESULT _result)
    {
        gameObject.SetActive(true);
        SetSprite(_humanType, _result);
        resultText.text = resultString[(int)_result];
        isActive = true;
        judgeClickUI.Initialize();
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
        judgeClickUI.Initialize();
        isClickYes = false;
        isActive = false;
    }

    void SetSprite(ITEM_TYPE _humanType, RESULT _result)
    {
        if(_humanType == ITEM_TYPE.NONE)
        {
            humanImage.sprite = noneSprite;
            return;
        }

        humanImage.gameObject.SetActive(true);
        if (_result == RESULT.POWER_UP_SUCCESS)
        {
            humanImage.sprite = itemContextTable.GetItemContex(_humanType).GetPowerUpSprite();
        }
        else
        {
            humanImage.sprite = itemContextTable.GetItemContex(_humanType).GetSprite();
        }
    }

    public bool IsClickYes()
    {
        return isClickYes;
    }

    public bool IsClickOutSide()
    {
        return judgeClickUI.IsClickOutSide();
    }

    public bool IsActive()
    {
        return isActive;
    }
}
