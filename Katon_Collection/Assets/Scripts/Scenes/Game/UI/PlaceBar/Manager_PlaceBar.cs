using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_PlaceBar : MonoBehaviour
{
    //拠点の個数
    const int BASE_NUM = 4;


    Type m_placeType = Type.none;

    [SerializeField]
    GameObject placeBar;

    [SerializeField]
    Image m_pencil;

    [SerializeField]
    //拠点ボタン
    UI_Button m_bases;

    [SerializeField]
    //市場ボタン
    UI_Button m_ichiba;

    [SerializeField]
    //噴水ボタン
    UI_Button m_fountain;

    [SerializeField]
    //カメラボタン
    UI_Button m_camera;

    [SerializeField]
    //拠点の場所
    UI_Button[] m_placeButtons = new UI_Button[BASE_NUM];

    //カメラが押されたか
    bool m_isQRLeader = false;

    //アクティブな状態か
    bool m_active = false;

    // 移動するボタンを押したかどうか
    bool isClickButton = false;
    


    // Update is called once per frame
    void Update() {


        ClickEvent();
    }

    //ボタンをクリックしたときにタイプを設定
    void ClickEvent()
    {
        //拠点をクリックしたら
        if (m_bases.IsClick())
        {
            m_bases.OnClickProcess();

            m_active = !m_active;

            SetActiveBase(m_active);
        }

        isClickButton = false;

        //森
        if (m_placeButtons[0].IsClick())
        {
            m_placeButtons[0].OnClickProcess();
            
            ClickButton(Type.forest);

        }

        //洞窟
        if (m_placeButtons[1].IsClick())
        {
            m_placeButtons[1].OnClickProcess();
            
            ClickButton(Type.cave);
        }

        //工場
        if (m_placeButtons[2].IsClick())
        {
            m_placeButtons[2].OnClickProcess();
            
            ClickButton(Type.factory);

        }

        //牧場
        if (m_placeButtons[3].IsClick())
        {
            m_placeButtons[3].OnClickProcess();
            
            ClickButton(Type.farm);

        }



        //市場ボタンを押したら
        if (m_ichiba.IsClick())
        {
            m_ichiba.OnClickProcess();
            
            ClickButton(Type.market);

        }

        //噴水ボタンを押したら
        if (m_fountain.IsClick())
        {
            m_fountain.OnClickProcess();
            
            ClickButton(Type.fountain);

        }

        //カメラボタンを押したら
        m_isQRLeader = false;
        if (m_camera.IsClick())
        {
            m_camera.OnClickProcess();

            m_isQRLeader = true;
        }
    }
    
    //タイプの取得
    public Type GetchangeType()
    {
        return m_placeType;
    }

    public bool IsChangeCameraPosiiton()
    {
        if (m_placeType != Type.none && isClickButton) return true;
        return false;
    }

    public bool IsActiveShop()
    {
        if (m_placeType == Type.market && isClickButton) return true;
        return false;
    }

    public bool IsActiveFountain()
    {
        if (m_placeType == Type.fountain && isClickButton)
        {
            return true;
        }
        return false;
    }

    //カメラを起動したかを取得
    public bool GetIsQRLeader()
    {
        return m_isQRLeader;
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

    /// <summary>
    /// 移動ボタンを押した時の処理
    /// </summary>
    /// <param name="_placeType"></param>
    void ClickButton(Type _placeType)
    {
        m_placeType = _placeType;
        isClickButton = true;
    }

    /// <summary>
    /// 拠点ボタンが有効化かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsActiveBase()
    {
        return m_active;
    }

    public void SetActiveBase(bool _isActive)
    {
        m_active = _isActive;
        for (int i = 0; i < BASE_NUM; i++)
        {
            if (m_active)
            {
                m_placeButtons[i].gameObject.SetActive(true);
                m_pencil.gameObject.SetActive(true);
            }
            else
            {
                m_placeButtons[i].gameObject.SetActive(false);
                m_pencil.gameObject.SetActive(false);
            }

        }
    }
}
