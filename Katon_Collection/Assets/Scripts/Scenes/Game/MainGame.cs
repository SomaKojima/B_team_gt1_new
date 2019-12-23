using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    QR_ReaderWindow qrReaderWindow;

    [SerializeField]
    Manager_PlaceBar manager_placeBar;

    [SerializeField]
    Manager_Item manager_item;

    [SerializeField]
    Owner_Human owner_human;

    [SerializeField]
    Owner_Floor owner_floor;

    [SerializeField]
    Owner_SignBoard owner_signBoard;

    [SerializeField]
    CameraMove cameraMove;

    [SerializeField]
    GameObject obj;

    [SerializeField]
    Fade_CloudEffect fade_CloudEffect = null;

    [SerializeField]
    Manage_SI_Player manager_SI_Player;

    //フェード　
    bool m_fade = false;

    //リザルトに移行したら
    bool m_switching = false;

    // Start is called before the first frame update
    void Start()
    {
        manager_item.Initialize();
        owner_human.Intialize();

    }

     

    // Update is called once per frame
    void Update()
    {
       
        // QRリーダーを起動
        if (manager_placeBar.GetIsQRLeader())
        {
            qrReaderWindow.Initialize();
        }

        // カメラを移動
        if (manager_placeBar.IsChangeCameraPosiiton())
        {
            m_fade = true;
            
            cameraMove.ChangePosition(manager_placeBar.GetchangeType());

            m_switching = false;
        }

        if(!m_switching)
        {
            if (m_fade)
            {
                StartCoroutine(fade_CloudEffect.FadeIn());
              

                if (!fade_CloudEffect.GetIsProcess)
                {
                    m_fade = false;
                   
                }
            }
            else
            {
                //フェードアウトの処理
                StartCoroutine(fade_CloudEffect.FadeOut());
            }
        }
       



        // QR読み込み完了
        if (qrReaderWindow.IsExchange())
        {
            bool isExchangable = IsExchangable();
            qrReaderWindow.FinishExchange(isExchangable);
            if(isExchangable)
            {
                // アイテムのマネージャに追加・削除
                foreach (IItem item in qrReaderWindow.GetItems())
                {
                    manager_item.GetItem(item.GetItemType()).AddCount(item.GetCount());
                }
            }
        }

        // アイテムのマネージャと人間の数を合わせる
        for (int i = 0; i < (int)ITEM_TYPE.WOOD; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            owner_human.MatchItemsHumans(manager_item.GetItem(type), false);
        }
        
        // 看板が押されたら建築
        if (owner_signBoard.IsBuilding())
        {
            Type type = owner_signBoard.GetPlaceType();
            owner_floor.Building(type);
        }

        if (owner_signBoard.IsSigneBoardInScreen(cameraMove.GetCamera()))
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }

    bool IsExchangable()
    {
        foreach (IItem item in qrReaderWindow.GetItems())
        {
            IItem myItem = manager_item.GetItem(item.GetItemType());

            if (myItem.GetCount() + item.GetCount() < 0) return false;
        }
        return true;
    }

    //リザルトに行くときのフェード
    void ResultStart()
    {
        m_switching = true;
        StartCoroutine(fade_CloudEffect.FadeIn());
    }

    private void ChangedItem(int Count, int ItemType)
    {
        for (int i = 0; i < manager_SI_Player.GetPlayers().Count; i++)
        {
            if (PhotonNetwork.player.ID == manager_SI_Player.GetPlayer(i).ID)
            {
                manager_SI_Player.GetPlayer(i).SetItemCount(Count, ItemType);
            }
        }
    }

}
