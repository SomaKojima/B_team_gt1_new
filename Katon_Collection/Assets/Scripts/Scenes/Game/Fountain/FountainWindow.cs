using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainWindow : MonoBehaviour
{
    [SerializeField]
    SelectItemButtonWidnow payWindow;

    [SerializeField]
    SelectItemButtonWidnow getWindow;

    [SerializeField]
    UI_Button createQRButton;

    [SerializeField]
    QRWindow qrWindow;

    [SerializeField]
    UI_Button back;

    Manager_Item manager_getItem = new Manager_Item();

    QR_Encode qrEncode = new QR_Encode();

    List<IItem> items = new List<IItem>();

    bool isBack = false;

    public void Initialize(Manager_Item _pay)
    {

        payWindow.Initialize(_pay);

        manager_getItem.Initialize();
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            manager_getItem.GetItem((ITEM_TYPE)i).SetCount(1000);
        }
        getWindow.Initialize(manager_getItem);
    }
    // Start is called before the first frame update
    void Start()
    {
        qrEncode.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // QR生成
        if (createQRButton.IsClick())
        {
            createQRButton.OnClickProcess();
            ActiveQR();
        }

        isBack = false;
        if (back.IsClick())
        {
            back.OnClickProcess();
            UnActive();
            isBack = true;
        }
        
    }

    void ActiveQR()
    {
        string code = "";
        items.Clear();

        Manager_Item getButtonItems = getWindow.GetManagerItem();
        Manager_Item payButtonItems = payWindow.GetManagerItem();

        for (int i = 0; i< (int)ITEM_TYPE.NUM; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            
            if (getButtonItems.GetItem(type).GetCount() != 0)
            {
                getButtonItems.GetItem(type).SetCount(-getButtonItems.GetItem(type).GetCount());
                items.Add(getButtonItems.GetItem(type));
            }
            if (payButtonItems.GetItem(type).GetCount() != 0)
            {
                items.Add(payButtonItems.GetItem(type));
            }
        }

        qrEncode.EncodeToQRCode(items, ref code);
        Debug.Log(code);

        if (code != "")
        {
            qrWindow.Active(code);
        }

        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            ITEM_TYPE type = (ITEM_TYPE)i;
            if (getButtonItems.GetItem(type).GetCount() != 0)
            {
                getButtonItems.GetItem(type).SetCount(-getButtonItems.GetItem(type).GetCount());
            }
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
    }


    public bool IsBack()
    {
        return isBack;
    }
}
