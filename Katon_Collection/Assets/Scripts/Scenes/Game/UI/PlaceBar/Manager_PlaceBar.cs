using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_PlaceBar : MonoBehaviour
{
    enum PlaceBarButtonType
    {
        NONE,
        HUMAN,
        MARKET,
        CAMERA
    }
    [SerializeField]
    GameObject placeBar;

    [SerializeField]
    //人間ボタン
    UI_Button m_human;

    [SerializeField]
    //市場ボタン
    UI_Button m_ichiba;

    [SerializeField]
    //カメラボタン
    UI_Button m_camera;

    PlaceBarButtonType buttonType = PlaceBarButtonType.NONE;

    // Update is called once per frame
    void Update() {


        ClickEvent();
    }

    //ボタンをクリックしたときにタイプを設定
    void ClickEvent()
    {
        buttonType = PlaceBarButtonType.NONE;


        //拠点をクリックしたら
        if (m_human.IsClick())
        {
            m_human.OnClickProcess();
            buttonType = PlaceBarButtonType.HUMAN;
        }
        
        //市場ボタンを押したら
        if (m_ichiba.IsClick())
        {
            m_ichiba.OnClickProcess();
            buttonType = PlaceBarButtonType.MARKET;
        }

        //カメラボタンを押したら
        if (m_camera.IsClick())
        {
            m_camera.OnClickProcess();

            buttonType = PlaceBarButtonType.CAMERA;
        }
    }
    
    /// <summary>
    /// 人間のボタンが押されたかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsClickHuman()
    {
        return IsClickButton(PlaceBarButtonType.HUMAN);
    }

    /// <summary>
    /// 市場のボタンが押されたかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsClickMarket()
    {
        return IsClickButton(PlaceBarButtonType.MARKET);
    }

    /// <summary>
    /// カメラのボタンが押されたかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsClickCamera()
    {
        return IsClickButton(PlaceBarButtonType.CAMERA);
    }

    /// <summary>
    /// 特定のボタンが押されたかどうか 
    /// </summary>
    /// <param name="_buttonType"></param>
    /// <returns></returns>
    bool IsClickButton(PlaceBarButtonType _buttonType)
    {
        if (buttonType == _buttonType) return true;
        return false;
    }


    public void Active()
    {
        if (placeBar.activeSelf) return;
        placeBar.SetActive(true) ;
    }

    public void UnActive()
    {
        if (!placeBar.activeSelf) return;
        placeBar.SetActive(false);
    }
}
