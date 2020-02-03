using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_CommonUnitButton : MonoBehaviour
{
    const int HUMAN_NECESSARY = 100;

    [SerializeField]
    Factory_CommonUnitButton factory;
    [SerializeField]
    Manager_CommonUnitButton manager;

    List<IItem> humanGetItems = new List<IItem>();
    List<CommonUnitButton> humanUnit = new List<CommonUnitButton>();

    CommonUnitButton selectButton = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        humanUnit.Clear();
        for (int i = 0; i < (int)ItemType.HumanMax; i++)
        {
            humanGetItems.Add(new Item(1, (ITEM_TYPE)i));
            int necessary = HUMAN_NECESSARY;
            CommonUnitButton button = factory.Create(humanGetItems[i], necessary);
            humanUnit.Add(button);
            manager.Add(button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CommonUnitButton button in manager.GetButtons())
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
        }
    }

    public void Create(List<IItem> items, int requiredNum)
    {
        manager.Add(factory.Create(items, requiredNum));
    }

    public CommonUnitButton GetSelectCommonUnitButton()
    {
        return selectButton;
    }

    /// <summary>
    /// 建築時の更新処理
    /// </summary>
    /// <param name="buildingTotal"></param>
    public void UpdateBuilding(int buildingTotal)
    {
        foreach (CommonUnitButton button in humanUnit)
        {
            int necessary = 0;//HUMAN_NECESSARY * buildingTotal;
            if (buildingTotal < 6)
            {
                switch (buildingTotal)
                {
                    case 0:
                        necessary = 20;
                        break;
                    case 1:
                        necessary = 50;
                        break;
                    case 2:
                        necessary = 100;
                        break;
                    case 3:
                        necessary = 160;
                        break;
                    case 4:
                        necessary = 240;
                        break;
                    case 5:
                        necessary = 300;
                        break;
                }
            }
            else
            {
                necessary = 300;
            }
            button.ChangeRequiredNum(necessary);
        }
    }
}
