using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWindow : MonoBehaviour
{
    // 人間に対して何をするかを選択するウィンドウ
    [SerializeField]
    SelectHumanAction selectHumanAction;

    // 強化ウィンドウ
    [SerializeField]
    PowerUpWindow powerUpWindow;

    // 雇用ウィンドウ
    [SerializeField]
    EmploymentWindow employmentWindow;

    // 結果表示用のウィンドウ
    [SerializeField]
    HumanResuletWindow humanResultWindow;

    // 戻るボタンが押されたかどうか
    bool isBack = false;

    bool isActive = false;

    public void Initialize()
    {
        selectHumanAction.Initialize();

        powerUpWindow.UnActive();
        powerUpWindow.Initialize();

        employmentWindow.UnActive();
        employmentWindow.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        isBack = false;

        // 強化を選択
        if (selectHumanAction.IsActivePowerUpWindow())
        {
            selectHumanAction.UnActive();
            powerUpWindow.Active();
        }

        // 雇用を選択
        if (selectHumanAction.IsActiveEmploymentWindow())
        {
            selectHumanAction.UnActive();
            employmentWindow.Active();
        }

        // ウィンドウを閉じる処理
        if ((powerUpWindow.IsClickOutSide() || employmentWindow.IsClickOutSide()))
        {
            // 結果ウィンドウの内側をクリックした場合は無視する
            if (!(!humanResultWindow.IsClickOutSide() && humanResultWindow.IsActive()))
            {
                humanResultWindow.UnActive();
                powerUpWindow.UnActive();
                employmentWindow.UnActive();
                isBack = true;
            }
        }

        if(humanResultWindow.IsClickYes() || humanResultWindow.IsClickOutSide())
        {
            humanResultWindow.UnActive();

            // 全てのウィンドウが閉じられていたら戻る
            if (!powerUpWindow.gameObject.activeSelf && !employmentWindow.gameObject.activeSelf)
            {
                isBack = true;
            }
        }

        if (selectHumanAction.IsBack())
        {
            selectHumanAction.UnActive();
            isBack = true;
        }
    }

    public void Active()
    {
        selectHumanAction.Active();
        powerUpWindow.UnActive();
        employmentWindow.UnActive();
        humanResultWindow.UnActive();
        isBack = false;
        isActive = true;
    }
    
    public void UnActive()
    {
        selectHumanAction.UnActive();
        humanResultWindow.UnActive();
        isActive = false;
    }

    /// <summary>
    /// 強化させるかどうかを取得
    /// </summary>
    /// <returns></returns>
    public bool IsPowerUp()
    {
        return powerUpWindow.IsPowerUp();
    }

    /// <summary>
    /// 強化させる人間のタイプを取得
    /// </summary>
    /// <returns></returns>
    public ITEM_TYPE GetPowerUpItemType()
    {
        return powerUpWindow.GetPowerUpItemType();
    }
    
    /// <summary>
    /// 強化に必要な資源
    /// </summary>
    /// <returns></returns>
    public List<IItem> GetPowerUpResource()
    {
        return powerUpWindow.GetResources();
    }

    /// <summary>
    /// 雇用に必要な資源
    /// </summary>
    /// <returns></returns>
    public List<IItem> GetEmploymentResource()
    {
        return employmentWindow.GetResources();
    }

    /// <summary>
    /// 雇用するかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsEmployment()
    {
        return employmentWindow.IsEmployment();
    }


    /// <summary>
    /// 強化成功時の処理
    /// </summary>
    public void OnCorrectPowerUp()
    {
        powerUpWindow.OnCorrectPowerUp();
        humanResultWindow.Active(powerUpWindow.GetPowerUpItemType(), HumanResuletWindow.RESULT.POWER_UP_SUCCESS);
    }

    /// <summary>
    /// 強化失敗時の処理
    /// </summary>
    public void OnFailedPowerUp()
    {
        humanResultWindow.Active(powerUpWindow.GetPowerUpItemType(), HumanResuletWindow.RESULT.NOT_HAVE_RESOUTCE);
    }

    /// <summary>
    /// 建築時に呼ぶ処理
    /// </summary>
    public void OnBuilding(int _totalFloor)
    {
        powerUpWindow.OnBuilding(_totalFloor);
        employmentWindow.OnBuilding(_totalFloor);
    }

    /// <summary>
    /// 人間の情報を取得したときに呼ぶ処理
    /// </summary>
    /// <param name="_humanType"></param>
    public void OnGetCurrentPlaceHumanInfo(List<ITEM_TYPE> _humanType)
    {
        powerUpWindow.SetUnit(_humanType);
    }



    /// <summary>
    /// 雇用できた場合
    /// </summary>
    public void OnEmploymentSuccess(ITEM_TYPE type)
    {
        humanResultWindow.Active(type, HumanResuletWindow.RESULT.EMPLOYMENT_SUCCESS);
        employmentWindow.UnActive();
    }

    /// <summary>
    /// 雇用が失敗したときの処理
    /// </summary>
    /// <param name="type"></param>
    public void OnEmploymentFailed(ITEM_TYPE type)
    {
        humanResultWindow.Active(type, HumanResuletWindow.RESULT.NOT_HAVE_RESOUTCE);
    }

    /// <summary>
    /// 戻るボタン
    /// </summary>
    /// <returns></returns>
    public bool IsBack()
    {
        return isBack;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
    