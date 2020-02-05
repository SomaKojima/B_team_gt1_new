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

    // 戻るボタンが押されたかどうか
    bool isBack = false;

    public void Initialize()
    {
        selectHumanAction.Initialize();

        powerUpWindow.UnActive();
        powerUpWindow.Initialize();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        isBack = false;
        if (selectHumanAction.IsBack())
        {
            selectHumanAction.UnActive();
            isBack = true;
        }
    }

    public void Active()
    {
        if (selectHumanAction.gameObject.activeSelf) return;
        selectHumanAction.Active();
        powerUpWindow.UnActive();
        isBack = false;
    }
    
    public void UnActive()
    {
        if (!selectHumanAction.gameObject.activeSelf) return;

        selectHumanAction.UnActive();
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
    /// 強化成功時の処理
    /// </summary>
    public void OnCorrectPowerUp()
    {
        powerUpWindow.OnCorrectPowerUp();
    }

    /// <summary>
    /// 建築時に呼ぶ処理
    /// </summary>
    public void OnBuilding(int _totalFloor)
    {
        powerUpWindow.OnBuilding(_totalFloor);
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
    /// 戻るボタン
    /// </summary>
    /// <returns></returns>
    public bool IsBack()
    {
        return isBack;
    }
}
    