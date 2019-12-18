using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGame : MonoBehaviour
{

    [SerializeField]
    private BackGround_Title m_backGround_Title = null;

    [SerializeField]
    private UI_Title_Tap m_uI_Title_Tap = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_backGround_Title.ChangePlace();
    }
}
