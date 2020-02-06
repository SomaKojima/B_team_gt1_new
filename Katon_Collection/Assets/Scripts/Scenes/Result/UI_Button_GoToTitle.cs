using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button_GoToTitle : UI_Button
{
    //リザルトからタイトルボタンへ遷移する
    [SerializeField]
    private UI_Button m_goto_result_button = null;
    [SerializeField]
    Manager_ServerInterface server = null;

    //最初はボタンを非表示にしておく
    private void Start()
    {
        m_goto_result_button.gameObject.SetActive(false);
    }

    //ボタンを押した
    public void ClickgotoResultButton()
    {
        m_goto_result_button.IsClickEnter();
        server.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        m_goto_result_button.OnClickProcess();
    }

    //タイトルへ
    public bool IsClickGotoTitle()
    {
        return m_goto_result_button.IsClick();
    }


}
