using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_SaleUnitButton : MonoBehaviour
{
    [SerializeField]
    Manager_SaleUnitButton manager;

    [SerializeField]
    Factory_SaleUnitButton factory;

    SaleUnitButton selectButton = null;

    Manager_Item managerItem;

    public void Initialize(Manager_Item _managerItem)
    {
        managerItem = _managerItem;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SaleUnitButton button in manager.GetButtons())
        {
            if (button != selectButton && selectButton != null)
            {
                button.ActiveMask();
            }
            if (button.IsClick())
            {
                button.OnClickProcess();
                button.UnActiveMask();
                selectButton = button;
            }


            // 支払うアイテムが足りているかどうか判定
            button.ChackePayItems(managerItem.GetItemList());
        }
    }

    public void Create(List<IItem> getItems, List<IItem> payItems)
    {
        manager.Add(factory.Create(getItems, payItems));
    }

    public SaleUnitButton GetSelectCommonUnitButton()
    {
        return selectButton;
    }

    /// <summary>
    /// 建築時の更新処理
    /// </summary>
    /// <param name="buildingTotal"></param>
    public void UpdateBuilding(int buildingTotal)
    {

    }

    public void AllDestory()
    {
        manager.AllDestory();
    }
}
