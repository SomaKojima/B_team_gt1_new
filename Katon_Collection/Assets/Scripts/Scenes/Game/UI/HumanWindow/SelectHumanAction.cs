using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHumanAction : MonoBehaviour
{

    // 強化ボタン
    [SerializeField]
    UI_Button powerUp;

    // 雇用ボタン
    [SerializeField]
    UI_Button employmentButton;

    // 戻るボタン
    [SerializeField]
    UI_Button backBtn;

    bool isPowerUp = false;
    bool isEmployment = false;
    bool isBack = false;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // 強化ウィンドウを表示させる
        isPowerUp = false;
        if (powerUp.IsClick())
        {
            powerUp.OnClickProcess();
            isPowerUp = true;
        }

        // 雇用ウィンドウを表示させる
        isEmployment = false;
        if (employmentButton.IsClick())
        {
            employmentButton.OnClickProcess();
            isEmployment = true;
        }

        // 戻るボタンの処理
        isBack = false;
        if (backBtn.IsClick())
        {
            backBtn.OnClickProcess();
            isBack = true;
        }
    }

    public void Active()
    {
        if (gameObject.activeSelf) return;
        gameObject.SetActive(true);
        
    }

    public void UnActive()
    {
        if (!gameObject.activeSelf) return;
        gameObject.SetActive(false);
        isPowerUp = false;
        isEmployment = false;
        isBack = false;
    }


    /// <summary>
    /// 戻るボタンが押されたかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsBack()
    {
        return isBack;
    }

    /// <summary>
    /// パワーアップを選択
    /// </summary>
    /// <returns></returns>
    public bool IsActivePowerUpWindow()
    {
        return isPowerUp;
    }

    /// <summary>
    /// 雇用を選択
    /// </summary>
    /// <returns></returns>
    public bool IsActiveEmploymentWindow()
    {
        return isEmployment;
    }
}
