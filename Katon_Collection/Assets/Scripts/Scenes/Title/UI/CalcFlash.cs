using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalcFlash : MonoBehaviour
{

    float m_duringFrame;

    float m_frame = 0.5f;

    //光っているかのフラグ
    bool m_isBright;

    Image m_image;

    //光ってるか取得
    public bool IsFlash()
    {
        return m_isBright;
    }

    void Start()
    {
        m_duringFrame = Time.time;

        m_image = this.gameObject.GetComponent<Image>();

    }

    private void Update()
    {
        UpdateFrame();

        
    }

    //フレームの更新
    private void UpdateFrame()
    {
        if (Time.time > m_duringFrame)
        {
            m_isBright = true;

            m_image.enabled = !m_image.enabled;

            m_duringFrame += m_frame;
        }
        m_isBright = false;
    }

   
}
